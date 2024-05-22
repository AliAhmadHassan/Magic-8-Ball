using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic8Ball.Application.Models
{
    public class GeneralResponse
    {
        /// <summary>
        /// Error code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Description of error
        /// </summary>
        public string Message { get; set; }
    }
}
