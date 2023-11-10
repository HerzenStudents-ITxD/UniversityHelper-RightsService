using System;
using UniversityHelper.Core.Attributes;
using UniversityHelper.Models.Broker.Publishing.Subscriber.Right;
using UniversityHelper.RightsService.Models.Db;
using UniversityHelper.RightsService.Models.Dto.Requests;

namespace UniversityHelper.RightsService.Mappers.Db.Interfaces
{
  [AutoInject]
  public interface IDbUserRoleMapper
  {
    DbUserRole Map(ICreateUserRolePublish request);

    DbUserRole Map(EditUserRoleRequest request);
  }
}
