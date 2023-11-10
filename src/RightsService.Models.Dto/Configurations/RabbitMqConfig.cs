using UniversityHelper.Core.BrokerSupport.Attributes;
using UniversityHelper.Core.BrokerSupport.Configurations;
using UniversityHelper.Models.Broker.Common;
using UniversityHelper.Models.Broker.Requests.User;

namespace UniversityHelper.RightsService.Models.Dto.Configurations
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
