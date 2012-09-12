using Podio.API.Model;
using Podio.API.Utils;
using System;
using System.Collections.Generic;

namespace Podio.API.Services
{
    public class StreamService
    {
        private Client _client;
        public StreamService(Client client) {
            _client = client;
        }
        /// <summary>
        /// https://developers.podio.com/doc/organizations/get-organizations-22344
        /// </summary>
        public IEnumerable<StreamObject> GetGlobalStream(int limit, int offset)
        {
            Dictionary<string,string> _args = new Dictionary<string,string>();
            _args.Add("limit",limit.ToString());
            _args.Add("offset",offset.ToString());
            return PodioRestHelper.Request<List<StreamObject>>(Constants.PODIOAPI_BASEURL + "/stream/", _client.AuthInfo.access_token, _args).Data;
        }


    }
}
