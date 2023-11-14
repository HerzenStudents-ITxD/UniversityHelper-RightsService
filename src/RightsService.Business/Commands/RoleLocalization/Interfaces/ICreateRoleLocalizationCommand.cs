using System;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;
using UniversityHelper.RightsService.Models.Dto.Requests;

namespace UniversityHelper.RightsService.Business.Commands.RoleLocalization.Interfaces;

[AutoInject]
public interface ICreateRoleLocalizationCommand
{
  Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateRoleLocalizationRequest request);
}
