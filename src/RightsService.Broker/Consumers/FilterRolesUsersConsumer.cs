using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HerzenHelper.Core.BrokerSupport.Broker;
using HerzenHelper.Core.RedisSupport.Configurations;
using HerzenHelper.Core.RedisSupport.Constants;
using HerzenHelper.Core.RedisSupport.Extensions;
using HerzenHelper.Core.RedisSupport.Helpers.Interfaces;
using HerzenHelper.Models.Broker.Models.Right;
using HerzenHelper.Models.Broker.Requests.Rights;
using HerzenHelper.Models.Broker.Responses.Rights;
using HerzenHelper.RightsService.Data.Interfaces;
using HerzenHelper.RightsService.Models.Db;
using MassTransit;
using Microsoft.Extensions.Options;

namespace HerzenHelper.RightsService.Broker.Consumers
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
