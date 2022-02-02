using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using JsonSubTypes;
using Newtonsoft.Json;

namespace BabelNet.HttpApi
{
    // Use a JsonConverter provided by JsonSubtypes, which deserializes a SenseResponse object as a
    // WordNetSenseResponse subtype when Type is `SenseType.WordNetSense`
    [JsonConverter(typeof(JsonSubtypes), nameof(Type))]
    [JsonSubtypes.KnownSubType(typeof(WordNetSenseResponse), SenseType.WordNetSense)]
    [DebuggerDisplay("SenseResponse(Type = {Type}; Lemma = {Sense.SimpleLemma})")]
    public partial class SenseResponse
    {
        public SenseResponse()
        {
            Type = SenseType.BabelSense;
        }

        public SenseResponse(Sense sense) : this()
        {
            Properties = sense;
        }

        /// <summary>
        /// The sense object contained within the SenseResponse
        /// </summary>
        public virtual Sense Sense => Properties;
    }
}
