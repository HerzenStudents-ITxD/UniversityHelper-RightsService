using System;
using UniversityHelper.Core.Extensions;
using UniversityHelper.RightsService.Mappers.Db.Interfaces;
using UniversityHelper.RightsService.Models.Db;
using UniversityHelper.RightsService.Models.Dto.Requests;
using Microsoft.AspNetCore.Http;

namespace UniversityHelper.RightsService.Mappers.Db;

public class DbRoleLocalizationMapper : IDbRoleLocalizationMapper
{
  private IHttpContextAccessor _httpContextAccessor;

  public DbRoleLocalizationMapper(
    IHttpContextAccessor httpContextAccessor)
  {
    _httpContextAccessor = httpContextAccessor;
  }

  public DbRoleLocalization Map(CreateRoleLocalizationRequest request)
  {
    if (request == null)
    {
      return null;
    }

    return new DbRoleLocalization
    {
      Id = Guid.NewGuid(),
      RoleId = request.RoleId.Value,
      Locale = request.Locale,
      Name = request.Name.Trim(),
      Description = string.IsNullOrEmpty(request.Description?.Trim()) ? null : request.Description?.Trim(),
      CreatedBy = _httpContextAccessor.HttpContext.GetUserId(),
      CreatedAtUtc = DateTime.UtcNow,
      IsActive = true
    };
  }

  public DbRoleLocalization Map(CreateRoleLocalizationRequest request, Guid roleId)
  {
    if (request == null)
    {
      return null;
    }

    return new DbRoleLocalization
    {
      Id = Guid.NewGuid(),
      RoleId = roleId,
      Locale = request.Locale,
      Name = request.Name,
      Description = string.IsNullOrEmpty(request.Description?.Trim()) ? null : request.Description?.Trim(),
      CreatedBy = _httpContextAccessor.HttpContext.GetUserId(),
      CreatedAtUtc = DateTime.UtcNow,
      IsActive = true
    };
  }
}
