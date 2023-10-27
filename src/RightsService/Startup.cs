using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using HerzenHelper.Core.RedisSupport.Extensions;
using HealthChecks.UI.Client;
using HerzenHelper.Core.BrokerSupport.Configurations;
using HerzenHelper.Core.BrokerSupport.Extensions;
using HerzenHelper.Core.BrokerSupport.Middlewares.Token;
using HerzenHelper.Core.Configurations;
using HerzenHelper.Core.EFSupport.Extensions;
using HerzenHelper.Core.EFSupport.Helpers;
using HerzenHelper.Core.Extensions;
using HerzenHelper.Core.Middlewares.ApiInformation;
using HerzenHelper.Core.RedisSupport.Configurations;
using HerzenHelper.Core.RedisSupport.Constants;
using HerzenHelper.Core.RedisSupport.Helpers;
using HerzenHelper.Core.RedisSupport.Helpers.Interfaces;
using HerzenHelper.RightsService.Data.Provider.MsSql.Ef;
using HerzenHelper.RightsService.Models.Dto.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace HerzenHelper.RightsService
{
  public class Startup : BaseApiInfo
  {
    public const string CorsPolicyName = "LtDoCorsPolicy";

    private readonly BaseServiceInfoConfig _serviceInfoConfig;
    private readonly RabbitMqConfig _rabbitMqConfig;

    private string _redisConnStr;

    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;

      _serviceInfoConfig = Configuration
        .GetSection(BaseServiceInfoConfig.SectionName)
        .Get<BaseServiceInfoConfig>();

      _rabbitMqConfig = Configuration
        .GetSection(BaseRabbitMqConfig.SectionName)
        .Get<RabbitMqConfig>();

      //App.Release.BreakChange.Version
      Version = "2.0.1.0";
      Description = "RightsService is an API intended to work with the user rights.";
      StartTime = DateTime.UtcNow;
      ApiName = $"HerzenHelper - {_serviceInfoConfig.Name}";
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors(options =>
      {
        options.AddPolicy(
          CorsPolicyName,
            builder =>
            {
              builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
      });

      services.Configure<TokenConfiguration>(Configuration.GetSection("CheckTokenMiddleware"));
      services.Configure<BaseRabbitMqConfig>(Configuration.GetSection(BaseRabbitMqConfig.SectionName));
      services.Configure<BaseServiceInfoConfig>(Configuration.GetSection(BaseServiceInfoConfig.SectionName));

      services.AddHttpContextAccessor();
      services.AddMemoryCache();
      services.AddControllers()
        .AddJsonOptions(options =>
        {
          options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        })
        .AddNewtonsoftJson();

      string dbConnectionString = ConnectionStringHandler.Get(Configuration);

      services.AddDbContext<RightsServiceDbContext>(options =>
      {
        options.UseSqlServer(dbConnectionString);
      });

      if (int.TryParse(Environment.GetEnvironmentVariable("MemoryCacheLiveInMinutes"), out int memoryCacheLifetime))
      {
        services.Configure<MemoryCacheConfig>(options =>
        {
          options.CacheLiveInMinutes = memoryCacheLifetime;
        });
      }
      else
      {
        services.Configure<MemoryCacheConfig>(Configuration.GetSection(MemoryCacheConfig.SectionName));
      }

      if (int.TryParse(Environment.GetEnvironmentVariable("RedisCacheLiveInMinutes"), out int redisCacheLifeTime))
      {
        services.Configure<RedisConfig>(options =>
        {
          options.CacheLiveInMinutes = redisCacheLifeTime;
        });
      }
      else
      {
        services.Configure<RedisConfig>(Configuration.GetSection(RedisConfig.SectionName));
      }

      services.AddBusinessObjects();
      services.AddTransient<IRedisHelper, RedisHelper>();
      services.AddTransient<ICacheNotebook, CacheNotebook>();

      _redisConnStr = services.AddRedisSingleton(Configuration);

      services.ConfigureMassTransit(_rabbitMqConfig);

      services
        .AddHealthChecks()
        .AddSqlServer(dbConnectionString)
        .AddRabbitMqCheck();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
      app.UpdateDatabase<RightsServiceDbContext>();

      FlushRedisDbHelper.FlushDatabase(_redisConnStr, Cache.Rights);

      app.UseForwardedHeaders();

      app.UseExceptionsHandler(loggerFactory);

      app.UseApiInformation();

      app.UseRouting();

      app.UseMiddleware<TokenMiddleware>();

      app.UseCors(CorsPolicyName);

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers().RequireCors(CorsPolicyName);

        endpoints.MapHealthChecks($"/{_serviceInfoConfig.Id}/hc", new HealthCheckOptions
        {
          ResultStatusCodes = new Dictionary<HealthStatus, int>
          {
            { HealthStatus.Unhealthy, 200 },
            { HealthStatus.Healthy, 200 },
            { HealthStatus.Degraded, 200 },
          },
          Predicate = check => check.Name != "masstransit-bus",
          ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
      });
    }
  }
}
