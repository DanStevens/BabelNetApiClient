using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.IO;
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

        public ITextWriter Log { get; set; }

        /// <summary>
        /// Optionally set to a list in order to log request messages
        /// </summary>
        public IList<HttpRequestMessage> RequestHistory { get; set; }

        /// <summary>
        /// Optionally set to a list in order to log response messages
        /// </summary>
        public IList<HttpResponseMessage> ResponseHistory { get; set; }

        protected override void OnRequesting(HttpClient client, HttpRequestMessage request, string url)
        {
            RequestHistory?.Add(request);
            Log?.WriteLine("Requesting {0}", url);
        }

        protected override void OnResponse(HttpClient client, HttpResponseMessage response)
        {
            ResponseHistory?.Add(response);
            Log?.WriteLine("Received {0} response of length {1} and type '{2}'", response.StatusCode, response.Content.Headers?.ContentLength?.ToString() ?? "?", response.Content.Headers?.ContentType?.ToString() ?? "?");
        }
    }
}
