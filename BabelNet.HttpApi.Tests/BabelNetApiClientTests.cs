using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using System.Linq;
using System.Threading;

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
        const string source = "WIKI";
        var res = await _apiClient.GetSynsetIdsAsync(lemma, searchLang, targetLang, pos, source);

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
        const string source = "WIKI";
        var res = await _apiClient.GetSynsetIdsAsync(lemma, searchLangs, targetLangs, pos, source);

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

    [Test]
    public async Task GetSynset()
    {
        const string id = "bn:14792761n";
        const string targetLang = "EN";
        var synset = await _apiClient.GetSynsetAsync(id, new string[] { targetLang });

        ApiClientRequestHistory.Count.Should().Be(1);
        ApiClientRequestHistory[0].Method.Should().Be(HttpMethod.Get);
        ApiClientRequestHistory[0].RequestUri.Should().Be(
            $"https://babelnet.io/v6/getSynset?id={Uri.EscapeDataString(id)}&targetLang={targetLang}&key={_apiKey}");

        synset.Should().NotBeNull();
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

        var babelSense = AssertGetSensesResponseItem<Sense>(res, SenseType.BabelSense);
        babelSense.SimpleLemma.Should().Be(lemma);
        babelSense.Language.Should().Be(searchLang);    

        var wordNetSense = AssertGetSensesResponseItem<WordNetSense>(res, SenseType.WordNetSense);
        wordNetSense.WordNetSenseNumber.Should().BePositive();
        wordNetSense.WordNetSynsetPosition.Should().BePositive();
        wordNetSense.WordNetOffset.Should().NotBeNullOrEmpty();

        // Gets the first response item of the given `SenseType` and asserts that it is of the given TSense
        // Throws `InconclusiveException` if the response doesn't contain an item of the given type
        // Returns the response item that was found
        TSense AssertGetSensesResponseItem<TSense>(ICollection<Sense> collection, SenseType senseType) where TSense : Sense
        {
            var item = collection.FirstOrDefault(s => s is TSense);
            if (item != null)
            {
                return (TSense)item;
            }

            Assert.Inconclusive($"The API did not return any {senseType} objects");
            return null;
        }

    }

    [Test]
    public async Task GetSenses_WithArgs_lemma_searchLang_targetLang_pos_source()
    {
        const string lemma = "apple";
        const string searchLang = "EN";
        const string targetLang = "DE";
        const UniversalPOS pos = UniversalPOS.NOUN;
        const string source = "WIKI";
        var res = await _apiClient.GetSensesAsync(lemma, searchLang, targetLang, pos, source);

        ApiClientRequestHistory.Count.Should().Be(1);
        ApiClientRequestHistory[0].Method.Should().Be(HttpMethod.Get);
        ApiClientRequestHistory[0].RequestUri.Should().Be(
            $"https://babelnet.io/v6/getSenses?lemma={lemma}&searchLang={searchLang}&targetLang={targetLang}&pos={pos}&source={source}&key={_apiKey}");

        Assert.IsNotNull(res);
    }

    [Test]
    public async Task GetSenses_ShouldBeConvertableToWordNetSensesCollection()
    {
        var res = await _apiClient.GetSensesAsync("apple", "EN", "EN", UniversalPOS.NOUN, "WN");

        if (res.Count > 0)
        {
            WordNetSense[] wnSenses = res.OfType<WordNetSense>().ToArray();
            wnSenses.Length.Should().Be(res.Count);
        }
        else
        {
            Assert.Inconclusive("The API did not return any WordNetSense objects");
        }
    }

    [Test]
    public void GetSenses_TestCancellation()
    {
        var cancellationSource = new CancellationTokenSource();
        
        const string lemma = "apple";
        const string searchLang = "EN";
        var task = _apiClient.GetSensesAsync(lemma, searchLang, cancellationSource.Token);

        cancellationSource.CancelAfter(10);

        Assert.ThrowsAsync<TaskCanceledException>(async () => await task);
    }

    [Test]
    public async Task GetOutgoingEdges()
    {
        var id = "bn:00007287n";
        var res = await _apiClient.GetOutgoingEdgesAsync(id);

        ApiClientRequestHistory.Count.Should().Be(1);
        ApiClientRequestHistory[0].Method.Should().Be(HttpMethod.Get);
        ApiClientRequestHistory[0].RequestUri.Should().Be(
            $"https://babelnet.io/v6/getOutgoingEdges?id={Uri.EscapeDataString(id)}&key={_apiKey}");

        Assert.IsNotNull(res);
    }

    [Test]
    public async Task GetSynsetIdsFromResourceID_WithArgs_id_BabelNet_source_WIKI()
    {
        string id = "BabelNet";
        string source = "WIKI";
        string searchLang = "EN";
        var pos = UniversalPOS.NOUN;

        var res = await _apiClient.GetSynsetIdsFromResourceIDAsync(id, source, searchLang, new[] { searchLang }, pos, null);

        ApiClientRequestHistory.Count.Should().Be(1);
        ApiClientRequestHistory[0].Method.Should().Be(HttpMethod.Get);
        ApiClientRequestHistory[0].RequestUri.Should().Be(
            $"https://babelnet.io/v6/getSynsetIdsFromResourceID?id={Uri.EscapeDataString(id)}" +
            $"&source={source}&searchLang={searchLang}&targetLang={searchLang}&pos={pos}" +
            $"&key={_apiKey}");


        Assert.IsNotNull(res);
    }

    [Test]
    public async Task GetSynsetIdsFromResourceID_WithArgs_id_Q4837690_source_WIKIDATA_targetLang_IT()
    {
        string id = "Q4837690";
        string source = "WIKIDATA";
        string targetLang = "IT";

        var res = await _apiClient.GetSynsetIdsFromResourceIDAsync(id, source, null, new[] { targetLang }, null, null);

        ApiClientRequestHistory.Count.Should().Be(1);
        ApiClientRequestHistory[0].Method.Should().Be(HttpMethod.Get);
        ApiClientRequestHistory[0].RequestUri.Should().Be(
            $"https://babelnet.io/v6/getSynsetIdsFromResourceID?id={Uri.EscapeDataString(id)}" +
            $"&source={source}&targetLang={targetLang}&key={_apiKey}");

        Assert.IsNotNull(res);
    }
    [Test]
    public async Task GetSynsetIdsFromResourceID_WithArgs_id_wn02398357v_source_WN_wnVersion_WN21()
    {
        string id = "wn:02398357v";
        string source = "WN";
        string wnVersion = "WN_21";

        var res = await _apiClient.GetSynsetIdsFromResourceIDAsync(id, source, null, null, null, wnVersion);

        ApiClientRequestHistory.Count.Should().Be(1);
        ApiClientRequestHistory[0].Method.Should().Be(HttpMethod.Get);
        ApiClientRequestHistory[0].RequestUri.Should().Be(
            $"https://babelnet.io/v6/getSynsetIdsFromResourceID?id={Uri.EscapeDataString(id)}" +
            $"&source={source}&wnVersion={wnVersion}&key={_apiKey}");

        Assert.IsNotNull(res);
    }
}
