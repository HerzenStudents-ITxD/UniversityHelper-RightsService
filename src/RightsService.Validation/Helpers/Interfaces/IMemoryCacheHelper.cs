using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HerzenHelper.Core.Attributes;

namespace HerzenHelper.RightsService.Validation.Helpers.Interfaces
{
  [AutoInject]
  public interface IMemoryCacheHelper
  {
    Task<List<(Guid roleId, bool isActive, IEnumerable<int> rights)>> GetRoleRightsListAsync();
    Task<List<int>> GetRightIdsAsync();
  }
}
