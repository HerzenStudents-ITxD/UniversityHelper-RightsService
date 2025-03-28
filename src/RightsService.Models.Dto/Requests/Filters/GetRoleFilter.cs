﻿using Microsoft.AspNetCore.Mvc;
using System;

namespace UniversityHelper.RightsService.Models.Dto.Requests.Filters;

public record GetRoleFilter
{
  [FromQuery(Name = "roleid")]
  public Guid RoleId { get; set; }

  [FromQuery(Name = "locale")]
  public string Locale { get; set; }
}
