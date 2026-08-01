namespace Chimera.Services
{
    internal static class DualShockMapper
    {
        public static string? GetButtonName(int byteIndex, int bit)
        {
            switch(byteIndex)
            {
                case 5:
                    switch(bit)
                    {
                        case 4: return "Square";
                        case 5: return "Cross";
                        case 6: return "Circle";
                        case 7: return "Triangle";
                    }
                    
                    break;
            }
            return null;
        }
    }
}