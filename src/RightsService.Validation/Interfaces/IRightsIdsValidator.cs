using FluentValidation;
using UniversityHelper.Core.Attributes;
using System.Collections.Generic;

namespace UniversityHelper.RightsService.Validation.Interfaces;

[AutoInject]
public interface IRightsIdsValidator : IValidator<IEnumerable<int>>
{
}
