using Chimera.Models.Feedback;

namespace Chimera.Models.Feedback
{
    internal class DualShockOutputState
    {
        // Motors
        public byte SmallMotor { get; private set; }
        public byte LargeMotor { get; private set; }

        // Light Bar
        public byte Red { get; private set; }
        public byte Green { get; private set; }
        public byte Blue { get; private set; }

        // Flash
        public byte FlashOn { get; private set; }
        public byte FlashOff { get; private set; }

        public void Apply(FeedbackState feedback)
        {
            SmallMotor = feedback.SmallMotor;
            LargeMotor = feedback.LargeMotor;

            Red = feedback.Red;
            Green = feedback.Green;
            Blue = feedback.Blue;

            FlashOn = feedback.FlashOn;
            FlashOff = feedback.FlashOff;

        }
    }
}