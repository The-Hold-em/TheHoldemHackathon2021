using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using THH.Services.MainApi.BLL.Settings;
using THH.Services.MainApi.Entities.Concrete;
using THH.Shared.Core.StringInfo;

namespace THH.Services.MainApi.BLL.Seeding;

public class DefaultRecords
{
    private readonly CitySetting _citySetting;
    private readonly Guid _systemUserId;

    public DefaultRecords(CitySetting citySetting)
    {
        _citySetting = citySetting;
        _systemUserId = Guid.Parse(SystemUserInfo.SystemUserId);
    }

    public List<City> GetCities() => _citySetting.Cities.Select(c => new City()
    {
        CreatedUserId = _systemUserId,
        Name = c.Name,
        Plate = c.Plate,
        Districts = c.Districts.Select(x => new District() { Name = x, CreatedUserId = _systemUserId, CreatedTime = DateTime.Now }).ToList()
    }).ToList();

    public List<Candidate> GetCandidates() => new()
        {
             new ()
            {
                Name = "Ömer",
                Party = "Mars",
            },
             new ()
            {
                Name = "Hikmet",
                Party = "Venüs",
            },
             new ()
            {
                Name = "Recep",
                Party = "Jupiter",
            },
             new ()
            {
                Name = "İrfan",
                Party = "Satürn",
            },
             new ()
            {
                Name = "Yusuf",
                Party = "Merkür",
            }
         };
}
