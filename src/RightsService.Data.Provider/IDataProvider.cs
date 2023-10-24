using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.EFSupport.Provider;
using HerzenHelper.Core.Enums;
using HerzenHelper.RightsService.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace HerzenHelper.RightsService.Data.Provider
{
  [AutoInject(InjectType.Scoped)]
    public interface IDataProvider : IBaseDataProvider
    {
        DbSet<DbRightLocalization> RightsLocalizations { get; set; }
        DbSet<DbRole> Roles { get; set; }
        DbSet<DbRoleLocalization> RolesLocalizations { get; set; }
        DbSet<DbRoleRight> RolesRights { get; set;}
        DbSet<DbUserRole> UsersRoles { get; set; }
    }
}
