using System.Collections.Generic;

namespace Chimera.Services
{
    internal static class BitAnalyzer
    {
        public static List<int> GetChangedBits(byte difference)
        {
            List<int> changedBits = new List<int>();

            for (int bit = 0; bit < 8; bit++)
            {
                byte mask = (byte)(1 << bit);

                if ((difference & mask) != 0)
                {
                    changedBits.Add(bit);
                }
            }

            return changedBits;
        }
    }
}