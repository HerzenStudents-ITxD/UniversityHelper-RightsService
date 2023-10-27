using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HerzenHelper.Core.Attributes;
using HerzenHelper.Models.Broker.Models;
using HerzenHelper.Models.Broker.Models.User;

namespace HerzenHelper.RightsService.Broker.Requests.Interfaces
{
  [AutoInject]
  public interface IUserService
  {
    Task<List<Guid>> CheckUsersExistence(List<Guid> usersIds, List<string> errors);
    Task<List<UserData>> GetUsersAsync(List<Guid> usersIds, List<string> errors);
  }
}
