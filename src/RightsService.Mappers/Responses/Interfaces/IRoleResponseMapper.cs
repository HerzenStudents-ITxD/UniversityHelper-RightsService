using System.Collections.Generic;
using HerzenHelper.Core.Attributes;
using HerzenHelper.Models.Broker.Models;
using HerzenHelper.RightsService.Models.Db;
using HerzenHelper.RightsService.Models.Dto.Responses;

namespace HerzenHelper.RightsService.Mappers.Responses.Interfaces
{
  [AutoInject]
  public interface IRoleResponseMapper
  {
    RoleResponse Map(DbRole role, List<DbRightLocalization> rights, List<UserData> users);
  }
}
