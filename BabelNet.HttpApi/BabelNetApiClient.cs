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
            string newQuery = string.Format("{0}{1}key={2}",
                uri.Query,
                string.IsNullOrWhiteSpace(uri.Query) ? "?" : "&",
                Uri.EscapeDataString(_apiKey));
            request.RequestUri = new Uri(uri.ToString() + newQuery);
        }
    }
}
