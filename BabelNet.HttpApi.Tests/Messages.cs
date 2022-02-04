namespace BabelNet.HttpApi.Tests;

internal static class Messages
{
    public const string GeneralRequestError = "There is a problem with the request\n\nStatus: 400\n";
    public const string WrongParameters = "Wrong parameters.";
    public const string ApiKeyError = "There was an problem with the supplied API Key\n\nStatus: 403\n";
    public const string InvalidKeyError =
        "Your key is not valid or the daily requests limit has been reached. Please visit http://babelnet.org.";
    public const string SynsetNotFound = "BabelSynset not found.";
}