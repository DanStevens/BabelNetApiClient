namespace BabelNet.HttpApi
{
    public partial class WordNetSenseResponse
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

    }
}