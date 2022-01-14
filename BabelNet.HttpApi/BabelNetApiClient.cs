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

    /// <summary>
    /// Optionally set to a list in order to log request messages
    /// </summary>
    public IList<HttpRequestMessage> RequestLog { get; set; }

    /// <summary>
    /// Optionally set to a list in order to log response messages
    /// </summary>
    public IList<HttpResponseMessage> ResponseLog { get; set; }

    partial void PrepareRequest(HttpClient client, HttpRequestMessage request, string url)
    {
        if (!string.IsNullOrWhiteSpace(_apiKey))
        {
            // Append `key` query parameter to each request with _apiKey
            Uri uri = new Uri(url);
            request.RequestUri = new Uri(uri.ToString() + GetKeyQueryParameter(uri));
        }

        if (RequestLog != null)
        {
            RequestLog.Add(request);
        }

        string GetKeyQueryParameter(Uri uri)
        {
            return (string.IsNullOrWhiteSpace(uri.Query) ? "?" : "&") + "key=" + _apiKey;
        }
    }

    partial void ProcessResponse(HttpClient client, HttpResponseMessage response)
    {
        if (ResponseLog != null)
        {
            ResponseLog.Add(response);
        }
    }

    public Task<ICollection<Synset>> GetSynsetIdsAsync(string lemma, string searchLang)
    {
        return GetSynsetIdsAsync(
            lemma,
            new[] { searchLang },
            null,
            null,
            null);
    }

    public Task<ICollection<Synset>> GetSynsetIdsAsync(string lemma, IEnumerable<string> searchLangs)
    {
        return GetSynsetIdsAsync(
            lemma,
            searchLangs,
            null,
            null,
            null);
    }

    public Task<ICollection<Synset>> GetSynsetIdsAsync(
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
}
