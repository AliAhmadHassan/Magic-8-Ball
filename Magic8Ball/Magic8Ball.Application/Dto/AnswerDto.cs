using Magic8Ball.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic8Ball.Application.Dto
{
    public record AnswerDto(int Id, string Message, string AnswerType)
    {
    }
}
