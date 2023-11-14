using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityHelper.RightsService.Data.Interfaces;
using UniversityHelper.RightsService.Data.Provider;
using UniversityHelper.RightsService.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace UniversityHelper.RightsService.Data;

/// <inheritdoc cref="IRightLocalizationRepository"/>
public class RightLocalizationRepository : IRightLocalizationRepository
{
  private readonly IDataProvider _provider;

  public RightLocalizationRepository(
    IDataProvider provider)
  {
    _provider = provider;
  }

  public async Task<List<DbRightLocalization>> GetRightsListAsync()
  {
    return await _provider.RightsLocalizations.ToListAsync();
  }

  public async Task<List<DbRightLocalization>> GetRightsListAsync(string locale)
  {
    return await _provider.RightsLocalizations.Where(r => r.Locale == locale).OrderBy(r => r.RightId).ToListAsync();
  }
}
