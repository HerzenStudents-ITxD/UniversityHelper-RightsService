using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityHelper.Core.BrokerSupport.Broker;
using UniversityHelper.Core.RedisSupport.Configurations;
using UniversityHelper.Core.RedisSupport.Constants;
using UniversityHelper.Core.RedisSupport.Extensions;
using UniversityHelper.Core.RedisSupport.Helpers.Interfaces;
using UniversityHelper.Models.Broker.Models.Right;
using UniversityHelper.Models.Broker.Requests.Rights;
using UniversityHelper.Models.Broker.Responses.Rights;
using UniversityHelper.RightsService.Data.Interfaces;
using UniversityHelper.RightsService.Models.Db;
using MassTransit;
using Microsoft.Extensions.Options;

namespace UniversityHelper.RightsService.Broker.Consumers
{
  public class FilterRolesUsersConsumer : IConsumer<IFilterRolesRequest>
  {
    private readonly IRoleRepository _repository;
    private readonly IOptions<RedisConfig> _redisConfig;
    private readonly IGlobalCacheRepository _globalCache;

    private async Task<List<RoleFilteredData>> GetRolesDataAsync(IFilterRolesRequest request)
    {
      List<DbRole> roles = await _repository.GetAsync(request.RolesIds);

      return roles.Select(r =>
        new RoleFilteredData(
          r.Id,
          r.RoleLocalizations.Where(rl => rl.RoleId == r.Id).Select(rl => rl.Name).FirstOrDefault(),
          r.Users.Select(u => u.UserId).ToList()))
      .ToList();
    }

    public FilterRolesUsersConsumer(
      IRoleRepository repository,
      IOptions<RedisConfig> redisConfig,
      IGlobalCacheRepository globalCache)
    {
      _repository = repository;
      _redisConfig = redisConfig;
      _globalCache = globalCache;
    }

    public async Task Consume(ConsumeContext<IFilterRolesRequest> context)
    {
      List<RoleFilteredData> rolesFilteredData = await GetRolesDataAsync(context.Message);

      await context.RespondAsync<IOperationResult<IFilterRolesResponse>>(
        OperationResultWrapper.CreateResponse((_) => IFilterRolesResponse.CreateObj(rolesFilteredData), context));

      if (rolesFilteredData is not null)
      {
        await _globalCache.CreateAsync(
          Cache.Rights,
          context.Message.RolesIds.GetRedisCacheKey(nameof(IFilterRolesRequest), context.Message.GetBasicProperties()),
          rolesFilteredData,
          context.Message.RolesIds,
          TimeSpan.FromMinutes(_redisConfig.Value.CacheLiveInMinutes));
      }
    }
  }
}
