﻿using System;
using System.Collections.Generic;
using System.Linq;
using UniversityHelper.Core.Extensions;
using UniversityHelper.RightsService.Mappers.Db.Interfaces;
using UniversityHelper.RightsService.Models.Db;
using Microsoft.AspNetCore.Http;

namespace UniversityHelper.RightsService.Mappers.Db;

public class DbRoleRightMapper : IDbRoleRightMapper
{
  private readonly IHttpContextAccessor _httpContextAccessor;

  public DbRoleRightMapper(
    IHttpContextAccessor httpContextAccessor)
  {
    _httpContextAccessor = httpContextAccessor;
  }

  public List<DbRoleRight> Map(Guid roleId, List<int> rightsIds)
  {
    Guid senderId = _httpContextAccessor.HttpContext.GetUserId();
    DateTime createdAtUtc = DateTime.UtcNow;

    return rightsIds.Select(x =>
      new DbRoleRight
      {
        Id = Guid.NewGuid(),
        RoleId = roleId,
        CreatedBy = senderId,
        RightId = x
      }).ToList();
  }
}
