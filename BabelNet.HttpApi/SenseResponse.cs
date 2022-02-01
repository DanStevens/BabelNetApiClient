using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using JsonSubTypes;
using Newtonsoft.Json;

namespace BabelNet.HttpApi
{
    // Use a JsonConverter provided by JsonSubtypes, which deserializes a SenseResponse object as a
    // WordNetSenseResponse subtype when Type is `SenseType.WordNetSense`
    [JsonConverter(typeof(JsonSubtypes), "Type")]
    [JsonSubtypes.KnownSubType(typeof(WordNetSenseResponse), SenseType.WordNetSense)]
    [DebuggerDisplay("SenseResponse(Type = {Type}; Lemma = {SimpleLemma})")]
    public partial class SenseResponse : ISense
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

        public bool BKeySense { get => Sense.BKeySense; set => Sense.BKeySense = value; }
        public int Frequence { get => Sense.Frequence; set => Sense.Frequence = value; }
        public string FullLemma { get => Sense.FullLemma; set => Sense.FullLemma = value; }
        public int IdSense { get => Sense.IdSense; set => Sense.IdSense = value; }
        public string Language { get => Sense.Language; set => Sense.Language = value; }
        public object Lemma { get => Sense.Lemma; set => Sense.Lemma = value; }
        public string Pos { get => Sense.Pos; set => Sense.Pos = value; }
        public Pronunciations Pronunciations { get => Sense.Pronunciations; set => Sense.Pronunciations = value; }
        public string SenseKey { get => Sense.SenseKey; set => Sense.SenseKey = value; }
        public string SimpleLemma { get => Sense.SimpleLemma; set => Sense.SimpleLemma = value; }
        public string Source { get => Sense.Source; set => Sense.Source = value; }
        public SynsetId SynsetID { get => Sense.SynsetID; set => Sense.SynsetID = value; }
        public object Tags { get => Sense.Tags; set => Sense.Tags = value; }
        public string TranslationInfo { get => Sense.TranslationInfo; set => Sense.TranslationInfo = value; }

        public static explicit operator Sense(SenseResponse item) => item.Sense;

        public TSense ToSenseType<TSense>() where TSense : ISense
        {
            try
            {
                return (TSense) (ISense) Sense;
            }
            catch (InvalidCastException ex)
            {
                string msg = $"Cannot convert to type {typeof(TSense)}; " +
                             $"check {nameof(Type)} property of {typeof(SenseResponse)}";
                throw new InvalidOperationException(msg, ex);
            }
        }
    }
}
