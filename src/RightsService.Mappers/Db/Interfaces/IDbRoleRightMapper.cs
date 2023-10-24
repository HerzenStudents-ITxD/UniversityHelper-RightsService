using System;
using System.Collections.Generic;
using HerzenHelper.Core.Attributes;
using HerzenHelper.RightsService.Models.Db;

namespace HerzenHelper.RightsService.Mappers.Db.Interfaces
{
  [AutoInject]
  public interface IDbRoleRightMapper
  {
    List<DbRoleRight> Map(Guid roleId, List<int> rightsIds);
  }
}
