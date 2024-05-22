using FluentAssertions;
using FluentValidation.Results;
using Magic8Ball.Application.UseCases.Answer;
using Magic8Ball.Application.UseCases.Log;
using Magic8Ball.Domain.Common;
using Magic8Ball.Domain.Repositories;
using Magic8Ball.Tests.Mock;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic8Ball.Tests.Application.UseCase
{
    public class LogUserVaseTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ILogRepository> _mockLogRepository;

        public LogUserVaseTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockLogRepository = new Mock<ILogRepository>();
        }


        private ValidationResult _validationResultFail = new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Property", "Error") });
        private ValidationResult _validationResultOk = new ValidationResult(new List<ValidationFailure> { });

        [Fact]
        public async Task ShouldReturnResponse_WhenLogExists()
        {
            // Arrange
            var services = new ServiceCollection();
            var serviceProvider = services
               .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetLogsHandler).Assembly))
               .AddScoped<ILogRepository, MockLogRepository>()
               .AddScoped<IUnitOfWork, MockUnitOfWork>()
               .BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();
            var query = new GetLogsRequest() {};

            //Act
            ObjectResult response = await mediator.Send(query) as ObjectResult;

            // Assert
            response.StatusCode.Should().Be(200);

        }
    }
}
