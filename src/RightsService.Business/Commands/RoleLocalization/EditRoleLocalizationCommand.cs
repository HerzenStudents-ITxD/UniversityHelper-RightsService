using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation.Results;
using HerzenHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using HerzenHelper.Core.Helpers.Interfaces;
using HerzenHelper.Core.Responses;
using HerzenHelper.RightsService.Business.Commands.RoleLocalization.Interfaces;
using HerzenHelper.RightsService.Data.Interfaces;
using HerzenHelper.RightsService.Mappers.Models.Interfaces;
using HerzenHelper.RightsService.Models.Dto.Requests;
using HerzenHelper.RightsService.Validation.Interfaces;
using Microsoft.AspNetCore.JsonPatch;

namespace HerzenHelper.RightsService.Business.Commands.RoleLocalization
{
  public class EditRoleLocalizationCommand : IEditRoleLocalizationCommand
  {
    private readonly IAccessValidator _accessValidator;
    private readonly IResponseCreator _responseCreator;
    private readonly IRoleLocalizationRepository _roleLocalizationRepository;
    private readonly IPatchDbRoleLocalizationMapper _roleLocalizationMapper;
    private readonly IEditRoleLocalizationRequestValidator _validator;

    public EditRoleLocalizationCommand(
      IAccessValidator accessValidator,
      IResponseCreator responseCreator,
      IRoleLocalizationRepository roleLocalizationRepository,
      IPatchDbRoleLocalizationMapper roleLocalizationMapper,
      IEditRoleLocalizationRequestValidator validator)
    {
      _accessValidator = accessValidator;
      _responseCreator = responseCreator;
      _roleLocalizationRepository = roleLocalizationRepository;
      _roleLocalizationMapper = roleLocalizationMapper;
      _validator = validator;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(Guid roleLocalizationId, JsonPatchDocument<EditRoleLocalizationRequest> request)
    {
      if (!await _accessValidator.IsAdminAsync())
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.Forbidden);
      }

      ValidationResult result = await _validator.ValidateAsync((roleLocalizationId, request));

      if (!result.IsValid)
      {
        return _responseCreator.CreateFailureResponse<bool>(
          HttpStatusCode.BadRequest,
          result.Errors.Select(x => x.ErrorMessage).ToList());
      }

      return new OperationResultResponse<bool>()
      {
        Body = await _roleLocalizationRepository.EditRoleLocalizationAsync(roleLocalizationId, _roleLocalizationMapper.Map(request))
      };
    }
  }
}
