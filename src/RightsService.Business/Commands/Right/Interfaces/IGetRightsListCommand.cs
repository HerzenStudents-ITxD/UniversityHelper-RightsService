using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.Responses;
using HerzenHelper.RightsService.Models.Dto.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HerzenHelper.RightsService.Business.Commands.Right.Interfaces
{
  /// <summary>
  /// Represents the command pattern.
  /// Provides a method for getting all added rights.
  /// </summary>
  [AutoInject]
    public interface IGetRightsListCommand
    {
        /// <summary>
        /// Returns all added rights.
        /// </summary>
        /// <returns>All added rights.</returns>
        Task<OperationResultResponse<List<RightInfo>>> ExecuteAsync(string locale);
    }
}
