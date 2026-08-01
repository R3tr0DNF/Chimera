using System.Collections.Generic;
using Chimera.Models;

namespace Chimera.Services
{
    internal static class ChangeDetector
    {
        public static List<ByteChange> DetectChanges(byte[] previousReport, byte[] currentReport)
        {
            List<ByteChange> changes = new List<ByteChange>();

            int length = previousReport.Length;

            if (currentReport.Length < length)
            {
                length = currentReport.Length;
            }

            for (int i = 0; i < length; i++)
            {
                if (previousReport[i] != currentReport[i])
                {
                    changes.Add(new ByteChange
                    {
                        Index = i,
                        PreviousValue = previousReport[i],
                        CurrentValue = currentReport[i]
                    });
                }
            }

            return changes;
        }
    }
}