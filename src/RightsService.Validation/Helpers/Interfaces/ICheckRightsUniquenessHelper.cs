using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;

namespace UniversityHelper.RightsService.Validation.Helpers.Interfaces;

[AutoInject]
public interface ICheckRightsUniquenessHelper
{
  Task<bool> IsRightsSetUniqueAsync(IEnumerable<int> rightsIds);
}
