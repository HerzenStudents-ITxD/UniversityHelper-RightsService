using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Responses;
using UniversityHelper.RightsService.Business.Commands.Right.Interfaces;
using UniversityHelper.RightsService.Models.Dto.Models;
using Microsoft.AspNetCore.Mvc;

namespace UniversityHelper.RightsService.Controllers
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
