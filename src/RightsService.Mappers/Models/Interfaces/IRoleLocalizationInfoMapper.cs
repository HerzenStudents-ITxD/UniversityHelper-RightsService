using UniversityHelper.Core.Attributes;
using UniversityHelper.RightsService.Models.Db;
using UniversityHelper.RightsService.Models.Dto.Models;

namespace UniversityHelper.RightsService.Mappers.Models.Interfaces
{
  [AutoInject]
  public interface IRoleLocalizationInfoMapper
  {
    RoleLocalizationInfo Map(DbRoleLocalization dbRoleLocalization);
  }
}
