﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HerzenHelper.Models.Broker.Publishing;
using HerzenHelper.Core.Attributes;
using HerzenHelper.RightsService.Models.Db;

namespace HerzenHelper.RightsService.Data.Interfaces
{
  [AutoInject]
  public interface IUserRoleRepository
  {
    Task<Guid?> CreateAsync(DbUserRole dbUserRole);

    Task<Guid?> ActivateAsync(IActivateUserPublish request);

    Task<bool> EditAsync(DbUserRole oldUser, Guid roleId);

    Task<bool> CheckRightsAsync(Guid userId, params int[] rightIds);

    Task<List<DbUserRole>> GetAsync(List<Guid> usersIds, string locale);

    Task<DbUserRole> GetAsync(Guid userId);

    Task<List<DbUserRole>> GetWithRightsAsync();

    Task<bool> RemoveAsync(Guid userId, DbUserRole removedUser = null, Guid? removedBy = null);

    Task<bool> DoesExistAsync(Guid userId);
  }
}
