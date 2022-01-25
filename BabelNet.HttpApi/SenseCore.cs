using Newtonsoft.Json;
using JsonSubTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelNet.HttpApi
{
    [JsonConverter(typeof(JsonSubtypes))]
    [JsonSubtypes.FallBackSubType(typeof(BabelSense))]
    [JsonSubtypes.KnownSubTypeWithProperty(typeof(WordNetSense), nameof(WordNetSense.WordNetSenseNumber))]
    public partial class SenseCore : ISense
    {
    }
}
