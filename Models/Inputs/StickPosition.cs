namespace Chimera.Models
{
    internal class StickPosition
    {
        public byte X {get;}
        public byte Y {get;}
        
        public StickPosition(byte x, byte y)
        {
            X = x;
            Y = y;
        }
    }
}