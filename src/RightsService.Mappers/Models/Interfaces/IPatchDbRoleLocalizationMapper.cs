using HerzenHelper.Core.Attributes;
using HerzenHelper.RightsService.Models.Db;
using HerzenHelper.RightsService.Models.Dto.Requests;
using Microsoft.AspNetCore.JsonPatch;

namespace HerzenHelper.RightsService.Mappers.Models.Interfaces
{
  [AutoInject]
  public interface IPatchDbRoleLocalizationMapper
  {
    JsonPatchDocument<DbRoleLocalization> Map(JsonPatchDocument<EditRoleLocalizationRequest> request);
  }
}
