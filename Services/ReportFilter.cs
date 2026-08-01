namespace Chimera.Models
{
    internal static class ReportFilter
    {
        public static bool ShouldIgnore(int byteIndex)
        {
            switch (byteIndex)
            {
                case 7:
                return true; // Battery level

                default:
                return false;
            }
        }
    }
}