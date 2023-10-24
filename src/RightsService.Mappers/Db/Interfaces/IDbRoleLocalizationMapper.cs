using System;
using HerzenHelper.Core.Attributes;
using HerzenHelper.RightsService.Models.Db;
using HerzenHelper.RightsService.Models.Dto.Requests;

namespace HerzenHelper.RightsService.Mappers.Db.Interfaces
{
  [AutoInject]
  public interface IDbRoleLocalizationMapper
  {
    DbRoleLocalization Map(CreateRoleLocalizationRequest request);
    DbRoleLocalization Map(CreateRoleLocalizationRequest request, Guid roleId);
  }
}
