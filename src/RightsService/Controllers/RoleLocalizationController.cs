using System;
using System.Threading.Tasks;
using UniversityHelper.Core.Responses;
using UniversityHelper.RightsService.Business.Commands.RoleLocalization.Interfaces;
using UniversityHelper.RightsService.Models.Dto.Requests;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace UniversityHelper.RightsService.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class RoleLocalizationController
  {
    [HttpPost("create")]
    public async Task<OperationResultResponse<Guid?>> CreateAsync(
      [FromServices] ICreateRoleLocalizationCommand command,
      [FromBody] CreateRoleLocalizationRequest request)
    {
      return await command.ExecuteAsync(request);
    }

    [HttpPatch("edit")]
    public async Task<OperationResultResponse<bool>> EditAsync(
      [FromServices] IEditRoleLocalizationCommand command,
      [FromQuery] Guid roleLocalizationId,
      [FromBody] JsonPatchDocument<EditRoleLocalizationRequest> request)
    {
      return await command.ExecuteAsync(roleLocalizationId, request);
    }
  }
}
