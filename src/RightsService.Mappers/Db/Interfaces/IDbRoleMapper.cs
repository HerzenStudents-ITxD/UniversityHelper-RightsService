using UniversityHelper.Core.Attributes;
using UniversityHelper.RightsService.Models.Db;
using UniversityHelper.RightsService.Models.Dto.Requests;

namespace UniversityHelper.RightsService.Mappers.Db.Interfaces
{
  [AutoInject]
  public interface IDbRoleMapper
  {
    DbRole Map(CreateRoleRequest value);
  }
}
