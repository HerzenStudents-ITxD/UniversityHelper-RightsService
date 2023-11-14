using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UniversityHelper.Core.Responses;
using UniversityHelper.Models.Broker.Models;
using UniversityHelper.Models.Broker.Models.User;
using UniversityHelper.RightsService.Broker.Requests.Interfaces;
using UniversityHelper.RightsService.Business.Role.Interfaces;
using UniversityHelper.RightsService.Data.Interfaces;
using UniversityHelper.RightsService.Mappers.Responses.Interfaces;
using UniversityHelper.RightsService.Models.Db;
using UniversityHelper.RightsService.Models.Dto.Requests.Filters;
using UniversityHelper.RightsService.Models.Dto.Responses;
using Microsoft.AspNetCore.Http;

namespace UniversityHelper.RightsService.Business.Role;

/// <inheritdoc/>
public class GetRoleCommand : IGetRoleCommand
{
  private readonly IRoleRepository _roleRepository;
  private readonly IRoleResponseMapper _roleResponseMapper;
  private readonly IUserService _userService;
  private readonly IHttpContextAccessor _httpContextAccessor;

  public GetRoleCommand(
    IRoleRepository roleRepository,
    IRoleResponseMapper roleResponseMapper,
    IUserService userService,
    IHttpContextAccessor httpContextAccessor)
  {
    _roleRepository = roleRepository;
    _roleResponseMapper = roleResponseMapper;
    _userService = userService;
    _httpContextAccessor = httpContextAccessor;
  }

  public async Task<OperationResultResponse<RoleResponse>> ExecuteAsync(GetRoleFilter filter)
  {
    OperationResultResponse<RoleResponse> result = new();

    (DbRole dbRole, List<DbUserRole> dbUsersRoles, List<DbRightLocalization> dbRights) = await _roleRepository.GetAsync(filter);

    if (dbRole is null)
    {
      _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;

      return result;
    }

    List<Guid> usersIds = dbUsersRoles?.Select(u => u.UserId).ToList();

    usersIds.Add(dbRole.CreatedBy);

    List<UserData> usersDatas = await _userService.GetUsersAsync(usersIds, result.Errors);

    result.Body = _roleResponseMapper.Map(dbRole, dbRights, usersDatas);

    return result;
  }
}
