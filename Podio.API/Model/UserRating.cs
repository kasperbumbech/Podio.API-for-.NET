using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Podio.API.Model
{
    [DataContract]
    public class UserRating
    {
        [DataMember(Name = "rating_id")]
        public int RatingId { get; set; }

        [DataMember(Name = "type")] // "type": The type of rating, either "approved", "rsvp", "fivestar", "yesno", "thumbs" or "like",
        public string Type { get; set; }

        [DataMember(Name = "value")] //  "value": The value of the rating, see the rating area for details
        public string Value { get; set; }
    }
}
