namespace BabelNet.HttpApi
{
    public interface ISense
    {
        bool BKeySense { get; set; }
        int Frequence { get; set; }
        string FullLemma { get; set; }
        int IdSense { get; set; }
        string Language { get; set; }
        object Lemma { get; set; }
        string Pos { get; set; }
        Pronunciations Pronunciations { get; set; }
        string SenseKey { get; set; }
        string SimpleLemma { get; set; }
        string Source { get; set; }
        SynsetId SynsetID { get; set; }
        object Tags { get; set; }
        string TranslationInfo { get; set; }
    }
}