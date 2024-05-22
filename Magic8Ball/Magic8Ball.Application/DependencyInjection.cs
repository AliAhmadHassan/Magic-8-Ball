using MediatR.Pipeline;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Magic8Ball.Application.Validator;
using FluentValidation;
using Magic8Ball.Application.UseCases.Answer;
using Magic8Ball.Application.UseCases.Log;

namespace Magic8Ball.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            

            services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddTransient<IValidator<CreateAnswerRequest>, CreateAnswerRequest.CreateValidator>();
            services.AddTransient<IValidator<DeleteAnswerRequest>, DeleteAnswerRequest.DeleteValidator>();
            services.AddTransient<IValidator<GetAnswerRequest>, GetAnswerRequest.GetValidator>();
            services.AddTransient<IValidator<GetAnswersRequest>, GetAnswersRequest.GetAnswersValidator>();
            services.AddTransient<IValidator<GetYesNoQuestionAnswerRequest>, GetYesNoQuestionAnswerRequest.GetYesNoQuestionAnswerValidator>();
            services.AddTransient<IValidator<UpdateAnswerRequest>, UpdateAnswerRequest.UpdateAnswerValidator>();

            services.AddTransient<IValidator<GetLogsRequest>, GetLogsRequest.GetLogsValidator>();

            //services.AddScoped(typeof(IStreamRequestHandler<Sing, Song>), typeof(SingHandler));

            //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(GenericPipelineBehavior<,>));
            //services.AddScoped(typeof(IRequestPreProcessor<>), typeof(GenericRequestPreProcessor<>));
            //services.AddScoped(typeof(IRequestPostProcessor<,>), typeof(GenericRequestPostProcessor<,>));
            //services.AddScoped(typeof(IStreamPipelineBehavior<,>), typeof(GenericStreamPipelineBehavior<,>));

            return services;

        }
    }
}
