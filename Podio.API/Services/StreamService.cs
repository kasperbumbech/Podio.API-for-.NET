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
        /// https://developers.podio.com/doc/stream/get-global-stream-80012
        /// </summary>
        public IEnumerable<StreamObject> GetGlobalStream(int limit, int offset)
        {
            Dictionary<string,string> _args = new Dictionary<string,string>();
            _args.Add("limit",limit.ToString());
            _args.Add("offset",offset.ToString());
            return PodioRestHelper.Request<List<StreamObject>>(Constants.PODIOAPI_BASEURL + "/stream/", _client.AuthInfo.AccessToken, _args).Data;
        }


        /// <summary>
        /// https://developers.podio.com/doc/stream/get-personal-stream-1656647
        /// </summary>
        public IEnumerable<StreamObject> GetPersonalStream(int limit, int offset)
        {
            Dictionary<string, string> _args = new Dictionary<string, string>();
            _args.Add("limit", limit.ToString());
            _args.Add("offset", offset.ToString());
            return PodioRestHelper.Request<List<StreamObject>>(Constants.PODIOAPI_BASEURL + "/stream/personal/", _client.AuthInfo.AccessToken, _args).Data;
        }


        /// <summary>
        /// https://developers.podio.com/doc/stream/get-user-stream-1289318
        /// </summary>
        public IEnumerable<StreamObject> GetUserStream(int userId, int limit, int offset)
        {
            Dictionary<string, string> _args = new Dictionary<string, string>();
            _args.Add("limit", limit.ToString());
            _args.Add("offset", offset.ToString());
            return PodioRestHelper.Request<List<StreamObject>>(Constants.PODIOAPI_BASEURL + "/stream/user/" + userId + "/", _client.AuthInfo.AccessToken, _args).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/stream/get-app-stream-264673
        /// </summary>
        public IEnumerable<StreamObject> GetAppStream(int appId,int limit, int offset)
        {
            Dictionary<string, string> _args = new Dictionary<string, string>();
            _args.Add("limit", limit.ToString());
            _args.Add("offset", offset.ToString());
            return PodioRestHelper.Request<List<StreamObject>>(Constants.PODIOAPI_BASEURL + "/stream/app/" + appId + "/", _client.AuthInfo.AccessToken, _args).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/stream/get-space-stream-80039
        /// </summary>
        public IEnumerable<StreamObject> GetSpaceStream(int spaceId, int limit, int offset)
        {
            Dictionary<string, string> _args = new Dictionary<string, string>();
            _args.Add("limit", limit.ToString());
            _args.Add("offset", offset.ToString());
            return PodioRestHelper.Request<List<StreamObject>>(Constants.PODIOAPI_BASEURL + "/stream/space/" + spaceId + "/", _client.AuthInfo.AccessToken, _args).Data;
        }


        /*
         * 
        Get global stream
        Get mutes in global stream
        Get organization stream
        Get personal stream
        Get stream object
        Get user stream
        Mute object from global stream
        Unmute objects from the global stream
        
    */

    }
}
