using System.Linq;
using System.Threading.Tasks;
using UniversityHelper.Core.BrokerSupport.AccessValidatorEngine.Requests;
using UniversityHelper.Core.BrokerSupport.Broker;
using UniversityHelper.RightsService.Data.Provider;
using MassTransit;

namespace UniversityHelper.RightsService.Broker.Consumers
{
  public class CheckUserRightsConsumer : IConsumer<ICheckUserRightsRequest>
  {
    private readonly IDataProvider _provider;

    private object HasRightAsync(ICheckUserRightsRequest request)
    {
      return request.RightIds.Intersect(
          (from user in _provider.UsersRoles
           where user.UserId == request.UserId && user.IsActive
           join role in _provider.Roles on user.RoleId equals role.Id
           where role.IsActive
           join rolesRights in _provider.RolesRights on role.Id equals rolesRights.RoleId
           select rolesRights.RightId)
          .AsEnumerable())
        .Count() == request.RightIds.Length;
    }

    public CheckUserRightsConsumer(IDataProvider provider)
    {
      _provider = provider;
    }

    public async Task Consume(ConsumeContext<ICheckUserRightsRequest> context)
    {
      var response = OperationResultWrapper.CreateResponse(HasRightAsync, context.Message);

      await context.RespondAsync<IOperationResult<bool>>(response);
    }
  }
}
