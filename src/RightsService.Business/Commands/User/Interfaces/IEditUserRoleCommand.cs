using System.Threading.Tasks;
using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.Responses;
using HerzenHelper.RightsService.Models.Dto.Requests;

namespace HerzenHelper.RightsService.Business.Commands.User.Interfaces
{
  [AutoInject]
  public interface IEditUserRoleCommand
  {
    Task<OperationResultResponse<bool>> ExecuteAsync(EditUserRoleRequest request);
  }
}
