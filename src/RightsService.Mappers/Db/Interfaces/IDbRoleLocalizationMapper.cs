using System;
using UniversityHelper.Core.Attributes;
using UniversityHelper.RightsService.Models.Db;
using UniversityHelper.RightsService.Models.Dto.Requests;

namespace UniversityHelper.RightsService.Mappers.Db.Interfaces
{
  [AutoInject]
  public interface IDbRoleLocalizationMapper
  {
    DbRoleLocalization Map(CreateRoleLocalizationRequest request);
    DbRoleLocalization Map(CreateRoleLocalizationRequest request, Guid roleId);
  }
}
