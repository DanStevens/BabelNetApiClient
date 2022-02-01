using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonSubTypes;
using Newtonsoft.Json;

namespace BabelNet.HttpApi
{
    // Use a JsonConverter provided by JsonSubtypes, which deserializes a Sense object as a WordNetSense
    // subtype when it contains a property named 'workNetSenseNumber'
    [JsonConverter(typeof(JsonSubtypes))]
    [JsonSubtypes.KnownSubTypeWithProperty(typeof(WordNetSense), nameof(WordNetSense.WordNetSenseNumber))]
    public partial class Sense : ISense {}
}
