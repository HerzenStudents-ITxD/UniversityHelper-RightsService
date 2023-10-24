using System.Collections.Generic;
using System.Threading.Tasks;
using HerzenHelper.Core.Attributes;

namespace HerzenHelper.RightsService.Validation.Helpers.Interfaces
{
  [AutoInject]
  public interface ICheckRightsUniquenessHelper
  {
    Task<bool> IsRightsSetUniqueAsync(IEnumerable<int> rightsIds);
  }
}
