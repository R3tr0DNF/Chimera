using Chimera.Models.Inputs;

namespace Chimera.Models.Xbox
{
    internal class XboxState
    {
        //xbox buttons

        public bool A { get; set; }
        public bool B { get; set; }
        public bool X { get; set; }
        public bool Y { get; set; }

        // D pad xbox

        public DPadDirection DPad { get; set; }

        //Shoulders buttons xbox

        public bool LB { get; set; }
        public bool RB { get; set; }

        //triggers buttons xbox
        
        public byte LT { get; set; }
        public byte RT { get; set; }

        //system buttons xbox

        public bool Back { get; set; }
        public bool Start { get; set; }
        public bool LeftThumb { get; set; }
        public bool RightThumb { get; set; }

        //Analog Sticks xbox

        public StickPosition LeftStick { get; set; }
        public StickPosition RightStick { get; set; }

        public XboxState()
        {
            LeftStick = new StickPosition(128, 128);
            RightStick = new StickPosition(128, 128);

            DPad = DPadDirection.Neutral;
        }
    }
}