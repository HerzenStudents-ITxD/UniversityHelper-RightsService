using UniversityHelper.Core.Attributes;
using UniversityHelper.Models.Broker.Models;
using UniversityHelper.Models.Broker.Models.User;
using UniversityHelper.RightsService.Models.Dto.Models;

namespace UniversityHelper.RightsService.Mappers.Models.Interfaces
{
  [AutoInject]
  public interface IUserInfoMapper
  {
    UserInfo Map(UserData userData);
  }
}
