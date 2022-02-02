# BabelNet API Client
A .NET 5.0 client for the [BabelNet HTTP API](https://babelnet.org/guide). Requires registration with BabelNet for an API Key.

## Example usage

```csharp
using BabelNet.HttpApi;

// The BabelNet API Key - requires registration (see https://babelnet.org/guide)
string apiKey = "00000000-0000-0000-0000-000000000000";
using var httpClient = new System.Net.Http.HttpClient();
IBabelNetApiClient apiClient = new BabelNetApiClient(httpClient, apiKey);
var senses = await apiClient.GetSensesAsync("apple", "EN");
var synsetId = senses.First().SynsetID.Id;
var synset = await apiClient.GetSynsetAsync(synsetId, new[] {"EN"});
//...
```
