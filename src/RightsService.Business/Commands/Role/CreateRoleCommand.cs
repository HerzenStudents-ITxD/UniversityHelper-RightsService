using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation.Results;
using UniversityHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using UniversityHelper.Core.Helpers.Interfaces;
using UniversityHelper.Core.Responses;
using UniversityHelper.RightsService.Business.Role.Interfaces;
using UniversityHelper.RightsService.Data.Interfaces;
using UniversityHelper.RightsService.Mappers.Db.Interfaces;
using UniversityHelper.RightsService.Models.Db;
using UniversityHelper.RightsService.Models.Dto.Constants;
using UniversityHelper.RightsService.Models.Dto.Requests;
using UniversityHelper.RightsService.Validation.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace UniversityHelper.RightsService.Business.Role
{
  /// <inheritdoc/>
  public class CreateRoleCommand : ICreateRoleCommand
  {
    private readonly IRoleRepository _roleRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICreateRoleRequestValidator _validator;
    private readonly IDbRoleMapper _mapper;
    private readonly IAccessValidator _accessValidator;
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
        rolesRights.Add((roleId, true, addedRights));
      }

      _cache.Set(CacheKeys.RolesRights, rolesRights);
    }

    public CreateRoleCommand(
      IHttpContextAccessor httpContextAccessor,
      IRoleRepository roleRepository,
      ICreateRoleRequestValidator validator,
      IDbRoleMapper mapper,
      IAccessValidator accessValidator,
      IMemoryCache cache,
      IResponseCreator responseCreator)
    {
      _validator = validator;
      _httpContextAccessor = httpContextAccessor;
      _roleRepository = roleRepository;
      _mapper = mapper;
      _accessValidator = accessValidator;
      _cache = cache;
      _responseCreator = responseCreator;
    }

    public async Task<OperationResultResponse<Guid>> ExecuteAsync(CreateRoleRequest request)
    {
      if (!await _accessValidator.IsAdminAsync())
      {
        return _responseCreator.CreateFailureResponse<Guid>(HttpStatusCode.Forbidden);
      }

      ValidationResult validationResult = await _validator.ValidateAsync(request);
      if (!validationResult.IsValid)
      {
        return _responseCreator.CreateFailureResponse<Guid>(
          HttpStatusCode.BadRequest,
          validationResult.Errors.Select(validationFailure => validationFailure.ErrorMessage).ToList());
      }

      DbRole dbRole = _mapper.Map(request);
      await _roleRepository.CreateAsync(dbRole);

      _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

      await UpdateCacheAsync(request.Rights, dbRole.Id);

      return new OperationResultResponse<Guid>(body: dbRole.Id);
    }
  }
}
