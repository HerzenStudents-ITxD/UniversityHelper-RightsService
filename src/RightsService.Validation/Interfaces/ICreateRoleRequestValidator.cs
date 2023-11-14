using FluentValidation;
using UniversityHelper.Core.Attributes;
using UniversityHelper.RightsService.Models.Dto.Requests;

namespace UniversityHelper.RightsService.Validation.Interfaces;

[AutoInject]
public interface ICreateRoleRequestValidator : IValidator<CreateRoleRequest>
{
}
