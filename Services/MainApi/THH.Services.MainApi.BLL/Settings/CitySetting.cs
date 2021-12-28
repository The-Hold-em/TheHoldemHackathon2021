#nullable disable
namespace THH.Services.MainApi.BLL.Settings;

public class CitySetting
{
    public IEnumerable<CityModel> Cities { get; set; }
}

public class CityModel
{
    public string Name { get; set; }
    public int Plate { get; set; }
    public string[] Districts { get; set; }
};