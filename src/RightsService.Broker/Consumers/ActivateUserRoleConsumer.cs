using System;
using System.Threading.Tasks;
using UniversityHelper.Models.Broker.Publishing;
using UniversityHelper.Core.RedisSupport.Helpers.Interfaces;
using UniversityHelper.RightsService.Data.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace UniversityHelper.RightsService.Broker.Consumers
{
  public class ActivateUserRoleConsumer : IConsumer<IActivateUserPublish>
  {
    private readonly IUserRoleRepository _repository;
    private readonly IGlobalCacheRepository _globalCache;
    private readonly ILogger<ActivateUserRoleConsumer> _logger;

    public ActivateUserRoleConsumer(
      IUserRoleRepository repository,
      IGlobalCacheRepository globalCache,
      ILogger<ActivateUserRoleConsumer> logger)
    {
      _repository = repository;
      _globalCache = globalCache;
      _logger = logger;
    }

    public async Task Consume(ConsumeContext<IActivateUserPublish> context)
    {
      Guid? roleId = await _repository.ActivateAsync(context.Message);

      if (roleId.HasValue)
      {
        await _globalCache.RemoveAsync(roleId.Value);

        _logger.LogInformation("UserId '{UserId}' activated in roleId '{RoleId}'", context.Message.UserId, roleId);
      }
    }
  }
}
