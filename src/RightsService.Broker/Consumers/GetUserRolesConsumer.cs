using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HerzenHelper.Core.BrokerSupport.Broker;
using HerzenHelper.Models.Broker.Models;
using HerzenHelper.Models.Broker.Models.Right;
using HerzenHelper.Models.Broker.Requests.Rights;
using HerzenHelper.Models.Broker.Responses.Rights;
using HerzenHelper.RightsService.Data.Interfaces;
using HerzenHelper.RightsService.Models.Db;
using MassTransit;

namespace HerzenHelper.RightsService.Broker.Consumers
{
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
}
