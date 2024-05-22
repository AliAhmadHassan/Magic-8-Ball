using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic8Ball.Domain.Entities
{
    public class Log : Base.EntityBase
    {
        private string _payloadResponse = default!;

        public string LogMessage { get; set; } = default!;

        public string EndPoint { get; set; } = default!;

        public string HttpVerb { get; set; } = default!;

        public string PayloadRequest { get; set; } = default!;
        
        public string PayloadResponse
        {
            get { return _payloadResponse; }
            set
            {
                _payloadResponse = value;
                ElapsedTime = DateTime.Now.Subtract(CreatedAt).TotalMilliseconds;
            }
        }
        public double ElapsedTime { get; set; }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return LogMessage;
            yield return EndPoint;
            yield return HttpVerb;
            yield return PayloadRequest;
            yield return PayloadResponse;
        }
    }
}
