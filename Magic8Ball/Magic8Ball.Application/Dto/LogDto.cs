using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic8Ball.Application.Dto
{
    public record LogDto(int Id, DateTime CreatedAt, string LogMessage, string EndPoint, string HttpVerb)
    {
        public string PayloadRequest { get; set; } = default!;
        public string PayloadResponse { get; set; } = default!;
        public double ElapsedTime { get; set; }
    }
}
