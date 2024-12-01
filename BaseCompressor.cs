using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace RLE_Algorithm
{
    public abstract class BaseCompressor : ICompressor
    {
        public abstract string CompressText(string input);
        public abstract string DecompressText(string input);
        
    }
}
