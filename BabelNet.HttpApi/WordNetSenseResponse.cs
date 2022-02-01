namespace BabelNet.HttpApi
{
    public partial class WordNetSenseResponse : IWordNetSense
    {
        public WordNetSenseResponse()
        {
            Type = SenseType.WordNetSense;
        }

        public WordNetSenseResponse(WordNetSense wordNetSense) : this()
        {
            Properties = wordNetSense;
        }

        public override Sense Sense => Properties;

        public double WordNetSenseNumber { get => ((IWordNetSense)Sense).WordNetSenseNumber; set => ((IWordNetSense)Sense).WordNetSenseNumber = value; }
        public string WordNetOffset { get => ((IWordNetSense)Sense).WordNetOffset; set => ((IWordNetSense)Sense).WordNetOffset = value; }
        public double WordNetSynsetPosition { get => ((IWordNetSense)Sense).WordNetSynsetPosition; set => ((IWordNetSense)Sense).WordNetSynsetPosition = value; }

        public static explicit operator Sense(WordNetSenseResponse item) => item.Sense;

    }
}