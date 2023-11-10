using System;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;
using UniversityHelper.RightsService.Models.Dto.Requests;
using Microsoft.AspNetCore.JsonPatch;

namespace UniversityHelper.RightsService.Business.Commands.RoleLocalization.Interfaces
{
  [AutoInject]
  public interface IEditRoleLocalizationCommand
  {
    Task<OperationResultResponse<bool>> ExecuteAsync(Guid roleLocalizationId, JsonPatchDocument<EditRoleLocalizationRequest> request);
  }
}
