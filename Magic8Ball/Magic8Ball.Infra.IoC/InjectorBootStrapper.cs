using FluentValidation;
using Magic8Ball.Application.UseCases.Answer;
using Magic8Ball.Domain.Common;
using Magic8Ball.Domain.Repositories;
using Magic8Ball.Infra.Data.Common;
using Magic8Ball.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;


namespace Magic8Ball.Infra.IoC
{
    public static class InjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Application

            //services.AddTransient<IValidator<CreateAnswerRequest>, CreateAnswerRequest.CreateValidator>();
            //services.AddTransient<IValidator<GetAnswerRequest>, GetAnswerRequest.GetValidator>();


            // Infra - Data
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            // Infra - Caching (singleton)
            //services.AddSingleton<ICache<Answer>, MemoryCache<Answer>>();

            // Infra - External Services

        }
    }
}
