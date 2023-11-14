using System;
using System.Threading.Tasks;
using UniversityHelper.Core.Responses;
using UniversityHelper.RightsService.Business.Commands.User.Interfaces;
using UniversityHelper.RightsService.Models.Dto.Requests;
using Microsoft.AspNetCore.Mvc;

namespace UniversityHelper.RightsService.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
  [HttpPut("edit")]
  public async Task<OperationResultResponse<bool>> EditAsync(
    [FromServices] IEditUserRoleCommand command,
    [FromBody] EditUserRoleRequest request)
  {
    return await command.ExecuteAsync(request);
  }
}
