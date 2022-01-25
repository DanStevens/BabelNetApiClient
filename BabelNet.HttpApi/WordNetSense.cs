using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelNet.HttpApi
{
    public partial class WordNetSense : ISense
    {
        protected override SenseType GetSenseType() => SenseType.WordNetSense;
    }
}
