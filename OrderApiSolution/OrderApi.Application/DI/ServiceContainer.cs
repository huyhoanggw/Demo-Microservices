using eCommerce.SharedLibary.Logs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderApi.Application.Interface;
using OrderApi.Application.Service;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Application.DI
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection service, IConfiguration config)         {
            service.AddHttpClient<IOrderService, OrderService>(option =>
            {
                option.BaseAddress = new Uri(config["ApiGateway:BaseAddress"]!);
                option.Timeout = TimeSpan.FromSeconds(1);
            });
            var retryStrategy = new RetryStrategyOptions()
            {
                ShouldHandle = new PredicateBuilder().Handle<TaskCanceledException>(),
                BackoffType = DelayBackoffType.Constant,
                UseJitter = true,
                MaxRetryAttempts = 3,
                Delay = TimeSpan.FromMilliseconds(500),
                OnRetry = args =>
                {
                    string message = $"OnRetry , Attemp: {args.AttemptNumber} Outcome {args.Outcome} ";
                    LogException.LogToConsole(message);
                    LogException.LogToDebugger(message);
                    return ValueTask.CompletedTask;
                }
            };
            // add retry policy
            service.AddResiliencePipeline("my-retry-pipeline", builder =>
            {
                builder.AddRetry(retryStrategy);
            });
                
                     
                 
                return service;
        }
    }
}
