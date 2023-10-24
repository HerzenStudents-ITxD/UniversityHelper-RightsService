using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HerzenHelper.RightsService.Data.Interfaces;
using HerzenHelper.RightsService.Models.Db;
using HerzenHelper.RightsService.Models.Dto.Constants;
using HerzenHelper.RightsService.Validation.Helpers.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace HerzenHelper.RightsService.Validation.Helpers
{
  public class MemoryCacheHelper : IMemoryCacheHelper
  {
    private readonly IMemoryCache _cache;
    private readonly IRoleRepository _roleRepository;
    private readonly IRightLocalizationRepository _rightLocalizationRepository;

    public MemoryCacheHelper(
      IMemoryCache cache,
      IRoleRepository roleRepository,
      IRightLocalizationRepository rightLocalizationRepository)
    {
      _cache = cache;
      _roleRepository = roleRepository;
      _rightLocalizationRepository = rightLocalizationRepository;
    }

    public async Task<List<int>> GetRightIdsAsync()
    {
      List<int> rights = _cache.Get<List<int>>(CacheKeys.RightsIds);

      if (rights == null)
      {
        rights = (await _rightLocalizationRepository.GetRightsListAsync()).Select(r => r.RightId).ToList();
        _cache.Set(CacheKeys.RightsIds, rights);
      }

      return rights;
    }

    public async Task<List<(Guid roleId, bool isActive, IEnumerable<int> rights)>> GetRoleRightsListAsync()
    {
      List<(Guid roleId, bool isActive, IEnumerable<int> rights)> rolesRights = _cache.Get<List<(Guid, bool, IEnumerable<int>)>>(CacheKeys.RolesRights);

      if (rolesRights is null)
      {
        List<DbRole> roles = await _roleRepository.GetAllWithRightsAsync();

        rolesRights = roles.Select(x => (x.Id, x.IsActive, x.RolesRights.Select(x => x.RightId))).ToList();
        
        _cache.Set(CacheKeys.RolesRights, rolesRights);
      }

      return rolesRights;
    }
  }
}
