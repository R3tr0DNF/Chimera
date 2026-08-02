namespace Chimera.Services.Translator
{
    internal static class AxisTranslator
    {
        public static short ToXboxAxis(byte value, bool invert = false)
        {
            if (invert)
            {
                value = (byte)(255 - value);
            }

            double normalized = value / 255.0;

            double xbox = normalized * 65535.0 - 32768.0;

            return (short)Math.Round(xbox);
        }

    }
}