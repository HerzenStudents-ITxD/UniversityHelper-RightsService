using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.Responses;
using HerzenHelper.RightsService.Models.Dto.Requests;
using System;
using System.Threading.Tasks;

namespace HerzenHelper.RightsService.Business.Role.Interfaces
{
  /// <summary>
  /// Represents interface for a command in command pattern.
  /// Provides method for creating a new role.
  /// </summary>
  [AutoInject]
    public interface ICreateRoleCommand
    {
        /// <summary>
        /// Create a new role. Returns true if it succeeded to create a role, otherwise false.
        /// </summary>
        Task<OperationResultResponse<Guid>> ExecuteAsync(CreateRoleRequest request);
    }
}
