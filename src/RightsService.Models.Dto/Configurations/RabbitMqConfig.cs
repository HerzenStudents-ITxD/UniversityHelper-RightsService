using HerzenHelper.Core.BrokerSupport.Attributes;
using HerzenHelper.Core.BrokerSupport.Configurations;
using HerzenHelper.Models.Broker.Common;
using HerzenHelper.Models.Broker.Requests.User;

namespace HerzenHelper.RightsService.Models.Dto.Configurations
{
  public class RabbitMqConfig : BaseRabbitMqConfig
  {
    public string GetUserRolesEndpoint { get; set; }
    public string CreateUserRoleEndpoint { get; set; }
    public string DisactivateUserRoleEndpoint { get; set; }
    public string ActivateUserRoleEndpoint { get; set; }
    public string FilterRolesEndpoint { get; set; }

    // users

    [AutoInjectRequest(typeof(IGetUsersDataRequest))]
    public string GetUsersDataEndpoint { get; set; }

    [AutoInjectRequest(typeof(ICheckUsersExistence))]
    public string CheckUsersExistenceEndpoint { get; set; }
  }
}
