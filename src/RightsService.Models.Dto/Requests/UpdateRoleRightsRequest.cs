using System;
using System.Collections.Generic;

namespace HerzenHelper.RightsService.Models.Dto.Requests
{
  public class UpdateRoleRightsRequest
  {
    public Guid RoleId { get; set; }
    public List<int> Rights { get; set; }
  }
}
