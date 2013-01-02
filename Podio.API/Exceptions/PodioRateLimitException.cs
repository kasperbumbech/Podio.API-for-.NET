using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Podio.API.Exceptions
{
    public class PodioRateLimitException : Exception
    {
        public PodioRateLimitException()
            : base("Podio rate limit exceeded. https://developers.podio.com/index/limits")
        {
           
        }
    }
}
