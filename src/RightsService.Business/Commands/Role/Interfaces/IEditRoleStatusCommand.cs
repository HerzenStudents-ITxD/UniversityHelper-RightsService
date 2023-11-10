using System;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;

namespace UniversityHelper.RightsService.Business.Commands.Role.Interfaces
{
  [AutoInject]
  public interface IEditRoleStatusCommand
  {
    Task<OperationResultResponse<bool>> ExecuteAsync(Guid roleId, bool isActive);
  }
}
