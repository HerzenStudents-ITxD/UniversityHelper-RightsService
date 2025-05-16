using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using UniversityHelper.Core.RedisSupport.Extensions;
using HealthChecks.UI.Client;
using UniversityHelper.Core.BrokerSupport.Configurations;
using UniversityHelper.Core.BrokerSupport.Extensions;
using UniversityHelper.Core.BrokerSupport.Middlewares.Token;
using UniversityHelper.Core.Configurations;
using UniversityHelper.Core.EFSupport.Extensions;
using UniversityHelper.Core.EFSupport.Helpers;
using UniversityHelper.Core.Extensions;
using UniversityHelper.Core.Middlewares.ApiInformation;
using UniversityHelper.Core.RedisSupport.Configurations;
using UniversityHelper.Core.RedisSupport.Constants;
using UniversityHelper.Core.RedisSupport.Helpers;
using UniversityHelper.Core.RedisSupport.Helpers.Interfaces;
using UniversityHelper.RightsService.Data.Provider.MsSql.Ef;
using UniversityHelper.RightsService.Models.Dto.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace UniversityHelper.RightsService;

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
    Version = "2.0.2.0";
    Description = "RightsService is an API intended to work with the user rights.";
    StartTime = DateTime.UtcNow;
    ApiName = $"UniversityHelper - {_serviceInfoConfig.Name}";
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
              .AllowAnyMethod()
              .AllowCredentials();
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

    services.AddSwaggerGen(options =>
    {
      options.SwaggerDoc($"{Version}", new OpenApiInfo
      {
        Version = Version,
        Title = _serviceInfoConfig.Name,
        Description = Description
      });

      options.EnableAnnotations();
    });
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

    app.UseSwagger()
      .UseSwaggerUI(options =>
      {
        options.SwaggerEndpoint($"/swagger/{Version}/swagger.json", $"{Version}");
      });
  }
}
