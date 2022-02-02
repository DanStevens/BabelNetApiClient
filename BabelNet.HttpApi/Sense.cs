using System.Diagnostics;
using JsonSubTypes;
using Newtonsoft.Json;

namespace BabelNet.HttpApi
{
    // Use a JsonConverter provided by JsonSubtypes, which deserializes a Sense object as a WordNetSense
    // subtype when it contains a property named 'workNetSenseNumber'
    [JsonConverter(typeof(JsonSubtypes))]
    [JsonSubtypes.KnownSubTypeWithProperty(typeof(WordNetSense), nameof(WordNetSense.WordNetSenseNumber))]
    [DebuggerDisplay("Sense(Lemma = {FullLemma})")]
    public partial class Sense {}
}
