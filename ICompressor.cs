using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLE_Algorithm
{
    public interface ICompressor
    {
        string CompressText(string input);
        string DecompressText(string input);
    }
}
