using System.Threading.Tasks;
using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.Responses;
using HerzenHelper.RightsService.Models.Dto.Models;
using HerzenHelper.RightsService.Models.Dto.Requests.Filters;

namespace HerzenHelper.RightsService.Business.Role.Interfaces
{
  /// <summary>
  /// Represents interface for a command in command pattern.
  /// Provides method for getting list of role models with pagination.
  /// </summary>
  [AutoInject]
    public interface IFindRolesCommand
    {
        /// <summary>
        /// Returns the list of role models using pagination.
        /// </summary>
        Task<FindResultResponse<RoleInfo>> ExecuteAsync(FindRolesFilter filter);
    }
}
