using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.EFSupport.Provider;
using UniversityHelper.Core.Enums;
using UniversityHelper.RightsService.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace UniversityHelper.RightsService.Data.Provider
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
