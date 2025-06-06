﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Validators;
using UniversityHelper.Core.Validators;
using UniversityHelper.RightsService.Data.Interfaces;
using UniversityHelper.RightsService.Models.Db;
using UniversityHelper.RightsService.Models.Dto.Requests;
using UniversityHelper.RightsService.Validation.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;

namespace UniversityHelper.RightsService.Validation;

public class EditRoleLocalizationRequestValidator : ExtendedEditRequestValidator<Guid, EditRoleLocalizationRequest>, IEditRoleLocalizationRequestValidator
{
  private readonly IRoleLocalizationRepository _roleLocalizationRepository;

  private async Task RequestValidation(
    (Guid id, JsonPatchDocument<EditRoleLocalizationRequest> patch) tuple,
    ValidationContext<(Guid, JsonPatchDocument<EditRoleLocalizationRequest>)> context,
    CancellationToken _)
  {
    DbRoleLocalization roleLocalization = await _roleLocalizationRepository.GetAsync(tuple.id);

    if (roleLocalization is null)
    {
      context.AddFailure("roleLocalizationId", "Can't find role's localization with this ID.");
    }
    else
    {
      foreach (var operation in tuple.patch.Operations)
      {
        await HandleInternalPropertyValidation(roleLocalization, operation, context);
      }
    }
  }

  private async Task HandleInternalPropertyValidation(
    DbRoleLocalization roleLocalization,
    Operation<EditRoleLocalizationRequest> operation,
    ValidationContext<(Guid, JsonPatchDocument<EditRoleLocalizationRequest>)> context)
  {
    Context = context;
    RequestedOperation = operation;

    AddCorrectPaths(
      new List<string>
      {
        nameof(EditRoleLocalizationRequest.Name),
        nameof(EditRoleLocalizationRequest.Description),
        nameof(EditRoleLocalizationRequest.IsActive)
      });

    AddCorrectOperations(nameof(EditRoleLocalizationRequest.Name), new List<OperationType> { OperationType.Replace });
    AddCorrectOperations(nameof(EditRoleLocalizationRequest.Description), new List<OperationType> { OperationType.Replace });
    AddCorrectOperations(nameof(EditRoleLocalizationRequest.IsActive), new List<OperationType> { OperationType.Replace });

    //await AddFailureForPropertyIfNot(
    //  nameof(EditRoleLocalizationRequest.Name),
    //  x => x == OperationType.Replace,
    //  new Dictionary<Func<Operation<EditRoleLocalizationRequest>, Task<bool>>, string>
    //  {
    //    {
    //      x => Task.FromResult(!string.IsNullOrEmpty(x.value?.ToString())), "Name can't be empty."
    //    },
    //    {
    //      x => Task.FromResult(x.value.ToString().Trim().Length < 101), "Name is too long."
    //    },
    //    {
    //      async x => !await _roleLocalizationRepository.DoesNameExistAsync(roleLocalization.Locale, x.value.ToString().Trim(), roleLocalization.Id),
    //      "Name already exists."
    //    }
    //  },
    //  CascadeMode.Stop);

    //await AddFailureForPropertyIfNot(
    //  nameof(EditRoleLocalizationRequest.IsActive),
    //  x => x == OperationType.Replace,
    //  new Dictionary<Func<Operation<EditRoleLocalizationRequest>, Task<bool>>, string>
    //  {
    //    {
    //      x => Task.FromResult(bool.TryParse(x.value?.ToString(), out _)), "Incorrect isActive format."
    //    },
    //    {
    //      async x => roleLocalization.IsActive
    //      || !bool.Parse(x.value.ToString())
    //      || !await _roleLocalizationRepository.DoesLocaleExistAsync(roleLocalization.RoleId, roleLocalization.Locale),
    //      "Role must have only one localization per locale."
    //    }
    //  },
    //  CascadeMode.Stop);
  }

  public EditRoleLocalizationRequestValidator(
    IRoleLocalizationRepository roleLocalizationRepository)
  {
    _roleLocalizationRepository = roleLocalizationRepository;

    RuleFor(x => x)
      .CustomAsync(RequestValidation);
  }
}
