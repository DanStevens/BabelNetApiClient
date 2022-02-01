namespace BabelNet.HttpApi
{
    public partial class WordNetSenseResponse : IWordNetSense
    {
        protected override Sense GetProperties() => Properties;

        public double WordNetSenseNumber { get => ((IWordNetSense)GetProperties()).WordNetSenseNumber; set => ((IWordNetSense)GetProperties()).WordNetSenseNumber = value; }
        public string WordNetOffset { get => ((IWordNetSense)GetProperties()).WordNetOffset; set => ((IWordNetSense)GetProperties()).WordNetOffset = value; }
        public double WordNetSynsetPosition { get => ((IWordNetSense)GetProperties()).WordNetSynsetPosition; set => ((IWordNetSense)GetProperties()).WordNetSynsetPosition = value; }

        public static explicit operator Sense(WordNetSenseResponse item) => item.GetProperties();

    }
}