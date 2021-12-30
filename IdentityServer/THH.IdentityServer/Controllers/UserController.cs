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
using THH.Shared.Core.StringInfo;

using static Duende.IdentityServer.IdentityServerConstants;
using static Mernis.KPSPublicSoapClient;

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
            model.UserName = model.IdentityNumber;
            ApplicationUser user = _mapper.Map<ApplicationUser>(model);

            using var service = new Mernis.KPSPublicSoapClient(EndpointConfiguration.KPSPublicSoap);
            Mernis.TCKimlikNoDogrulaResponse status = await service.TCKimlikNoDogrulaAsync(long.Parse(model.IdentityNumber), model.FirstName.ToUpper(), model.LastName.ToUpper(), model.DateOfBirth.Year);
            if (!status.Body.TCKimlikNoDogrulaResult)
                return Response<NoContent>.Fail(
                      statusCode: StatusCodes.Status400BadRequest,
                      isShow: true,
                      path: $"api/User/Mernis",
                      errors: "Girdiğiniz bilgilerle eşleşen TC. vatandaşlığı bulunamadı"
                      ).CreateResponseInstance();


            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return GetResult(result, "SignUp");

            var roleResult = await _userManager.AddToRoleAsync(user, RoleInfo.User);

            if (!roleResult.Succeeded)
                return GetResult(roleResult, "SignUp_AddRole");

            return Response<NoContent>.Success(statusCode: StatusCodes.Status201Created).CreateResponseInstance();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateModel dto)
        {
            ApplicationUser updateModel = _mapper.Map<ApplicationUser>(dto);
            ApplicationUser user = await _userManager.FindByIdAsync(updateModel.Id);
            if (user.IsNull()) return Response<NoContent>.Fail(
              statusCode: StatusCodes.Status400BadRequest,
              isShow: true,
              path: "api/role/put",
              errors: "Gecerli bir kullanici bulunamadi"
              ).CreateResponseInstance();


            string token = await _userManager.GeneratePasswordResetTokenAsync(user);

            IdentityResult passwordRes = await _userManager.ResetPasswordAsync(user, token, dto.Password);

            if (!passwordRes.Succeeded) return GetResult(passwordRes, "UpdatePassword");

            user.FirstName = updateModel.FirstName;
            user.LastName = updateModel.LastName;
            user.IdentityNumber = updateModel.IdentityNumber;
            user.UserName = updateModel.IdentityNumber;
            user.DateOfBirth = updateModel.DateOfBirth;
            IdentityResult updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                return GetResult(updateResult, "UpdateUser");
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

        [HttpPut]
        public async Task<IActionResult> Vote()
        {
            Claim userClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);

            if (userClaim.IsNull()) return Response<NoContent>.Fail(
                 statusCode: StatusCodes.Status400BadRequest,
                 isShow: true,
                 path: "api/User/Vote",
                 errors: "Kullanici girisi dogrulanamadi"
                 ).CreateResponseInstance();

            ApplicationUser user = await _userManager.FindByIdAsync(userClaim.Value);
            if (user.IsNull()) return Response<NoContent>.Fail(
              statusCode: StatusCodes.Status400BadRequest,
              isShow: true,
              path: "api/role/put",
              errors: "Gecerli bir kullanici bulunamadi"
              ).CreateResponseInstance();

            user.IVoted = true;

            IdentityResult updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                return GetResult(updateResult, "UpdateUser");

            return Response<NoContent>.Success().CreateResponseInstance();
        }

        [HttpGet]
        public async Task<IActionResult> IsVote()
        {
            Claim userClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);

            if (userClaim.IsNull()) return Response<NoContent>.Fail(
                 statusCode: StatusCodes.Status400BadRequest,
                 isShow: true,
                 path: "api/User/Vote",
                 errors: "Kullanici girisi dogrulanamadi"
                 ).CreateResponseInstance();

            ApplicationUser user = await _userManager.FindByIdAsync(userClaim.Value);
            if (user.IsNull()) return Response<NoContent>.Fail(
             statusCode: StatusCodes.Status400BadRequest,
             isShow: true,
             path: "api/role/put",
             errors: "Gecerli bir kullanici bulunamadi"
             ).CreateResponseInstance();

            return Response<bool>.Success(user.IVoted).CreateResponseInstance();

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
