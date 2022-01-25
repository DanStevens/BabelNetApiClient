using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelNet.HttpApi
{
    public partial class Sense : ISense
    {
        public new bool BKeySense { get => Properties.BKeySense; set => Properties.BKeySense = value; }
        public new int Frequence { get => Properties.Frequence; set => Properties.Frequence = value; }
        public new string FullLemma { get => Properties.FullLemma; set => Properties.FullLemma = value; }
        public new int IdSense { get => Properties.IdSense; set => Properties.IdSense = value; }
        public new string Language { get => Properties.Language; set => Properties.Language = value; }
        public new object Lemma { get => Properties.Lemma; set => Properties.Lemma = value; }
        public new string Pos { get => Properties.Pos; set => Properties.Pos = value; }
        public new Pronunciations Pronunciations { get => Properties.Pronunciations; set => Properties.Pronunciations = value; }
        public new string SenseKey { get => Properties.SenseKey; set => Properties.SenseKey = value; }
        public new string SimpleLemma { get => Properties.SimpleLemma; set => Properties.SimpleLemma = value; }
        public new string Source { get => Properties.Source; set => Properties.Source = value; }
        public new SynsetId SynsetID { get => Properties.SynsetID; set => Properties.SynsetID = value; }
        public new object Tags { get => Properties.Tags; set => Properties.Tags = value; }
        public new string TranslationInfo { get => Properties.TranslationInfo; set => Properties.TranslationInfo = value; }

        protected override SenseType GetSenseType() => Type;

    }
}
