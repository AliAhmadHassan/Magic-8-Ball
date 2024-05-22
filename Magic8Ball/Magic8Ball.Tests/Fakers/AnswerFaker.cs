using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic8Ball.Tests.Fakers
{
    public class AnswerFaker
    {
        public static IEnumerable<Domain.Entities.Answer> Gemerate(int count)
        {
            var record = new Faker<Domain.Entities.Answer>()
                .RuleFor(x => x.Id, (f, x) => ++f.IndexVariable)
                .RuleFor(x => x.IsActive, (f, x) => f.Random.Bool())
                .RuleFor(x => x.Message, (f, x) => f.Random.String(20))
                .RuleFor(x => x.CreatedAt, (f, x) => f.Date.Recent());

            return record.Generate(count);
        }
    }
}
