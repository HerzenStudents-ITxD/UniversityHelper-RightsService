using System;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;
using UniversityHelper.RightsService.Models.Dto.Requests;

namespace UniversityHelper.RightsService.Business.Commands.Role.Interfaces;

[AutoInject]
public interface IUpdateRoleRightsCommand
{
  Task<OperationResultResponse<bool>> ExecuteAsync(UpdateRoleRightsRequest request);
}
