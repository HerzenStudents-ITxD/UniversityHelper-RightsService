using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;
using UniversityHelper.RightsService.Models.Dto.Models;
using UniversityHelper.RightsService.Models.Dto.Requests.Filters;

namespace UniversityHelper.RightsService.Business.Role.Interfaces
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
