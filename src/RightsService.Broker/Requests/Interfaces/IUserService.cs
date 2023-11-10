using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;
using UniversityHelper.Models.Broker.Models;
using UniversityHelper.Models.Broker.Models.User;

namespace UniversityHelper.RightsService.Broker.Requests.Interfaces
{
  [AutoInject]
  public interface IUserService
  {
    Task<List<Guid>> CheckUsersExistence(List<Guid> usersIds, List<string> errors);
    Task<List<UserData>> GetUsersAsync(List<Guid> usersIds, List<string> errors);
  }
}
