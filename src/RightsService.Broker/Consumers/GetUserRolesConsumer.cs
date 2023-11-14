using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityHelper.Core.BrokerSupport.Broker;
using UniversityHelper.Models.Broker.Models;
using UniversityHelper.Models.Broker.Models.Right;
using UniversityHelper.Models.Broker.Requests.Rights;
using UniversityHelper.Models.Broker.Responses.Rights;
using UniversityHelper.RightsService.Data.Interfaces;
using UniversityHelper.RightsService.Models.Db;
using MassTransit;

namespace UniversityHelper.RightsService.Broker.Consumers;

public class GetUserRolesConsumer : IConsumer<IGetUserRolesRequest>
{
  private readonly IUserRoleRepository _repository;

  private async Task<object> GetRolesAsync(IGetUserRolesRequest request)
  {
    List<DbUserRole> dbUsersRoles = await _repository.GetAsync(request.UserIds, request.Locale);

    List<DbRole> dbRoles = dbUsersRoles.Select(u => u.Role).Distinct().ToList();

    return IGetUserRolesResponse.CreateObj(
      dbRoles.Select(r =>
        new RoleData(
          r.Id,
          r.RoleLocalizations.FirstOrDefault()?.Name,
          r.RolesRights.Select(rr => rr.RightId).ToList(),
          dbUsersRoles.Where(u => u.RoleId == r.Id).Select(u => u.UserId).ToList()))
      .ToList());
  }

  public GetUserRolesConsumer(IUserRoleRepository userRepository)
  {
    _repository = userRepository;
  }

  public async Task Consume(ConsumeContext<IGetUserRolesRequest> context)
  {
    object response = OperationResultWrapper.CreateResponse(GetRolesAsync, context.Message);

    await context.RespondAsync<IOperationResult<IGetUserRolesResponse>>(response);
  }
}
