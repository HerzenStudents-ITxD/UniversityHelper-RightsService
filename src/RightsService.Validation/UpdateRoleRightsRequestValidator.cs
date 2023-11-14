using FluentValidation;
using UniversityHelper.RightsService.Data.Interfaces;
using UniversityHelper.RightsService.Models.Dto.Requests;
using UniversityHelper.RightsService.Validation.Interfaces;

namespace UniversityHelper.RightsService.Validation;

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
