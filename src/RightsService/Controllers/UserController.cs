using System;
using System.Threading.Tasks;
using HerzenHelper.Core.Responses;
using HerzenHelper.RightsService.Business.Commands.User.Interfaces;
using HerzenHelper.RightsService.Models.Dto.Requests;
using Microsoft.AspNetCore.Mvc;

namespace HerzenHelper.RightsService.Controllers
{
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
}
