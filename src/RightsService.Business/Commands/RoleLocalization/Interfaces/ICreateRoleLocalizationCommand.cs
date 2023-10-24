using System;
using System.Threading.Tasks;
using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.Responses;
using HerzenHelper.RightsService.Models.Dto.Requests;

namespace HerzenHelper.RightsService.Business.Commands.RoleLocalization.Interfaces
{
  [AutoInject]
  public interface ICreateRoleLocalizationCommand
  {
    Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateRoleLocalizationRequest request);
  }
}
