using System;
using System.Collections.Generic;
using UniversityHelper.Core.Attributes;
using UniversityHelper.RightsService.Models.Db;

namespace UniversityHelper.RightsService.Mappers.Db.Interfaces;

[AutoInject]
public interface IDbRoleRightMapper
{
  List<DbRoleRight> Map(Guid roleId, List<int> rightsIds);
}
