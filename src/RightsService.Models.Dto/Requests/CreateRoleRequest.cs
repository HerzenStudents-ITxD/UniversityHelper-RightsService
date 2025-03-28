﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniversityHelper.RightsService.Models.Dto.Requests;

public record CreateRoleRequest
{
  [Required]
  public List<CreateRoleLocalizationRequest> Localizations { get; set; }
  public List<int> Rights { get; set; }
}
