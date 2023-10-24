using HerzenHelper.Core.Attributes;
using HerzenHelper.Models.Broker.Models;
using HerzenHelper.RightsService.Models.Dto.Models;

namespace HerzenHelper.RightsService.Mappers.Models.Interfaces
{
  [AutoInject]
  public interface IUserInfoMapper
  {
    UserInfo Map(UserData userData);
  }
}
