using FluentAssertions;
using FluentAssertions.Common;
using FluentValidation;
using FluentValidation.Results;
using Magic8Ball.Application.UseCases.Answer;
using Magic8Ball.Domain.Common;
using Magic8Ball.Domain.Entities;
using Magic8Ball.Domain.Repositories;
using Magic8Ball.Tests.Fakers;
using Magic8Ball.Tests.Mock;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Magic8Ball.Tests.Application.UseCase
{
    public class AnswerUserCaseTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IAnswerRepository> _mockAnswerRepository;

        public AnswerUserCaseTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockAnswerRepository = new Mock<IAnswerRepository>();
        }


        private ValidationResult _validationResultFail = new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Property", "Error") });
        private ValidationResult _validationResultOk = new ValidationResult(new List<ValidationFailure> { });

        [Fact]
        public async Task ShouldReturnNotFound_WhenAnswerIdDoesNotExist()
        {
            // Arrange
            var services = new ServiceCollection();
            var serviceProvider = services
               .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateAnswerHandler).Assembly))
               .AddScoped<IAnswerRepository, MockAnswerRepository>()
               .AddScoped<ILogRepository, MockLogRepository>()
               .AddScoped<IUnitOfWork, MockUnitOfWork>()
               .BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();
            //var answer = AnswerFaker.Gemerate(1).First();
            var query = new GetAnswerRequest() { Id = 172983 };

            //Act
            ObjectResult response = await mediator.Send(query) as ObjectResult;

            // Assert
            response.StatusCode.Should().Be(404);

        }

        [Fact]
        public async Task ShouldReturnResponse_WhenAnswerIdExists()
        {
            // Arrange
            var services = new ServiceCollection();
            var serviceProvider = services
               .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateAnswerHandler).Assembly))
               .AddScoped<IAnswerRepository, MockAnswerRepository>()
               .AddScoped<ILogRepository, MockLogRepository>()
               .AddScoped<IUnitOfWork, MockUnitOfWork>()
               .BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();
            //var answer = AnswerFaker.Gemerate(1).First();
            var query = new GetAnswerRequest() { Id = 1 };

            //Act
            ObjectResult response = await mediator.Send(query) as ObjectResult;

            // Assert
            response.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task ShouldReturnResponse_WhenCreateNewAnswer()
        {
            // Arrange
            var services = new ServiceCollection();
            var serviceProvider = services
               .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateAnswerHandler).Assembly))
               .AddScoped<IAnswerRepository, MockAnswerRepository>()
               .AddScoped<ILogRepository, MockLogRepository>()
               .AddScoped<IUnitOfWork, MockUnitOfWork>()
               .BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();
            var answer = AnswerFaker.Gemerate(1).First();
            var query = new CreateAnswerRequest() { Message = answer.Message, AnswerType = answer.Type.ToString() };

            //Act
            ObjectResult response = await mediator.Send(query) as ObjectResult;

            // Assert
            response.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task ShouldReturnResponse_WhenYesNoQuestion()
        {
            // Arrange
            var services = new ServiceCollection();
            var serviceProvider = services
               .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateAnswerHandler).Assembly))
               .AddScoped<IAnswerRepository, MockAnswerRepository>()
               .AddScoped<ILogRepository, MockLogRepository>()
               .AddScoped<IUnitOfWork, MockUnitOfWork>()
               .BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();
            var answer = AnswerFaker.Gemerate(1).First();
            var query = new GetYesNoQuestionAnswerRequest();

            //Act
            ObjectResult response = await mediator.Send(query) as ObjectResult;

            // Assert
            response.StatusCode.Should().Be(200);
        }
    }
}
