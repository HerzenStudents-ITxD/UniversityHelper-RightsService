﻿using System.Reflection;
using System.Threading.Tasks;
using HerzenHelper.Core.EFSupport.Provider;
using HerzenHelper.RightsService.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace HerzenHelper.RightsService.Data.Provider.MsSql.Ef
{
  public class RightsServiceDbContext : DbContext, IDataProvider
  {
    public DbSet<DbRightLocalization> RightsLocalizations { get; set; }
    public DbSet<DbRole> Roles { get; set; }
    public DbSet<DbRoleLocalization> RolesLocalizations { get; set; }
    public DbSet<DbRoleRight> RolesRights { get; set; }
    public DbSet<DbUserRole> UsersRoles { get; set; }

    public RightsServiceDbContext(DbContextOptions<RightsServiceDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("HerzenHelper.RightsService.Models.Db"));
    }

    public object MakeEntityDetached(object obj)
    {
      Entry(obj).State = EntityState.Detached;

      return Entry(obj).State;
    }

    void IBaseDataProvider.Save()
    {
      SaveChanges();
    }

    public void EnsureDeleted()
    {
      Database.EnsureDeleted();
    }

    public bool IsInMemory()
    {
      return Database.IsInMemory();
    }

    public async Task SaveAsync()
    {
      await SaveChangesAsync();
    }
  }
}
