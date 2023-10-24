﻿using HerzenHelper.Core.Attributes;
using HerzenHelper.RightsService.Models.Db;
using HerzenHelper.RightsService.Models.Dto.Models;

namespace HerzenHelper.RightsService.Mappers.Models.Interfaces
{
  [AutoInject]
  public interface IRoleLocalizationInfoMapper
  {
    RoleLocalizationInfo Map(DbRoleLocalization dbRoleLocalization);
  }
}
