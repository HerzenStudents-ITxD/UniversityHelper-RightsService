using HerzenHelper.Core.Attributes;
using HerzenHelper.RightsService.Models.Db;
using HerzenHelper.RightsService.Models.Dto.Models;
using System.Collections.Generic;

namespace HerzenHelper.RightsService.Mappers.Models.Interfaces
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
