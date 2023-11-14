using UniversityHelper.Models.Broker.Models;
using UniversityHelper.Models.Broker.Models.User;
using UniversityHelper.RightsService.Mappers.Models.Interfaces;
using UniversityHelper.RightsService.Models.Dto.Models;

namespace UniversityHelper.RightsService.Mappers.Models;

public class UserInfoMapper : IUserInfoMapper
{
  public UserInfo Map(UserData userData)
  {
    if (userData == null)
    {
      return null;
    }

    return new UserInfo
    {
      Id = userData.Id,
      FirstName = userData.FirstName,
      LastName = userData.LastName,
      MiddleName = userData.MiddleName
    };
  }
}
