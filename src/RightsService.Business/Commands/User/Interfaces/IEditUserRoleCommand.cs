using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;
using UniversityHelper.RightsService.Models.Dto.Requests;

namespace UniversityHelper.RightsService.Business.Commands.User.Interfaces;

[AutoInject]
public interface IEditUserRoleCommand
{
  Task<OperationResultResponse<bool>> ExecuteAsync(EditUserRoleRequest request);
}
