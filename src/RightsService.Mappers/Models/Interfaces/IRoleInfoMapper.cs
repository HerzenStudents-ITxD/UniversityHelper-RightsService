using UniversityHelper.Core.Attributes;
using UniversityHelper.RightsService.Models.Db;
using UniversityHelper.RightsService.Models.Dto.Models;
using System.Collections.Generic;

namespace UniversityHelper.RightsService.Mappers.Models.Interfaces
{
  [AutoInject]
  public interface IRoleInfoMapper
  {
    RoleInfo Map(
      DbRole dbRole,
      List<RightInfo> rights,
      List<UserInfo> userInfos);
  }
}
