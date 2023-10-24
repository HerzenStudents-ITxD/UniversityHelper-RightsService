using HerzenHelper.RightsService.Mappers.Models.Interfaces;
using HerzenHelper.RightsService.Models.Db;
using HerzenHelper.RightsService.Models.Dto.Models;

namespace HerzenHelper.RightsService.Mappers.Models
{
  public class RoleLocalizationInfoMapper : IRoleLocalizationInfoMapper
  {
    public RoleLocalizationInfo Map(DbRoleLocalization dbRoleLocalization)
    {
      if (dbRoleLocalization == null)
      {
        return null;
      }

      return new RoleLocalizationInfo
      {
        Id = dbRoleLocalization.Id,
        Locale = dbRoleLocalization.Locale,
        Name = dbRoleLocalization.Name,
        Description = dbRoleLocalization.Description,
        IsActive = dbRoleLocalization.IsActive,
        RoleId = dbRoleLocalization.RoleId
      };
    }
  }
}
