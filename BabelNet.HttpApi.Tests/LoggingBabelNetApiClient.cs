using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BabelNet.HttpApi.Tests
{
    internal class LoggingBabelNetApiClient : BabelNetApiClient
    {
        public LoggingBabelNetApiClient(HttpClient httpClient, string apiKey)
            : base(httpClient, apiKey)
        { }

        /// <summary>
        /// Optionally set to a list in order to log request messages
        /// </summary>
        public IList<HttpRequestMessage>? RequestLog { get; set; }

        /// <summary>
        /// Optionally set to a list in order to log response messages
        /// </summary>
        public IList<HttpResponseMessage>? ResponseLog { get; set; }

        protected override void OnRequesting(HttpClient client, HttpRequestMessage request, string url)
        {
            if (RequestLog != null)
            {
                RequestLog.Add(request);
            }
        }

        protected override void OnResponse(HttpClient client, HttpResponseMessage response)
        {
            if (ResponseLog != null)
            {
                ResponseLog.Add(response);
            }
        }
    }
}
