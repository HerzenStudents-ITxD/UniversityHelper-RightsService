using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UniversityHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using UniversityHelper.Core.Helpers.Interfaces;
using UniversityHelper.Core.Responses;
using UniversityHelper.RightsService.Business.Commands.Right.Interfaces;
using UniversityHelper.RightsService.Data.Interfaces;
using UniversityHelper.RightsService.Mappers.Models.Interfaces;
using UniversityHelper.RightsService.Models.Dto.Models;

namespace UniversityHelper.RightsService.Business.Commands.Right
{
  /// <inheritdoc cref="IGetRightsListCommand"/>
  public class GetRightsListCommand : IGetRightsListCommand
  {
    private readonly IRightLocalizationRepository _repository;
    private readonly IRightInfoMapper _mapper;
    private readonly IAccessValidator _accessValidator;
    private readonly IResponseCreator _responseCreator;

    public GetRightsListCommand(
      IRightLocalizationRepository repository,
      IRightInfoMapper mapper,
      IAccessValidator accessValidator,
      IResponseCreator responseCreator)
    {
      _repository = repository;
      _mapper = mapper;
      _accessValidator = accessValidator;
      _responseCreator = responseCreator;
    }

    public async Task<OperationResultResponse<List<RightInfo>>> ExecuteAsync(string locale)
    {
      return await _accessValidator.IsAdminAsync()
        ? new OperationResultResponse<List<RightInfo>>(
          body: (await _repository.GetRightsListAsync(locale))?.Select(_mapper.Map).ToList())
        : _responseCreator.CreateFailureResponse<List<RightInfo>>(HttpStatusCode.Forbidden);
    }
  }
}
