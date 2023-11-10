using UniversityHelper.Core.Attributes;
using UniversityHelper.RightsService.Models.Db;
using UniversityHelper.RightsService.Models.Dto.Requests;
using Microsoft.AspNetCore.JsonPatch;

namespace UniversityHelper.RightsService.Mappers.Models.Interfaces
{
  [AutoInject]
  public interface IPatchDbRoleLocalizationMapper
  {
    JsonPatchDocument<DbRoleLocalization> Map(JsonPatchDocument<EditRoleLocalizationRequest> request);
  }
}
