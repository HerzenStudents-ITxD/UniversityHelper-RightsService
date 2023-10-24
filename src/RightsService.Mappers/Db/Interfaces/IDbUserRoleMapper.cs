using System;
using HerzenHelper.Core.Attributes;
using HerzenHelper.Models.Broker.Publishing.Subscriber.Right;
using HerzenHelper.RightsService.Models.Db;
using HerzenHelper.RightsService.Models.Dto.Requests;

namespace HerzenHelper.RightsService.Mappers.Db.Interfaces
{
  [AutoInject]
  public interface IDbUserRoleMapper
  {
    DbUserRole Map(ICreateUserRolePublish request);

    DbUserRole Map(EditUserRoleRequest request);
  }
}
