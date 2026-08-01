using Chimera.Models;

namespace Chimera.Services
{
    internal static class DPadAnalyzer
    {
        public static DPadDirection GetDirection(byte value)
        {
            int hat = value & 0x0F;

            return hat switch
            {
                0 => DPadDirection.Up,
                1 => DPadDirection.UpRight,
                2 => DPadDirection.Right,
                3 => DPadDirection.DownRight,
                4 => DPadDirection.Down,
                5 => DPadDirection.DownLeft,
                6 => DPadDirection.Left,
                7 => DPadDirection.UpLeft,
                _ => DPadDirection.Neutral
            };
        }

        public static bool HasChanged(byte previousValue, byte currentValue)
        {
            return (previousValue & 0x0F) != (currentValue & 0x0F);
        }
    }
}