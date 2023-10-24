using FluentValidation;
using HerzenHelper.RightsService.Data.Interfaces;
using HerzenHelper.RightsService.Models.Dto.Requests;
using HerzenHelper.RightsService.Validation.Interfaces;

namespace HerzenHelper.RightsService.Validation
{
  public class UpdateRoleRightsRequestValidator : AbstractValidator<UpdateRoleRightsRequest>, IUpdateRoleRightsRequestValidator
  {
    public UpdateRoleRightsRequestValidator(
      IRoleRepository roleRepository,
      IRightsIdsValidator rightsIdsValidator)
    {
      RuleFor(request => request.RoleId)
        .MustAsync(async (roleId, _) => await roleRepository.DoesExistAsync(roleId))
        .WithMessage("Role doesn't exist.");

      RuleFor(request => request.Rights)
        .SetValidator(rightsIdsValidator);
    }
  }
}
