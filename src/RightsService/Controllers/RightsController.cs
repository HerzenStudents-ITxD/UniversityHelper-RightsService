using System.Collections.Generic;
using System.Threading.Tasks;
using HerzenHelper.Core.Responses;
using HerzenHelper.RightsService.Business.Commands.Right.Interfaces;
using HerzenHelper.RightsService.Models.Dto.Models;
using Microsoft.AspNetCore.Mvc;

namespace HerzenHelper.RightsService.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class RightsController : ControllerBase
  {
    [HttpGet("get")]
    public async Task<OperationResultResponse<List<RightInfo>>> Get(
      [FromQuery] string locale,
      [FromServices] IGetRightsListCommand command)
    {
      return await command.ExecuteAsync(locale);
    }
  }
}
