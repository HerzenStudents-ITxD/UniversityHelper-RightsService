using HerzenHelper.RightsService.Mappers.Models.Interfaces;
using HerzenHelper.RightsService.Models.Db;
using HerzenHelper.RightsService.Models.Dto.Models;

namespace HerzenHelper.RightsService.Mappers.Models
{
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
}
