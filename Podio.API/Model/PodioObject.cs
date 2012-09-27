using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Podio.API.Model
{
    public interface IPodioObject {
         string JSON { get; set; }
    }

    [DataContract]
    public class PodioObject : IPodioObject
    {
        public string JSON { get; set; }
    }
}
