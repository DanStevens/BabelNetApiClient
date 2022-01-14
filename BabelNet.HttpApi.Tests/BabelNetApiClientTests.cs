using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;

namespace BabelNet.HttpApi.Tests;

public class BabelNetApiClientTests
{
    private const string _apiKey = Secrets.ApiKey;

    private HttpClient httpClient;
    private BabelNetApiClient apiClient;

    [SetUp]
    public void Setup()
    {
        httpClient = new HttpClient();
        apiClient = new(httpClient, _apiKey)
        {
            RequestLog = new List<HttpRequestMessage>(),
            ResponseLog = new List<HttpResponseMessage>(),
        };
    }

    [TearDown]
    public void TearDown()
    {
        httpClient.Dispose();
    }

    [Test]
    public async Task GetVersion()
    {
        var res = await apiClient.GetVersionAsync();

        apiClient.RequestLog.Count.Should().Be(1);
        apiClient.RequestLog[0].Method.Should().Be(HttpMethod.Get);
        apiClient.RequestLog[0].RequestUri.Should().Be(
            $"https://babelnet.io/v6/getVersion?key={_apiKey}");

        res.Should().NotBeNull();
        res.Version.Should().Be("V5_0");
    }

    [Test]
    public async Task GetSynsetIds_WithArgs_lemma_searchLang()
    {
        const string lemma = "apple";
        const string searchLang = "EN";
        var res = await apiClient.GetSynsetIdsAsync(lemma, searchLang);

        apiClient.RequestLog.Count.Should().Be(1);
        apiClient.RequestLog[0].Method.Should().Be(HttpMethod.Get);
        apiClient.RequestLog[0].RequestUri.Should().Be(
            $"https://babelnet.io/v6/getSynsetIds?lemma={lemma}&searchLang={searchLang}&key={_apiKey}");

        Assert.IsNotNull(res);
    }

    [Test]
    public async Task GetSynsetIds_WithArgs_lemma_searchLangs()
    {
        const string lemma = "apple";
        var searchLangs = new[] { "EN", "DE", "ES" };
        var res = await apiClient.GetSynsetIdsAsync(lemma, searchLangs);

        apiClient.RequestLog.Count.Should().Be(1);
        apiClient.RequestLog[0].Method.Should().Be(HttpMethod.Get);
        apiClient.RequestLog[0].RequestUri.Should().Be(
            $"https://babelnet.io/v6/getSynsetIds?lemma={lemma}&searchLang={string.Join("&searchLang=", searchLangs)}&key={_apiKey}");

        Assert.IsNotNull(res);
    }

    [Test]
    public async Task GetSyncSetIds_WithArgs_lemma_searchLang_targetLang_pos_source()
    {
        const string lemma = "apple";
        const string searchLang = "EN";
        const string targetLang = "DE";
        const UniversalPOS pos = UniversalPOS.NOUN;
        const string source = "BABELNET";
        var res = await apiClient.GetSynsetIdsAsync(lemma, searchLang, targetLang, pos, "BABELNET");

        apiClient.RequestLog.Count.Should().Be(1);
        apiClient.RequestLog[0].Method.Should().Be(HttpMethod.Get);
        apiClient.RequestLog[0].RequestUri.Should().Be(
            $"https://babelnet.io/v6/getSynsetIds?lemma={lemma}&searchLang={searchLang}&targetLang={targetLang}&pos={pos}&source={source}&key={_apiKey}");

        Assert.IsNotNull(res);
    }
}
