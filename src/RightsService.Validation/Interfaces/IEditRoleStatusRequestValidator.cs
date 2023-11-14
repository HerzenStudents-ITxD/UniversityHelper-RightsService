using System;
using FluentValidation;
using UniversityHelper.Core.Attributes;

namespace UniversityHelper.RightsService.Validation.Interfaces;

[AutoInject]
public interface IEditRoleStatusRequestValidator : IValidator<(Guid roleId, bool isActive)>
{
}
