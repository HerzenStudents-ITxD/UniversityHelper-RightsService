using System.Collections.Generic;
using UniversityHelper.RightsService.Models.Dto.Models;

namespace UniversityHelper.RightsService.Models.Dto.Responses
{
  public record RoleResponse
  {
    public RoleInfo Role { get; set; }
    public List<UserInfo> Users { get; set; }
  }
}
