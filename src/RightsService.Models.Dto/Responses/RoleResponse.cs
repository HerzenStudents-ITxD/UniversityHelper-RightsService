using System.Collections.Generic;
using HerzenHelper.RightsService.Models.Dto.Models;

namespace HerzenHelper.RightsService.Models.Dto.Responses
{
  public record RoleResponse
  {
    public RoleInfo Role { get; set; }
    public List<UserInfo> Users { get; set; }
  }
}
