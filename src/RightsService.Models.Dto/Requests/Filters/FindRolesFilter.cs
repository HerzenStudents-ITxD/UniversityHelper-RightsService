﻿using UniversityHelper.Core.Requests;
using Microsoft.AspNetCore.Mvc;

namespace UniversityHelper.RightsService.Models.Dto.Requests.Filters;

public record FindRolesFilter : BaseFindFilter
{
  [FromQuery(Name = "includedeactivated")]
  public bool IncludeDeactivated { get; set; } = false;

  [FromQuery(Name = "locale")]
  public string Locale { get; set; }
}
