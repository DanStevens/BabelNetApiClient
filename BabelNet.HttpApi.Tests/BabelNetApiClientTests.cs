using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;

namespace BabelNet.HttpApi.Tests;

public class BabelNetApiClientTests
{
    private const string _apiKey = Secrets.ApiKey;

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task GetVersion()
    {
        var babelNetApiClient = new BabelNetApiClient(new HttpClient(), _apiKey);
        var res = await babelNetApiClient.GetVersionAsync();
        Assert.IsNotNull(res);
        Assert.AreEqual("V5_0", res.Version);
    }
}
