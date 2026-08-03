using System.Dynamic;

namespace Chimera.Models.Feedback
{
    internal class FeedbackState
    {
        public byte LargeMotor { get; set; }
        public byte SmallMotor { get; set; }
        public byte LedNumber { get; set; }
        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }
        public byte FlashOn{ get; set;}
        public byte FlashOff{ get; set; }

    }
}