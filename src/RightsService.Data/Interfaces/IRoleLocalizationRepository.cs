using System;
using System.Threading.Tasks;
using HerzenHelper.Core.Attributes;
using HerzenHelper.RightsService.Models.Db;
using Microsoft.AspNetCore.JsonPatch;

namespace HerzenHelper.RightsService.Data.Interfaces
{
  [AutoInject]
  public interface IRoleLocalizationRepository
  {
    Task<Guid?> CreateAsync(DbRoleLocalization roleLocalization);
    Task<DbRoleLocalization> GetAsync(Guid roleLocalizationId);
    Task<bool> DoesLocaleExistAsync(Guid roleId, string locale);
    Task<bool> DoesNameExistAsync(string locale, string name, Guid id = default);
    Task<bool> EditRoleLocalizationAsync(Guid roleLocalizationId, JsonPatchDocument<DbRoleLocalization> patch);
  }
}
