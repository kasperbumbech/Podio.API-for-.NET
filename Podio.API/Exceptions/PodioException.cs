using Podio.API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podio.API.Exceptions
{
    public class PodioResponseException : Exception
    {
        public PodioRestHelper.PodioError Error { get; private set; }

        public PodioResponseException(string message, PodioRestHelper.PodioError error) : base(message)
        {
            Error = error;
        }
    }
}
