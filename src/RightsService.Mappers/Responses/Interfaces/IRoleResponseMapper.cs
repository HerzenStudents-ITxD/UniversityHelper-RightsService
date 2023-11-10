using System.Collections.Generic;
using UniversityHelper.Core.Attributes;
using UniversityHelper.Models.Broker.Models;
using UniversityHelper.Models.Broker.Models.User;
using UniversityHelper.RightsService.Models.Db;
using UniversityHelper.RightsService.Models.Dto.Responses;

namespace UniversityHelper.RightsService.Mappers.Responses.Interfaces
{
  [AutoInject]
  public interface IRoleResponseMapper
  {
    RoleResponse Map(DbRole role, List<DbRightLocalization> rights, List<UserData> users);
  }
}
