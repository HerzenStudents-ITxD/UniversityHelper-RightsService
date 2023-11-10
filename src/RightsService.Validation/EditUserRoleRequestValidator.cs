using System;
using System.Collections.Generic;
using FluentValidation;
using UniversityHelper.RightsService.Broker.Requests.Interfaces;
using UniversityHelper.RightsService.Data.Interfaces;
using UniversityHelper.RightsService.Models.Dto.Requests;
using UniversityHelper.RightsService.Validation.Interfaces;

namespace UniversityHelper.RightsService.Validation
{
  public class EditUserRoleRequestValidator : AbstractValidator<EditUserRoleRequest>, IEditUserRoleRequestValidator
  {
    public EditUserRoleRequestValidator(
      IRoleRepository repository,
      IUserService userService)
    {
      RuleFor(x => x.UserId)
        .Cascade(CascadeMode.Stop)
        .Must(x => x != Guid.Empty)
        .WithMessage("No user id provided.")
        .MustAsync(async (request, _) => (await userService.CheckUsersExistence(new List<Guid> { request }, new List<string>())).Count == 1)
        .WithMessage("User does not exist.");

      When(request =>
          request.RoleId.HasValue,
        () =>
          RuleFor(request => request.RoleId.Value)
            .MustAsync(async (id, _) => await repository.DoesExistAsync(id))
            .WithMessage("Role must exist."));
    }
  }
}
