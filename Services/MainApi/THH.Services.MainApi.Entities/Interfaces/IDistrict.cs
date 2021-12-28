﻿using THH.Core.Interfaces;
using THH.Services.MainApi.Entities.Concrete;

namespace THH.Services.MainApi.Entities.Interfaces;

public interface IDistrict:IEntityBase
{
     string Name { get; set; }

     City City { get; set; }
     Guid CityId { get; set; }
}
