using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation.Results;
using UniversityHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using UniversityHelper.Core.Constants;
using UniversityHelper.Core.Helpers.Interfaces;
using UniversityHelper.Core.Responses;
using UniversityHelper.RightsService.Business.Commands.User.Interfaces;
using UniversityHelper.RightsService.Data.Interfaces;
using UniversityHelper.RightsService.Mappers.Db.Interfaces;
using UniversityHelper.RightsService.Models.Db;
using UniversityHelper.RightsService.Models.Dto.Requests;
using UniversityHelper.RightsService.Validation.Interfaces;

namespace UniversityHelper.RightsService.Business.Commands.User
{
  public class EditUserRoleCommand : IEditUserRoleCommand
  {
    private readonly IAccessValidator _accessValidator;
    private readonly IResponseCreator _responseCreator;
    private readonly IEditUserRoleRequestValidator _validator;
    private readonly IDbUserRoleMapper _mapper;
    private readonly IUserRoleRepository _repository;

    public EditUserRoleCommand(
      IAccessValidator accessValidator,
      IResponseCreator responseCreator,
      IEditUserRoleRequestValidator validator,
      IDbUserRoleMapper mapper,
      IUserRoleRepository repository)
    {
      _accessValidator = accessValidator;
      _responseCreator = responseCreator;
      _validator = validator;
      _mapper = mapper;
      _repository = repository;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(EditUserRoleRequest request)
    {
      if (!await _accessValidator.HasRightsAsync(Rights.AddGetEditRemoveUserRoles))
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.Forbidden);
      }

      ValidationResult validationResult = await _validator.ValidateAsync(request);

      if (!validationResult.IsValid)
      {
        return _responseCreator.CreateFailureResponse<bool>(
          HttpStatusCode.BadRequest,
          validationResult.Errors.Select(e => e.ErrorMessage).ToList());
      }

      DbUserRole oldUser = await _repository.GetAsync(request.UserId);

      if (oldUser is null && !request.RoleId.HasValue)
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.BadRequest);
      }

      OperationResultResponse<bool> response = new();

      if (!request.RoleId.HasValue)
      {
        response.Body = await _repository.RemoveAsync(request.UserId, oldUser);
      }
      else
      {
        response.Body = oldUser is not null
          ? await _repository.EditAsync(oldUser, request.RoleId.Value)
          : (await _repository.CreateAsync(_mapper.Map(request))).HasValue;
      }

      return response;
    }
  }
}
