using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using System.Linq;

namespace BabelNet.HttpApi.Tests;

/// <summary>
/// Tests for additional code added to generated code via partial class
/// BabelNet.HttpApi\BabelNetApiClient.cs
/// </summary>
public class BabelNetApiClientTests
{
    private const string _apiKey = Secrets.ApiKey;

    private HttpClient _httpClient;
    private LoggingBabelNetApiClient _apiClient;

    [SetUp]
    public void Setup()
    {
        _httpClient = new HttpClient();
        _apiClient = new(_httpClient, _apiKey)
        {
            RequestLog = new List<HttpRequestMessage>(),
            ResponseLog = new List<HttpResponseMessage>(),
        };
    }

    [TearDown]
    public void TearDown()
    {
        _httpClient.Dispose();
    }

    [Test]
    public async Task GetVersion()
    {
        var res = await _apiClient.GetVersionAsync();

        _apiClient.RequestLog.Count.Should().Be(1);
        _apiClient.RequestLog[0].Method.Should().Be(HttpMethod.Get);
        _apiClient.RequestLog[0].RequestUri.Should().Be(
            $"https://babelnet.io/v6/getVersion?key={_apiKey}");

        res.Should().NotBeNull();
        res.Version.Should().Be("V5_0");
    }

    [Test]
    public async Task GetSynsetIds_WithArgs_lemma_searchLang()
    {
        const string lemma = "apple";
        const string searchLang = "EN";
        var res = await _apiClient.GetSynsetIdsAsync(lemma, searchLang);

        _apiClient.RequestLog.Count.Should().Be(1);
        _apiClient.RequestLog[0].Method.Should().Be(HttpMethod.Get);
        _apiClient.RequestLog[0].RequestUri.Should().Be(
            $"https://babelnet.io/v6/getSynsetIds?lemma={lemma}&searchLang={searchLang}&key={_apiKey}");

        Assert.IsNotNull(res);
    }

    [Test]
    public async Task GetSynsetIds_WithArgs_lemma_searchLangs()
    {
        const string lemma = "apple";
        var searchLangs = new[] { "EN", "DE", "ES" };
        var res = await _apiClient.GetSynsetIdsAsync(lemma, searchLangs);

        _apiClient.RequestLog.Count.Should().Be(1);
        _apiClient.RequestLog[0].Method.Should().Be(HttpMethod.Get);
        _apiClient.RequestLog[0].RequestUri.Should().Be(
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
        var res = await _apiClient.GetSynsetIdsAsync(lemma, searchLang, targetLang, pos, "BABELNET");

        _apiClient.RequestLog.Count.Should().Be(1);
        _apiClient.RequestLog[0].Method.Should().Be(HttpMethod.Get);
        _apiClient.RequestLog[0].RequestUri.Should().Be(
            $"https://babelnet.io/v6/getSynsetIds?lemma={lemma}&searchLang={searchLang}&targetLang={targetLang}&pos={pos}&source={source}&key={_apiKey}");

        Assert.IsNotNull(res);
    }

    [Test]
    public async Task GetSyncSetIds_WithArgs_lemma_searchLangs_targetLangs_pos_source()
    {
        const string lemma = "apple";
        var searchLangs = new[] { "EN", "DE", "ES" };
        var targetLangs = new[] { "FR", "IT" };
        const UniversalPOS pos = UniversalPOS.NOUN;
        const string source = "BABELNET";
        var res = await _apiClient.GetSynsetIdsAsync(lemma, searchLangs, targetLangs, pos, "BABELNET");

        _apiClient.RequestLog.Count.Should().Be(1);
        _apiClient.RequestLog[0].Method.Should().Be(HttpMethod.Get);
        _apiClient.RequestLog[0].RequestUri.Should().Be(
            $"https://babelnet.io/v6/getSynsetIds?lemma={lemma}&searchLang={string.Join("&searchLang=", searchLangs)}&targetLang={string.Join("&targetLang=", targetLangs)}&pos={pos}&source={source}&key={_apiKey}");

        Assert.IsNotNull(res);
    }

    [Test]
    public void GetSyncSetIds_WithArgs_lemma_emptySearchLangs_emptyTargetLangs()
    {
        const string lemma = "apple";

        // Empty searchLang will result in error from API
        Assert.ThrowsAsync<ApiException<Response2>>(async () => 
            await _apiClient.GetSynsetIdsAsync(lemma, Enumerable.Empty<string>(), Enumerable.Empty<string>(), null, null));

        _apiClient.RequestLog.Count.Should().Be(1);
        _apiClient.RequestLog[0].Method.Should().Be(HttpMethod.Get);
        _apiClient.RequestLog[0].RequestUri.Should().Be(
            $"https://babelnet.io/v6/getSynsetIds?lemma={lemma}&key={_apiKey}");
    }
}
