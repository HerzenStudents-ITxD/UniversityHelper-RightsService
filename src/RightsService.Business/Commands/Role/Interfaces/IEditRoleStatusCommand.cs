using System;
using System.Threading.Tasks;
using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.Responses;

namespace HerzenHelper.RightsService.Business.Commands.Role.Interfaces
{
  [AutoInject]
  public interface IEditRoleStatusCommand
  {
    Task<OperationResultResponse<bool>> ExecuteAsync(Guid roleId, bool isActive);
  }
}
