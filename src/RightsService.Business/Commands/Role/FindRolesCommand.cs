﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UniversityHelper.Core.FluentValidationExtensions;
using UniversityHelper.Core.Helpers.Interfaces;
using UniversityHelper.Core.Responses;
using UniversityHelper.Core.Validators.Interfaces;
using UniversityHelper.RightsService.Broker.Requests.Interfaces;
using UniversityHelper.RightsService.Business.Role.Interfaces;
using UniversityHelper.RightsService.Data.Interfaces;
using UniversityHelper.RightsService.Mappers.Models.Interfaces;
using UniversityHelper.RightsService.Models.Db;
using UniversityHelper.RightsService.Models.Dto.Models;
using UniversityHelper.RightsService.Models.Dto.Requests.Filters;
using Microsoft.AspNetCore.Http;

namespace UniversityHelper.RightsService.Business.Role;

/// <inheritdoc/>
public class FindRolesCommand : IFindRolesCommand
{
  private readonly IRoleRepository _roleRepository;
  private readonly IRoleInfoMapper _roleInfoMapper;
  private readonly IUserInfoMapper _userInfoMapper;
  private readonly IRightInfoMapper _rightMapper;
  private readonly IUserService _userService;
  //private readonly IBaseFindFilterValidator _findFilterValidator;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly IResponseCreator _responseCreator;

  public FindRolesCommand(
    IRoleRepository roleRepository,
    IUserInfoMapper userInfoMapper,
    IRoleInfoMapper roleInfoMapper,
    IRightInfoMapper rightMapper,
    IUserService userService,
    //IBaseFindFilterValidator findFilterValidator,
    IHttpContextAccessor httpContextAccessor,
    IResponseCreator responseCreator)
  {
    _roleRepository = roleRepository;
    _roleInfoMapper = roleInfoMapper;
    _userInfoMapper = userInfoMapper;
    _rightMapper = rightMapper;
    _userService = userService;
    //_findFilterValidator = findFilterValidator;
    _httpContextAccessor = httpContextAccessor;
    _responseCreator = responseCreator;
  }

  public async Task<FindResultResponse<RoleInfo>> ExecuteAsync(FindRolesFilter filter)
  {
    //if (!_findFilterValidator.ValidateCustom(filter, out List<string> errors))
    //{
    //  return _responseCreator.CreateFailureFindResponse<RoleInfo>(
    //    HttpStatusCode.BadRequest,
    //    errors);
    //}

    (List<(DbRole role, List<DbRightLocalization> rights)> roles, int totalCount) = filter.IncludeDeactivated
      ? await _roleRepository.FindAllAsync(filter)
      : await _roleRepository.FindActiveAsync(filter);

    FindResultResponse<RoleInfo> response = new(totalCount: totalCount, errors: new());// errors);

    List<Guid> usersIds = new();

    foreach ((DbRole role, List<DbRightLocalization> rights) in roles)
    {
      usersIds.Add(role.CreatedBy);
    }

    List<UserInfo> usersInfos = (await _userService.GetUsersAsync(usersIds.Distinct().ToList(), new()))?//errors))?
      .Select(_userInfoMapper.Map)
      .ToList();

    response.Body = roles.Select(
      pair => _roleInfoMapper.Map(pair.role, pair.rights.Select(_rightMapper.Map).ToList(), usersInfos)).ToList();

    return response;
  }
}
