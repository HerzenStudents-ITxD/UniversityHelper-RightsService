using System;
using System.Threading.Tasks;
using HerzenHelper.Core.Responses;
using HerzenHelper.RightsService.Business.Commands.Role.Interfaces;
using HerzenHelper.RightsService.Business.Role.Interfaces;
using HerzenHelper.RightsService.Models.Dto.Models;
using HerzenHelper.RightsService.Models.Dto.Requests;
using HerzenHelper.RightsService.Models.Dto.Requests.Filters;
using HerzenHelper.RightsService.Models.Dto.Responses;
using Microsoft.AspNetCore.Mvc;

namespace HerzenHelper.RightsService.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class RolesController : ControllerBase
  {
    [HttpGet("find")]
    public async Task<FindResultResponse<RoleInfo>> FindAsync(
      [FromServices] IFindRolesCommand command,
      [FromQuery] FindRolesFilter filter)
    {
      return await command.ExecuteAsync(filter);
    }

    [HttpPost("create")]
    public async Task<OperationResultResponse<Guid>> Create(
      [FromServices] ICreateRoleCommand command,
      [FromBody] CreateRoleRequest role)
    {
      return await command.ExecuteAsync(role);
    }

    [HttpGet("get")]
    public async Task<OperationResultResponse<RoleResponse>> GetAsync(
      [FromServices] IGetRoleCommand command,
      [FromQuery] GetRoleFilter filter)
    {
      return await command.ExecuteAsync(filter);
    }

    [HttpPut("editstatus")]
    public async Task<OperationResultResponse<bool>> ChangeRoleStatusAsync(
      [FromServices] IEditRoleStatusCommand command,
      [FromQuery] Guid roleId,
      [FromQuery] bool isActive)
    {
      return await command.ExecuteAsync(roleId, isActive);
    }

    [HttpPost("updaterightsset")]
    public async Task<OperationResultResponse<bool>> EditRoleRightsAsync(
      [FromServices] IUpdateRoleRightsCommand command,
      [FromBody] UpdateRoleRightsRequest request)
    {
      return await command.ExecuteAsync(request);
    }
  }
}
