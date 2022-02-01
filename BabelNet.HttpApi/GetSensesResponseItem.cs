using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelNet.HttpApi
{
    public partial class GetSensesResponseItem : ISense
    {
        public bool BKeySense { get => Properties.BKeySense; set => Properties.BKeySense = value; }
        public int Frequence { get => Properties.Frequence; set => Properties.Frequence = value; }
        public string FullLemma { get => Properties.FullLemma; set => Properties.FullLemma = value; }
        public int IdSense { get => Properties.IdSense; set => Properties.IdSense = value; }
        public string Language { get => Properties.Language; set => Properties.Language = value; }
        public object Lemma { get => Properties.Lemma; set => Properties.Lemma = value; }
        public string Pos { get => Properties.Pos; set => Properties.Pos = value; }
        public Pronunciations Pronunciations { get => Properties.Pronunciations; set => Properties.Pronunciations = value; }
        public string SenseKey { get => Properties.SenseKey; set => Properties.SenseKey = value; }
        public string SimpleLemma { get => Properties.SimpleLemma; set => Properties.SimpleLemma = value; }
        public string Source { get => Properties.Source; set => Properties.Source = value; }
        public SynsetId SynsetID { get => Properties.SynsetID; set => Properties.SynsetID = value; }
        public object Tags { get => Properties.Tags; set => Properties.Tags = value; }
        public string TranslationInfo { get => Properties.TranslationInfo; set => Properties.TranslationInfo = value; }
    }
}
