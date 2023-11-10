using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;
using UniversityHelper.RightsService.Models.Dto.Requests.Filters;
using UniversityHelper.RightsService.Models.Dto.Responses;
using System.Threading.Tasks;

namespace UniversityHelper.RightsService.Business.Role.Interfaces
{
  [AutoInject]
    public interface IGetRoleCommand
    {
      Task<OperationResultResponse<RoleResponse>> ExecuteAsync(GetRoleFilter filter);
    }
}
