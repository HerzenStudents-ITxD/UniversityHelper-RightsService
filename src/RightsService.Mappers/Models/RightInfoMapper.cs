using UniversityHelper.RightsService.Mappers.Models.Interfaces;
using UniversityHelper.RightsService.Models.Db;
using UniversityHelper.RightsService.Models.Dto.Models;

namespace UniversityHelper.RightsService.Mappers.Models;

public class RightInfoMapper : IRightInfoMapper
{
  public RightInfo Map(DbRightLocalization value)
  {
    if (value == null)
    {
      return null;
    }

    return new RightInfo
    {
      RightId = value.RightId,
      Locale = value.Locale,
      Name = value.Name,
      Description = value.Description
    };
  }
}
