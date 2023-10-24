using System;

namespace HerzenHelper.RightsService.Models.Dto.Requests
{
  public record EditUserRoleRequest
  {
    public Guid UserId { get; set; }
    public Guid? RoleId { get; set; }
  }
}
