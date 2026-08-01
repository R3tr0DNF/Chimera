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
                
                case 6:
                    switch(bit)
                    {
                        case 0: return "L1";
                        case 1: return "R1";
                        case 2: return "L2";
                        case 3: return "R2";
                        case 4: return "Share";
                        case 5: return "Options";
                        case 6: return "L3";
                        case 7: return "R3";
                    }
                    
                    break;
            }
            return null;
        }
    }
}