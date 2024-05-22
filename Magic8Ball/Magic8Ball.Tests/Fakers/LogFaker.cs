using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic8Ball.Tests.Fakers
{
    public class LogFaker
    {
        public static IEnumerable<Domain.Entities.Log> Gemerate(int count)
        {
            var record = new Faker<Domain.Entities.Log>()
                .RuleFor(x => x.Id, (f, x) => ++f.IndexVariable)
                .RuleFor(x => x.IsActive, (f, x) => f.Random.Bool())
                .RuleFor(x => x.LogMessage, (f, x) => f.Random.String(100))
                .RuleFor(x => x.PayloadResponse, (f, x) => f.Random.String(100))
                .RuleFor(x => x.PayloadRequest, (f, x) => f.Random.String(100))
                .RuleFor(x => x.HttpVerb, (f, x) => CreateHttpVerb())
                .RuleFor(x => x.EndPoint, (f, x) => CreateEndPoint())
                .RuleFor(x => x.ElapsedTime, (f, x) => f.Random.Int(1))
                .RuleFor(x => x.CreatedAt, (f, x) => f.Date.Recent());

            return record.Generate(count);
        }

        private static string CreateHttpVerb()
        {
            Magic8Ball.Application.Enums.HttpVerb[] itens = Enum.GetValues<Magic8Ball.Application.Enums.HttpVerb>();
            var index = new Random().Next(0, itens.Length);
            return itens[index].ToString();
        }

        private static string CreateEndPoint()
        {
            Magic8Ball.Application.Enums.EndPoint[] itens = Enum.GetValues<Magic8Ball.Application.Enums.EndPoint>();
            var index = new Random().Next(0, itens.Length);
            return itens[index].ToString();
        }
    }
}
