using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        partial void PrepareRequest(HttpClient client, HttpRequestMessage request, string url)
        {
            if (!string.IsNullOrWhiteSpace(_apiKey))
            {
                // Append `key` query parameter to each request with _apiKey
                url += GetKeyQueryParameter(new Uri(url));
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

        public async Task<ICollection<Sense>> GetSensesAsync(
            string lemma,
            string searchLang,
            CancellationToken cancellationToken = default)
        {
            var res = await GetSensesAsync(lemma, searchLang, Enumerable.Empty<string>(), null, null, cancellationToken).ConfigureAwait(false);
            return res.Select(sr => sr.Sense).ToList();
        }

        public async Task<ICollection<Sense>> GetSensesAsync(
            string lemma,
            string searchLang,
            string targetLang,
            UniversalPOS? pos = null,
            string? source = null,
            CancellationToken cancellationToken = default)
        {
            var res = await GetSensesAsync(
                lemma,
                searchLang,
                new[] { targetLang },
                pos,
                source,
                cancellationToken).ConfigureAwait(false);
            return res.Select(sr => sr.Sense).ToList();
        }


        async Task<ICollection<Sense>> IBabelNetApiClient.GetSensesAsync(string lemma, string searchLang, IEnumerable<string> targetLang, UniversalPOS? pos, string source)
        {
            var res = await GetSensesAsync(lemma, searchLang, targetLang, pos, source).ConfigureAwait(false);
            return res.Select(sr => sr.Sense).ToList();
        }

        async Task<ICollection<Sense>> IBabelNetApiClient.GetSensesAsync(string lemma, string searchLang, IEnumerable<string> targetLang, UniversalPOS? pos, string source, CancellationToken cancellationToken)
        {
            var res = await GetSensesAsync(lemma, searchLang, targetLang, pos, source, cancellationToken).ConfigureAwait(false);
            return res.Select(sr => sr.Sense).ToList();
        }

        async Task<ISynset> IBabelNetApiClient.GetSynsetAsync(string id, IEnumerable<string> targetLang, CancellationToken cancellationToken)
        {
            return await GetSynsetAsync(id, targetLang, cancellationToken).ConfigureAwait(false);
        }

        #endregion
    }
}