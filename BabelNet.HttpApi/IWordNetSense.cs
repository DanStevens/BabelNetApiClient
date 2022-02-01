namespace BabelNet.HttpApi
{
    public interface IWordNetSense : ISense
    {
        /// <summary>The sense number in WordNet</summary>
        double WordNetSenseNumber { get; set; }

        /// <summary>The offset of the Sense in WordNet</summary>
        string WordNetOffset { get; set; }

        /// <summary>The synset position of the Sense in WordNet</summary>
        double WordNetSynsetPosition { get; set; }
    }
}