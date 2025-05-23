﻿using System.Collections.Generic;
using System.Linq;
using UniversityHelper.RightsService.Mappers.Models.Interfaces;
using UniversityHelper.RightsService.Models.Db;
using UniversityHelper.RightsService.Models.Dto.Models;

namespace UniversityHelper.RightsService.Mappers.Models;

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
