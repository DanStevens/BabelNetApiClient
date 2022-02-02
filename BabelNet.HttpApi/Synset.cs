using System.Collections.Generic;
using System.Linq;

namespace BabelNet.HttpApi
{
    public partial class Synset : ISynset
    {
        ICollection<Sense> ISynset.Senses => Senses.Select(s => s.Sense).ToList();
    }
}