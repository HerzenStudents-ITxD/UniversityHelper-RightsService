using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation.Results;
using UniversityHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using UniversityHelper.Core.Helpers.Interfaces;
using UniversityHelper.Core.Responses;
using UniversityHelper.RightsService.Business.Commands.Role.Interfaces;
using UniversityHelper.RightsService.Data.Interfaces;
using UniversityHelper.RightsService.Mappers.Db.Interfaces;
using UniversityHelper.RightsService.Models.Db;
using UniversityHelper.RightsService.Models.Dto.Constants;
using UniversityHelper.RightsService.Models.Dto.Requests;
using UniversityHelper.RightsService.Validation.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace UniversityHelper.RightsService.Business.Commands.Role;

public class UpdateRoleRightsCommand : IUpdateRoleRightsCommand
{
  private readonly IRoleRepository _roleRepository;
  private readonly IDbRoleRightMapper _dbRoleRightMapper;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly IAccessValidator _accessValidator;
  private readonly IUpdateRoleRightsRequestValidator _requestValidator;
  private readonly IMemoryCache _cache;
  private readonly IResponseCreator _responseCreator;

  private async Task UpdateCacheAsync(IEnumerable<int> addedRights, Guid roleId)
  {
    List<(Guid roleId, bool isActive, IEnumerable<int> rights)> rolesRights = _cache.Get<List<(Guid, bool, IEnumerable<int>)>>(CacheKeys.RolesRights);

    if (rolesRights == null)
    {
      List<DbRole> roles = await _roleRepository.GetAllWithRightsAsync();

      rolesRights = roles.Select(x => (x.Id, x.IsActive, x.RolesRights.Select(x => x.RightId))).ToList();
    }
    else
    {
      (Guid roleId, bool isActive, IEnumerable<int> rights) oldRole = rolesRights.FirstOrDefault(x => x.roleId == roleId);

      rolesRights.Remove(oldRole);
      rolesRights.Add((roleId, oldRole.isActive, addedRights));
    }

    _cache.Set(CacheKeys.RolesRights, rolesRights);
  }

  public UpdateRoleRightsCommand(
    IRoleRepository roleRepository,
    IDbRoleRightMapper dbRoleRightMapper,
    IHttpContextAccessor httpContextAccessor,
    IAccessValidator accessValidator,
    IUpdateRoleRightsRequestValidator requestValidator,
    IMemoryCache cache,
    IResponseCreator responseCreator)
  {
    _roleRepository = roleRepository;
    _dbRoleRightMapper = dbRoleRightMapper;
    _httpContextAccessor = httpContextAccessor;
    _accessValidator = accessValidator;
    _requestValidator = requestValidator;
    _cache = cache;
    _responseCreator = responseCreator;
  }

  public async Task<OperationResultResponse<bool>> ExecuteAsync(UpdateRoleRightsRequest request)
  {
    if (!await _accessValidator.IsAdminAsync())
    {
      return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.Forbidden);
    }

    ValidationResult validationResult = await _requestValidator.ValidateAsync(request);
    if (!validationResult.IsValid)
    {
      return _responseCreator.CreateFailureResponse<bool>(
        HttpStatusCode.BadRequest,
        validationResult.Errors.Select(validationFailure => validationFailure.ErrorMessage).ToList());
    }

    OperationResultResponse<bool> response = new();

    response.Body = await _roleRepository.EditRoleRightsAsync(
      request.RoleId,
      _dbRoleRightMapper.Map(request.RoleId, request.Rights));

    await UpdateCacheAsync(request.Rights, request.RoleId);

    return response;
  }
}
