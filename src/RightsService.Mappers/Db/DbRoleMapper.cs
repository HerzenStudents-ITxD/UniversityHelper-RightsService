using UniversityHelper.Core.Extensions;
using UniversityHelper.RightsService.Mappers.Db.Interfaces;
using UniversityHelper.RightsService.Models.Db;
using UniversityHelper.RightsService.Models.Dto.Requests;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace UniversityHelper.RightsService.Mappers.Db
{
  public class DbRoleMapper : IDbRoleMapper
  {
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDbRoleLocalizationMapper _localizationMapper;

    public DbRoleMapper(
      IHttpContextAccessor httpContextAccessor,
      IDbRoleLocalizationMapper localizationMapper)
    {
      _httpContextAccessor = httpContextAccessor;
      _localizationMapper = localizationMapper;
    }

    public DbRole Map(CreateRoleRequest request)
    {
      if (request == null)
      {
        return null;
      }

      var roleId = Guid.NewGuid();
      var creatorId = _httpContextAccessor.HttpContext.GetUserId();

      return new DbRole
      {
        Id = roleId,
        CreatedBy = creatorId,
        IsActive = true,
        RoleLocalizations = request.Localizations.Select(rl => _localizationMapper.Map(rl, roleId)).ToList(),
        RolesRights = request.Rights?.Select(x => new DbRoleRight
        {
          Id = Guid.NewGuid(),
          RoleId = roleId,
          CreatedBy = creatorId,
          RightId = x,
        }).ToList()
      };
    }
  }
}
