using Newtonsoft.Json;
using JsonSubTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace BabelNet.HttpApi
{
    public partial class BabelNetApiClient : IBabelNetApiClient
    {

        private string _apiKey = null;

        public BabelNetApiClient(HttpClient httpClient, string apiKey)
            : this(httpClient)
        {
            _apiKey = apiKey;
        }

        partial void UpdateJsonSerializerSettings(JsonSerializerSettings settings)
        {
            var converter = JsonSubtypesWithPropertyConverterBuilder
                                .Of<SenseCore>()
                                .RegisterSubtypeWithProperty<WordNetSense>(nameof(WordNetSense.WordNetSenseNumber))
                                .SetFallbackSubtype<BabelSense>()
                                .Build();
            settings.Converters.Add(converter);
        }

        partial void PrepareRequest(HttpClient client, HttpRequestMessage request, string url)
        {
            if (!string.IsNullOrWhiteSpace(_apiKey))
            {
                // Append `key` query parameter to each request with _apiKey
                url = url + GetKeyQueryParameter(new Uri(url));
                request.RequestUri = new Uri(url);
            }

            OnRequesting(client, request, url);

            string GetKeyQueryParameter(Uri uri)
            {
                return (string.IsNullOrWhiteSpace(uri.Query) ? "?" : "&") + "key=" + _apiKey;
            }
        }

        partial void ProcessResponse(HttpClient client, HttpResponseMessage response)
        {
            OnResponse(client, response);
        }

        protected virtual void OnRequesting(HttpClient client, HttpRequestMessage request, string url)
        { }

        protected virtual void OnResponse(HttpClient client, HttpResponseMessage response)
        { }

        #region GetSynsetIdAsync overloads

        public Task<ICollection<SynsetId>> GetSynsetIdsAsync(string lemma, string searchLang, CancellationToken cancellationToken = default)
        {
            return GetSynsetIdsAsync(
                lemma,
                new[] { searchLang },
                null,
                null,
                null,
                cancellationToken);
        }

        public Task<ICollection<SynsetId>> GetSynsetIdsAsync(string lemma, IEnumerable<string> searchLangs)
        {
            return GetSynsetIdsAsync(
                lemma,
                searchLangs,
                null,
                null,
                null);
        }

        public Task<ICollection<SynsetId>> GetSynsetIdsAsync(
            string lemma,
            string searchLang,
            string targetLang,
            UniversalPOS? pos = null,
            string? source = null,
            CancellationToken cancellationToken = default)
        {
            return GetSynsetIdsAsync(
                lemma,
                new[] { searchLang },
                new[] { targetLang },
                pos,
                source,
                cancellationToken);
        }

        #endregion

        #region GetSensesAsync overloads

        public async Task<ICollection<ISense>> GetSensesAsync(
            string lemma,
            string searchLang,
            CancellationToken cancellationToken = default)
        {
            return (await GetSensesAsync(lemma, searchLang, Enumerable.Empty<string>(), null, null, cancellationToken)).Cast<ISense>().ToList();
        }

        public async Task<ICollection<ISense>> GetSensesAsync(
            string lemma,
            string searchLang,
            string targetLang,
            UniversalPOS? pos = null,
            string? source = null,
            CancellationToken cancellationToken = default)
        {
            return (await GetSensesAsync(
                lemma,
                searchLang,
                new[] { targetLang },
                pos,
                source,
                cancellationToken)).Cast<ISense>().ToList();
        }


        async Task<ICollection<ISense>> IBabelNetApiClient.GetSensesAsync(string lemma, string searchLang, IEnumerable<string> targetLang, UniversalPOS? pos, string source)
        {
            return (await GetSensesAsync(lemma, searchLang, targetLang, pos, source)).Cast<ISense>().ToList();
        }

        async Task<ICollection<ISense>> IBabelNetApiClient.GetSensesAsync(string lemma, string searchLang, IEnumerable<string> targetLang, UniversalPOS? pos, string source, CancellationToken cancellationToken)
        {
            return (await GetSensesAsync(lemma, searchLang, targetLang, pos, source, cancellationToken)).Cast<ISense>().ToList();

        }

        #endregion
    }
}