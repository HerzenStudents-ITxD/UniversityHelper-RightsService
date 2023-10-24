using FluentValidation;
using HerzenHelper.Core.Attributes;
using System.Collections.Generic;

namespace HerzenHelper.RightsService.Validation.Interfaces
{
    [AutoInject]
    public interface IRightsIdsValidator : IValidator<IEnumerable<int>>
    {
    }
}
