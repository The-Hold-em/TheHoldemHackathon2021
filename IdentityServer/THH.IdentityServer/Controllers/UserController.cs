using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using THH.IdentityServer.Dtos;
using THH.IdentityServer.Models;
using THH.Shared.Core.ExtensionMethods;
using THH.Shared.Core.Response;

using static Duende.IdentityServer.IdentityServerConstants;

namespace THH.IdentityServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(LocalApi.PolicyName)]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(UserManager<ApplicationUser> userManager, IMapper mapper, ILogger<UserController> logger)
        {
            _userManager = userManager;
            this._mapper = mapper;
            this._logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            List<ApplicationUser> users = _userManager.Users.ToList();
            ICollection<ApplicationUserDto> data = new List<ApplicationUserDto>();
            foreach (ApplicationUser user in users)
            {
                ApplicationUserDto map = _mapper.Map<ApplicationUserDto>(user);
                map.Roles = await _userManager.GetRolesAsync(user);
                data.Add(map);
            }
            return Response<IEnumerable<ApplicationUserDto>>.Success(data).CreateResponseInstance();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            ApplicationUser user = _mapper.Map<ApplicationUser>(model);

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return GetResult(result, "SignUp");


            IdentityResult roleResult = await _userManager.AddToRolesAsync(user, model.Roles);

            if (!roleResult.Succeeded)
                return GetResult(roleResult, "SignUp_AddRole");

            return Response<NoContent>.Success(statusCode: StatusCodes.Status201Created).CreateResponseInstance();
        }

        [HttpPut]
        public async Task<IActionResult> Update(ApplicationUserDto dto)
        {
            ApplicationUser updateModel = _mapper.Map<ApplicationUser>(dto);
            ApplicationUser user = await _userManager.FindByIdAsync(updateModel.Id);
            if (user.IsNull()) return Response<NoContent>.Fail(
              statusCode: StatusCodes.Status400BadRequest,
              isShow: true,
              path: "api/role/put",
              errors: "Gecerli bir kullanici bulunamadi"
              ).CreateResponseInstance();

            if (!user.Email.Equals(updateModel.Email))
            {
                IdentityResult emailResponse = await _userManager.SetEmailAsync(user, updateModel.Email);
                if (!emailResponse.Succeeded) return GetResult(emailResponse, "UpdateEmail");
            }

            if (!user.PhoneNumber.Equals(updateModel.PhoneNumber))
            {
                IdentityResult res = await _userManager.SetPhoneNumberAsync(user, updateModel.PhoneNumber);
                if (!res.Succeeded) return GetResult(res, "UpdatePhoneNumber");
            }

            if (!user.UserName.Equals(updateModel.UserName))
            {
                IdentityResult res = await _userManager.SetUserNameAsync(user, updateModel.UserName);
                if (!res.Succeeded) return GetResult(res, "UpdateUserName");
            }


            string token = await _userManager.GeneratePasswordResetTokenAsync(user);

            IdentityResult passwordRes = await _userManager.ResetPasswordAsync(user, token, dto.Password);

            if (!passwordRes.Succeeded) return GetResult(passwordRes, "UpdatePassword");

            user.FirstName = updateModel.FirstName;
            user.LastName = updateModel.LastName;
            user.IdentityNumber = updateModel.IdentityNumber;
            user.Address = updateModel.Address;
            IdentityResult updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                return GetResult(updateResult, "UpdateUser");

            IList<string> userRoles = await _userManager.GetRolesAsync(user);
            _logger.LogInformation($"Update Dto Roles: ${dto.Roles.IsNotNull()} {dto.Roles?.Count()}");
            _logger.LogInformation($"User Roles: ${userRoles.IsNotNull()} {userRoles?.Count()}");
            IEnumerable<string> deleteRoles = userRoles?.Where(x => !dto.Roles.Contains(x));
            IEnumerable<string> addRoles = dto.Roles?.Where(x => !userRoles.Contains(x));
            await _userManager.RemoveFromRolesAsync(user, deleteRoles);
            await _userManager.AddToRolesAsync(user, addRoles);
            return Response<NoContent>.Success().CreateResponseInstance();
        }
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            Claim userClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);

            if (userClaim.IsNull()) return Response<NoContent>.Fail(
                 statusCode: StatusCodes.Status400BadRequest,
                 isShow: true,
                 path: "api/User/GetUser",
                 errors: "Kullanici girisi dogrulanamadi"
                 ).CreateResponseInstance();

            ApplicationUser user = await _userManager.FindByIdAsync(userClaim.Value);

            if (user.IsNull()) return Response<NoContent>.Fail(
                 statusCode: StatusCodes.Status400BadRequest,
                 isShow: true,
                 path: "api/User/GetUser",
                 errors: "Gecerli bir kullanici bulunamadi"
                 ).CreateResponseInstance();

            ApplicationUserDto dto = _mapper.Map<ApplicationUserDto>(user);
            dto.Roles = await _userManager.GetRolesAsync(user);

            return Response<ApplicationUserDto>.Success(
                 data: dto,
                 statusCode: StatusCodes.Status200OK
                  ).CreateResponseInstance();
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id.ToString());

            if (user.IsNull()) return Response<NoContent>.Fail(
                 statusCode: StatusCodes.Status400BadRequest,
                 isShow: true,
                 path: "api/User/GetUser",
                 errors: "Gecerli bir kullanici bulunamadi"
                 ).CreateResponseInstance();

            ApplicationUserDto dto = _mapper.Map<ApplicationUserDto>(user);
            dto.Roles = await _userManager.GetRolesAsync(user);

            return Response<ApplicationUserDto>.Success(
                 data: dto,
                 statusCode: StatusCodes.Status200OK
                  ).CreateResponseInstance();
        }

        private IActionResult GetResult(IdentityResult result, string action = "UpdateProfile")
        {

            Response<NoContent> response = Response<NoContent>.Fail(
                      statusCode: StatusCodes.Status400BadRequest,
                      isShow: true,
                      path: $"api/User/{action}",
                      errors: result.Errors.Select(x => x.Description).ToArray()
                      );
            _logger.LogResponse(response);
            return response.CreateResponseInstance();
        }
    }
}
