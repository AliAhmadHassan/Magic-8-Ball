using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic8Ball.Application.Errors
{
    public class ProblemDetailsFields : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public List<FieldError> FieldErrors { get; set; }
    }
}
