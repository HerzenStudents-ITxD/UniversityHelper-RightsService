using System.Collections.Generic;
using System.Linq;
using HerzenHelper.RightsService.Mappers.Models.Interfaces;
using HerzenHelper.RightsService.Models.Db;
using HerzenHelper.RightsService.Models.Dto.Models;

namespace HerzenHelper.RightsService.Mappers.Models
{
  public class RoleInfoMapper : IRoleInfoMapper
  {
    private readonly IRoleLocalizationInfoMapper _roleLocalizationInfoMapper;

    public RoleInfoMapper(IRoleLocalizationInfoMapper roleLocalizationInfoMapper)
    {
      _roleLocalizationInfoMapper = roleLocalizationInfoMapper;
    }

    public RoleInfo Map(DbRole dbRole, List<RightInfo> rights, List<UserInfo> userInfos)
    {
      if (dbRole == null)
      {
        return null;
      }

      return new RoleInfo
      {
        Id = dbRole.Id,
        IsActive = dbRole.IsActive,
        CreatedBy = userInfos?.FirstOrDefault(x => x.Id == dbRole.CreatedBy),
        Rights = rights,
        Localizations = dbRole.RoleLocalizations.Select(_roleLocalizationInfoMapper.Map).ToList()
      };
    }
  }
}
