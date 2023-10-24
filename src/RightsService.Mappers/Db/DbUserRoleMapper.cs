using System;
using HerzenHelper.Core.Extensions;
using HerzenHelper.Models.Broker.Publishing.Subscriber.Right;
using HerzenHelper.RightsService.Mappers.Db.Interfaces;
using HerzenHelper.RightsService.Models.Db;
using HerzenHelper.RightsService.Models.Dto.Requests;
using Microsoft.AspNetCore.Http;

namespace HerzenHelper.RightsService.Mappers.Db
{
  public class DbUserRoleMapper : IDbUserRoleMapper
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DbUserRoleMapper(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public DbUserRole Map(ICreateUserRolePublish request)
    {
      return new DbUserRole
      {
        Id = Guid.NewGuid(),
        UserId = request.UserId,
        RoleId = request.RoleId,
        CreatedBy = request.CreatedBy,
        IsActive = request.IsActive
      };
    }

    public DbUserRole Map(EditUserRoleRequest request)
    {
      return new DbUserRole
      {
        Id = Guid.NewGuid(),
        UserId = request.UserId,
        RoleId = request.RoleId.Value,
        CreatedBy = _httpContextAccessor.HttpContext.GetUserId(),
        IsActive = true
      };
    }
  }
}
