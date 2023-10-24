using FluentValidation;
using HerzenHelper.Core.Attributes;
using HerzenHelper.RightsService.Models.Dto.Requests;

namespace HerzenHelper.RightsService.Validation.Interfaces
{
  [AutoInject]
  public interface ICreateRoleLocalizationRequestValidator : IValidator<CreateRoleLocalizationRequest>
  {
  }
}
