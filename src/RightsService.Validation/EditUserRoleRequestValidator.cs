using System;
using System.Collections.Generic;
using FluentValidation;
using HerzenHelper.RightsService.Broker.Requests.Interfaces;
using HerzenHelper.RightsService.Data.Interfaces;
using HerzenHelper.RightsService.Models.Dto.Requests;
using HerzenHelper.RightsService.Validation.Interfaces;

namespace HerzenHelper.RightsService.Validation
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
