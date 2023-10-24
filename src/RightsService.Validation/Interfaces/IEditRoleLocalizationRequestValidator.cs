using System;
using FluentValidation;
using HerzenHelper.Core.Attributes;
using HerzenHelper.RightsService.Models.Dto.Requests;
using Microsoft.AspNetCore.JsonPatch;

namespace HerzenHelper.RightsService.Validation.Interfaces
{
  [AutoInject]
  public interface IEditRoleLocalizationRequestValidator : IValidator<(Guid, JsonPatchDocument<EditRoleLocalizationRequest>)>
  {
  }
}
