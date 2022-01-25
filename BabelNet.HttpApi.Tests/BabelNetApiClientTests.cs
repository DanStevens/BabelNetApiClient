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
    private IBabelNetApiClient _apiClient;

    private IList<HttpRequestMessage> ApiClientRequestHistory =>
        ((LoggingBabelNetApiClient)_apiClient).RequestHistory;

    private IList<HttpResponseMessage> ApiClientResponseHistory =>
        ((LoggingBabelNetApiClient)_apiClient).ResponseHistory;

    [SetUp]
    public void Setup()
    {
        _httpClient = new HttpClient();
        _apiClient = new LoggingBabelNetApiClient(_httpClient, _apiKey)
        {
            RequestHistory = new List<HttpRequestMessage>(),
            ResponseHistory = new List<HttpResponseMessage>(),
            Log = new TestContextTextWriter(),
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

        ApiClientRequestHistory.Count.Should().Be(1);
        ApiClientRequestHistory[0].Method.Should().Be(HttpMethod.Get);
        ApiClientRequestHistory[0].RequestUri.Should().Be(
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

        ApiClientRequestHistory.Count.Should().Be(1);
        ApiClientRequestHistory[0].Method.Should().Be(HttpMethod.Get);
        ApiClientRequestHistory[0].RequestUri.Should().Be(
            $"https://babelnet.io/v6/getSynsetIds?lemma={lemma}&searchLang={searchLang}&key={_apiKey}");

        res.Should().NotBeNull();
    }

    [Test]
    public async Task GetSynsetIds_WithArgs_lemma_searchLangs()
    {
        const string lemma = "apple";
        var searchLangs = new[] { "EN", "DE", "ES" };
        var res = await _apiClient.GetSynsetIdsAsync(lemma, searchLangs);

        ApiClientRequestHistory.Count.Should().Be(1);
        ApiClientRequestHistory[0].Method.Should().Be(HttpMethod.Get);
        ApiClientRequestHistory[0].RequestUri.Should().Be(
            $"https://babelnet.io/v6/getSynsetIds?lemma={lemma}&searchLang={string.Join("&searchLang=", searchLangs)}&key={_apiKey}");

        res.Should().NotBeNull();
    }

    [Test]
    public async Task GetSynsetIds_WithArgs_lemma_searchLang_targetLang_pos_source()
    {
        const string lemma = "apple";
        const string searchLang = "EN";
        const string targetLang = "DE";
        const UniversalPOS pos = UniversalPOS.NOUN;
        const string source = "BABELNET";
        var res = await _apiClient.GetSynsetIdsAsync(lemma, searchLang, targetLang, pos, "BABELNET");

        ApiClientRequestHistory.Count.Should().Be(1);
        ApiClientRequestHistory[0].Method.Should().Be(HttpMethod.Get);
        ApiClientRequestHistory[0].RequestUri.Should().Be(
            $"https://babelnet.io/v6/getSynsetIds?lemma={lemma}&searchLang={searchLang}&targetLang={targetLang}&pos={pos}&source={source}&key={_apiKey}");

        Assert.IsNotNull(res);
    }

    [Test]
    public async Task GetSynsetIds_WithArgs_lemma_searchLangs_targetLangs_pos_source()
    {
        const string lemma = "apple";
        var searchLangs = new[] { "EN", "DE", "ES" };
        var targetLangs = new[] { "FR", "IT" };
        const UniversalPOS pos = UniversalPOS.NOUN;
        const string source = "BABELNET";
        var res = await _apiClient.GetSynsetIdsAsync(lemma, searchLangs, targetLangs, pos, "BABELNET");

        ApiClientRequestHistory.Count.Should().Be(1);
        ApiClientRequestHistory[0].Method.Should().Be(HttpMethod.Get);
        ApiClientRequestHistory[0].RequestUri.Should().Be(
            $"https://babelnet.io/v6/getSynsetIds?lemma={lemma}&searchLang={string.Join("&searchLang=", searchLangs)}&targetLang={string.Join("&targetLang=", targetLangs)}&pos={pos}&source={source}&key={_apiKey}");

        res.Should().NotBeNull();
    }

    [Test]
    public void GetSynsetIds_WithArgs_lemma_emptySearchLangs_emptyTargetLangs()
    {
        const string lemma = "apple";

        // Empty searchLang will result in error from API
        Assert.ThrowsAsync<ApiException<Response2>>(async () => 
            await _apiClient.GetSynsetIdsAsync(lemma, Enumerable.Empty<string>(), Enumerable.Empty<string>(), null, null));

        ApiClientRequestHistory.Count.Should().Be(1);
        ApiClientRequestHistory[0].Method.Should().Be(HttpMethod.Get);
        ApiClientRequestHistory[0].RequestUri.Should().Be(
            $"https://babelnet.io/v6/getSynsetIds?lemma={lemma}&key={_apiKey}");
    }

    [Ignore("TODO: Come back to this")]
    [Test]
    public async Task GetSynset()
    {
        const string id = "bn:14792761n";
        const string targetLang = "EN";
        var res = await _apiClient.GetSynsetAsync(id, new string[] { targetLang });

        ApiClientRequestHistory.Count.Should().Be(1);
        ApiClientRequestHistory[0].Method.Should().Be(HttpMethod.Get);
        ApiClientRequestHistory[0].RequestUri.Should().Be(
            $"https://babelnet.io/v6/getSynset?id={id}&targetLang={targetLang}&key={_apiKey}");
    }

    [Test]
    public async Task GetSenses_WithArgs_lemma_searchLang()
    {
        const string lemma = "apple";
        const string searchLang = "EN";
        var res = await _apiClient.GetSensesAsync(lemma, searchLang);

        ApiClientRequestHistory.Count.Should().Be(1);
        ApiClientRequestHistory[0].Method.Should().Be(HttpMethod.Get);
        ApiClientRequestHistory[0].RequestUri.Should().Be(
            $"https://babelnet.io/v6/getSenses?lemma={lemma}&searchLang={searchLang}&key={_apiKey}");

        res.Should().NotBeNull();
        res.First().IdSense.Should().NotBe(0);

        var babelSense = res.Cast<Sense>().FirstOrDefault(s => s.Type == SenseType.BabelSense);
        if (babelSense != null)
        {
            babelSense.Properties.Should().BeOfType<BabelSense>();
            babelSense.As<ISense>().Type.Should().Be(SenseType.BabelSense);
        }

        var wordNetSense = res.Cast<Sense>().FirstOrDefault(s => s.Type == SenseType.WordNetSense);
        if (wordNetSense != null)
        {
            wordNetSense.Properties.Should().BeOfType<WordNetSense>();
            wordNetSense.As<ISense>().Type.Should().Be(SenseType.WordNetSense);
        }
    }
}
