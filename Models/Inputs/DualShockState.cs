using Chimera.Modules;

namespace Chimera.Models.Inputs
{
    internal class DualShockState
    {
        //buttons
        public bool Square {get; set;}
        public bool Cross {get; set;}
        public bool Circle {get; set;}
        public bool Triangle {get; set;}
        //D-pad
        public DPadDirection DPad {get; set;}
        //R - l shoulder and triggers
        public bool L1 {get; set;}
        public bool R1 {get; set;}
        public bool L2Button {get; set;}
        public bool R2Button {get; set;}
        //system buttons
        public bool Share {get; set;}
        public bool Options {get; set;}
        public bool L3 {get; set;}
        public bool R3 {get; set;}
        //analog triggers
        public byte L2Value {get; set;}
        public byte R2Value {get; set;}
        //analog sticks
        public StickPosition LeftStick {get; set;}
        public StickPosition RightStick {get; set;}

        public DualShockState()
        {
            LeftStick = new StickPosition(128, 128);
            RightStick = new StickPosition(128, 128);

            DPad = DPadDirection.Neutral;
        }
    }
}