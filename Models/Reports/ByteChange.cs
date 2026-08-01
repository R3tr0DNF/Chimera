namespace Chimera.Models
{
    internal class ByteChange
    {
        public int Index { get; set; }
        public byte PreviousValue { get; set; }
        public byte CurrentValue { get; set; }

        public byte Difference
        {
            get
            {
                return (byte)(PreviousValue ^ CurrentValue);
            }
        }
    }
}