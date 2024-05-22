using Automatonymous;
using Magic8Ball.Application.Dto;
using Magic8Ball.Domain.Entities;
using Magic8Ball.Infra.Data;
using Magic8Ball.Integration.Tests.Configs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Magic8Ball.Integration.Tests.Controllers
{
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class AnswerControllerTest
    {
        private readonly IntegrationTestsFixture _testsFixture;
        private readonly DefaultDbContext _dbContext;

        public AnswerControllerTest(IntegrationTestsFixture testsFixture)
        {
            _testsFixture = testsFixture;
            _dbContext = testsFixture.TestServer.Host.Services.GetService(typeof(DefaultDbContext)) as DefaultDbContext;
        }

        [Fact]
        public async Task GetById_Test()
        {
            //Arrange
            var answerId = 1;

            var queryString = HttpUtility.ParseQueryString(string.Empty);

            var httpRequestMessage = PrepareHttpRequestMessage($"answer/{answerId}?{queryString}", HttpMethod.Get);

            //Act
            var response = await _testsFixture.Client.SendAsync(httpRequestMessage);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var result = JsonConvert.DeserializeObject<AnswerDto>(await response.Content.ReadAsStringAsync());

            Assert.NotNull(result);
            Assert.True(result.Id == answerId);
        }

        private HttpRequestMessage PrepareHttpRequestMessage(string path, HttpMethod httpMethod, StringContent content = null)
        {
            return new HttpRequestMessage
            {
                Method = httpMethod,
                RequestUri = new Uri(_testsFixture.Client.BaseAddress + path),
                Headers = {
                },
                Content = content
            };
        }
    }
}
