namespace Chimera.Services.Translator
{
    internal static class AxisTranslator
    {
        public static short ToXboxAxis(byte value)
        {
            double normalized = value / 255.0;

            double xbox = normalized * (short.MaxValue - short.MinValue) + short.MinValue;

            return (short)Math.Round(xbox);
        }

        public static short ToXboxAxisInverted(byte value)
        {
            return (short)-ToXboxAxis(value);
        }
    }
}