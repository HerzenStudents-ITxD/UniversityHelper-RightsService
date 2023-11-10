using System;
using FluentValidation;
using UniversityHelper.Core.Attributes;
using UniversityHelper.RightsService.Models.Dto.Requests;
using Microsoft.AspNetCore.JsonPatch;

namespace UniversityHelper.RightsService.Validation.Interfaces
{
  [AutoInject]
  public interface IEditRoleLocalizationRequestValidator : IValidator<(Guid, JsonPatchDocument<EditRoleLocalizationRequest>)>
  {
  }
}
