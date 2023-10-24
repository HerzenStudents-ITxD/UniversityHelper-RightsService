using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.Responses;
using HerzenHelper.RightsService.Models.Dto.Requests.Filters;
using HerzenHelper.RightsService.Models.Dto.Responses;
using System.Threading.Tasks;

namespace HerzenHelper.RightsService.Business.Role.Interfaces
{
  [AutoInject]
    public interface IGetRoleCommand
    {
      Task<OperationResultResponse<RoleResponse>> ExecuteAsync(GetRoleFilter filter);
    }
}
