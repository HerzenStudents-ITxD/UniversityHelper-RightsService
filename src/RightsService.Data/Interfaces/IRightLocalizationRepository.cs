using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;
using UniversityHelper.RightsService.Models.Db;

namespace UniversityHelper.RightsService.Data.Interfaces;

/// <summary>
/// Represents interface of repository in repository pattern.
/// Provides methods for working with the database of RightsService.
/// </summary>
[AutoInject]
public interface IRightLocalizationRepository
{
  Task<List<DbRightLocalization>> GetRightsListAsync();

  Task<List<DbRightLocalization>> GetRightsListAsync(string locale);
}
