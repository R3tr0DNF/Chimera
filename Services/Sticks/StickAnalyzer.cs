using Chimera.Models;

namespace Chimera.Services
{
    internal static class StickAnalyzer
    {
        public static StickPosition GetLeftStick(byte [] report)
        {
            return new StickPosition(report[1], report[2]);
        }

        public static StickPosition GetRightStick(byte [] report)
        {
            return new StickPosition(report[3], report[4]);
        }

        public static bool LeftStickChanged(byte [] previous, byte [] current)
        {
            return previous[1] != current[1] || previous[2] != current[2];
        }

        public static bool RightStickChanged(byte [] previous, byte [] current)
        {
            return previous[3] != current[3] || previous[4] != current[4];
        }
    }
}