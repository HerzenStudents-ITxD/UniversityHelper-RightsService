﻿using System;
using System.Threading.Tasks;
using UniversityHelper.Core.Extensions;
using UniversityHelper.RightsService.Data.Interfaces;
using UniversityHelper.RightsService.Data.Provider;
using UniversityHelper.RightsService.Models.Db;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace UniversityHelper.RightsService.Data;

public class RoleLocalizationRepository : IRoleLocalizationRepository
{
  private readonly IDataProvider _provider;
  private readonly IHttpContextAccessor _httpContextAccessor;

  public RoleLocalizationRepository(
    IDataProvider provider,
    IHttpContextAccessor httpContextAccessor)
  {
    _provider = provider;
    _httpContextAccessor = httpContextAccessor;
  }

  public async Task<Guid?> CreateAsync(DbRoleLocalization roleLocalization)
  {
    if (roleLocalization is null)
    {
      return null;
    }

    _provider.RolesLocalizations.Add(roleLocalization);
    await _provider.SaveAsync();

    return roleLocalization.Id;
  }

  public async Task<bool> DoesNameExistAsync(string locale, string name, Guid id = default)
  {
    return await _provider.RolesLocalizations.AnyAsync(rl => rl.Id != id && rl.IsActive && rl.Locale == locale && rl.Name == name);
  }

  public async Task<bool> DoesLocaleExistAsync(Guid roleId, string locale)
  {
    return await _provider.RolesLocalizations.AnyAsync(rl => rl.IsActive && rl.RoleId == roleId && rl.Locale == locale);
  }

  public async Task<DbRoleLocalization> GetAsync(Guid roleLocalizationId)
  {
    return await _provider.RolesLocalizations.FirstOrDefaultAsync(x => x.Id == roleLocalizationId);
  }

  public async Task<bool> EditRoleLocalizationAsync(Guid roleLocalizationId, JsonPatchDocument<DbRoleLocalization> patch)
  {
    if (patch == null)
    {
      return false;
    }

    DbRoleLocalization roleLocalization = await _provider.RolesLocalizations.FirstOrDefaultAsync(x => x.Id == roleLocalizationId);

    if (roleLocalization == default)
    {
      return false;
    }

    patch.ApplyTo(roleLocalization);
    roleLocalization.ModifiedBy = _httpContextAccessor.HttpContext.GetUserId();
    roleLocalization.ModifiedAtUtc = DateTime.UtcNow;

    await _provider.SaveAsync();

    return true;
  }
}
