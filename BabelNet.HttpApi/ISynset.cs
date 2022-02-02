using System.Collections.Generic;

namespace BabelNet.HttpApi
{
    public interface ISynset
    {
        /// <summary>Sense objects associated with the Synset</summary>
        ICollection<Sense> Senses { get; }

        /// <summary>WordNet offsets corresponding to this Synset</summary>
        ICollection<SynsetId> WnOffsets { get; }

        /// <summary>Glosses associated with this Synset</summary>
        ICollection<Gloss> Glosses { get; }

        /// <summary>A list of usage examples for this Synset</summary>
        ICollection<object> Examples { get; }

        /// <summary>A list of images associated with this Synset</summary>
        ICollection<Image> Images { get; }

        SynsetType SynsetType { get; }

        /// <summary>A list of Wikipedia categories for this Synset</summary>
        ICollection<WikipediaCategory> Categories { get; }

        /// <summary>Translations between senses found in this BabelSynset.</summary>
        object Translations { get; }

        /// <summary>A map of Domains to the importance of this Synset.</summary>
        object Domains { get; }

        /// <summary>The set of languages used in this Synset.</summary>
        ICollection<string> FilterLangs { get; }

        /// <summary>A list of tags associated with the Synset.</summary>
        ICollection<object> Tags { get; }

        /// <summary>True if the Synset is a key concept.</summary>
        bool BKeyConcepts { get; }
    }
}