﻿using Magic8Ball.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;
using Serilog;
using Magic8Ball.Domain.Entities.Base;
using FluentValidation;
using Magic8Ball.Application.Validator;
using Magic8Ball.Application;

namespace Magic8Ball.Api.Extensions
{
    public static class Extensions
    {
        [DebuggerStepThrough]
        public static IServiceCollection AddCustomMediatR(this IServiceCollection services, Type[] types = null,
            Action<IServiceCollection> doMoreActions = null)
        {
            services.AddHttpContextAccessor();

            /*services.AddMediatR(types)
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>))
                //.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
                ;
            //services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
            */
            doMoreActions?.Invoke(services);

            return services;
        }

        [DebuggerStepThrough]
        public static IServiceCollection AddCustomValidators(this IServiceCollection services, Type[] types)
        {
            return services.Scan(scan => scan
                .FromAssembliesOf(types)
                .AddClasses(c => c.AssignableTo(typeof(IValidator<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            //return services.AddValidatorsFromAssemblies(types.Select(t => t.Assembly));
        }

        //public static IServiceCollection AddCustomDaprClient(this IServiceCollection services)
        //{
        //    services.AddDaprClient();
        //    return services;
        //}

        public static IServiceCollection AddCustomCors(this IServiceCollection services, string corsName = "api",
            Action<CorsOptions> optionsAction = null)
        {
            services.AddCors(options =>
            {
                if (optionsAction == null)
                {
                    options.AddPolicy(corsName,
                        policy => { policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
                }
                else
                {
                    optionsAction.Invoke(options);
                }
            });

            return services;
        }

        public static IApplicationBuilder UseCustomCors(this IApplicationBuilder app, string corsName = "api")
        {
            return app.UseCors(corsName);
        }

        [DebuggerStepThrough]
        public static TResult SafeGetListQuery<TResult, TResponse, TEntity>(this HttpContext httpContext, string query)
            where TResult : IListQuery<TEntity>, new()
            where TEntity : IEntity
        {
            var queryModel = new TResult();
            if (!(string.IsNullOrEmpty(query) || query == "{}"))
            {
                queryModel = JsonConvert.DeserializeObject<TResult>(query);
            }

            httpContext?.Response.Headers.Add("x-query",
                JsonConvert.SerializeObject(queryModel,
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));

            return queryModel;
        }

        [DebuggerStepThrough]
        public static string GetTraceId(this IHttpContextAccessor httpContextAccessor)
        {
            return Activity.Current?.TraceId.ToString() ?? httpContextAccessor?.HttpContext?.TraceIdentifier;
        }

        [DebuggerStepThrough]
        public static T ConvertTo<T>(this object input)
        {
            return ConvertTo<T>(input.ToString());
        }

        [DebuggerStepThrough]
        public static T ConvertTo<T>(this string input)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                return (T)converter.ConvertFromString(input);
            }
            catch (NotSupportedException)
            {
                return default;
            }
        }

        [DebuggerStepThrough]
        public static TModel GetOptions<TModel>(this IConfiguration configuration, string section) where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(section).Bind(model);
            return model;
        }

        public static async Task<int> RunAsync(this IHostBuilder hostBuilder, bool isRunOnTye = true)
        {
            try
            {
                await hostBuilder.Build().RunAsync();
                return 0;
            }
            catch (Exception exception)
            {
                if (isRunOnTye)
                {
                    Log.Fatal(exception, "Host terminated unexpectedly");
                }

                return 1;
            }
            finally
            {
                if (isRunOnTye)
                {
                    Log.CloseAndFlush();
                }
            }
        }
    }
}
