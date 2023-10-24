using System;
using System.Threading.Tasks;
using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.Responses;
using HerzenHelper.RightsService.Models.Dto.Requests;

namespace HerzenHelper.RightsService.Business.Commands.Role.Interfaces
{
  [AutoInject]
  public interface IUpdateRoleRightsCommand
  {
    Task<OperationResultResponse<bool>> ExecuteAsync(UpdateRoleRightsRequest request);
  }
}
