using Microsoft.Extensions.Configuration;

using THH.Services.MainApi.BLL.Settings;
using THH.Services.MainApi.DAL.Concrete.EntityFrameworkCore.Contexts;
using THH.Services.MainApi.Entities.Concrete;
using THH.Shared.Core.StringInfo;
using THH.Shared.DAL.Interfaces;

namespace THH.Services.MainApi.BLL.Seeding;

public class DefaultsSeeder
{
    private readonly ApplicationDbContext _context;
    private readonly IGenericCommandRepository<City> _cityRepository;
    private readonly IGenericCommandRepository<Candidate> _candıdateRepository;
    private readonly DefaultRecords _defaultRecords;
    public DefaultsSeeder(
        ApplicationDbContext context,
        IGenericCommandRepository<City> cityRepository, 
        IGenericCommandRepository<Candidate> candıdateRepository,
        DefaultRecords defaultRecords)
    {
        _context = context;
        _cityRepository = cityRepository;
        _candıdateRepository = candıdateRepository;
        _defaultRecords = defaultRecords;
    }
    public async Task SeedAsync()
    {
        if (!_context.Cities.Any())
        {
            var cities = _defaultRecords.GetCities();
            await _cityRepository.AddRangeAsync(cities);
            await _cityRepository.SaveChangesAsync();
        }

        if (!_context.Candidates.Any())
        {
            var candidates = _defaultRecords.GetCandidates();
            await _candıdateRepository.AddRangeAsync(candidates);
            await _candıdateRepository.SaveChangesAsync();
        }
    }
}
