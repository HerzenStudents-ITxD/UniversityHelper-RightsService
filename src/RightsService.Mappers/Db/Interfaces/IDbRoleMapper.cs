using HerzenHelper.Core.Attributes;
using HerzenHelper.RightsService.Models.Db;
using HerzenHelper.RightsService.Models.Dto.Requests;

namespace HerzenHelper.RightsService.Mappers.Db.Interfaces
{
  [AutoInject]
  public interface IDbRoleMapper
  {
    DbRole Map(CreateRoleRequest value);
  }
}
