using System;
using System.Threading.Tasks;
using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.Responses;
using HerzenHelper.RightsService.Models.Dto.Requests;
using Microsoft.AspNetCore.JsonPatch;

namespace HerzenHelper.RightsService.Business.Commands.RoleLocalization.Interfaces
{
  [AutoInject]
  public interface IEditRoleLocalizationCommand
  {
    Task<OperationResultResponse<bool>> ExecuteAsync(Guid roleLocalizationId, JsonPatchDocument<EditRoleLocalizationRequest> request);
  }
}
