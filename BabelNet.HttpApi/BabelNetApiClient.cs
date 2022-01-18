using System.Text;

namespace BabelNet.HttpApi;

public partial class BabelNetApiClient
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
            Uri uri = new Uri(url);
            request.RequestUri = new Uri(uri.ToString() + GetKeyQueryParameter(uri));
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

    public Task<ICollection<SynsetId>> GetSynsetIdsAsync(string lemma, string searchLang)
    {
        return GetSynsetIdsAsync(
            lemma,
            new[] { searchLang },
            null,
            null,
            null);
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
        string? targetLang = null,
        UniversalPOS? pos = null,
        string? source = null)
    {
        return GetSynsetIdsAsync(
            lemma,
            new[] { searchLang },
            new[] { targetLang },
            pos,
            source);
    }

    protected virtual void OnRequesting(HttpClient client, HttpRequestMessage request, string url)
    { }

    protected virtual void OnResponse(HttpClient client, HttpResponseMessage response)
    { }
}
