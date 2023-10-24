using System;
using FluentValidation;
using HerzenHelper.Core.Attributes;

namespace HerzenHelper.RightsService.Validation.Interfaces
{
  [AutoInject]
  public interface IEditRoleStatusRequestValidator : IValidator<(Guid roleId, bool isActive)>
  {
  }
}
