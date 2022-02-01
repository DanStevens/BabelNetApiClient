using System;
using System.Diagnostics;

namespace BabelNet.HttpApi
{
    [DebuggerDisplay("SenseResponse(Type = {Type}; Lemma = {SimpleLemma})")]
    public partial class SenseResponse : ISense
    {
        protected virtual Sense GetProperties() => Properties;

        public bool BKeySense { get => GetProperties().BKeySense; set => GetProperties().BKeySense = value; }
        public int Frequence { get => GetProperties().Frequence; set => GetProperties().Frequence = value; }
        public string FullLemma { get => GetProperties().FullLemma; set => GetProperties().FullLemma = value; }
        public int IdSense { get => GetProperties().IdSense; set => GetProperties().IdSense = value; }
        public string Language { get => GetProperties().Language; set => GetProperties().Language = value; }
        public object Lemma { get => GetProperties().Lemma; set => GetProperties().Lemma = value; }
        public string Pos { get => GetProperties().Pos; set => GetProperties().Pos = value; }
        public Pronunciations Pronunciations { get => GetProperties().Pronunciations; set => GetProperties().Pronunciations = value; }
        public string SenseKey { get => GetProperties().SenseKey; set => GetProperties().SenseKey = value; }
        public string SimpleLemma { get => GetProperties().SimpleLemma; set => GetProperties().SimpleLemma = value; }
        public string Source { get => GetProperties().Source; set => GetProperties().Source = value; }
        public SynsetId SynsetID { get => GetProperties().SynsetID; set => GetProperties().SynsetID = value; }
        public object Tags { get => GetProperties().Tags; set => GetProperties().Tags = value; }
        public string TranslationInfo { get => GetProperties().TranslationInfo; set => GetProperties().TranslationInfo = value; }

        public static explicit operator Sense(SenseResponse item) => item.GetProperties();

        public TSense ToSenseType<TSense>() where TSense : ISense
        {
            try
            {
                return (TSense) (ISense) GetProperties();
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
