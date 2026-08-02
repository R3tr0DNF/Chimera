using Chimera.Models.Inputs;
using Chimera.Models.Xbox;

namespace Chimera.Services.Translator
{
    internal static class XboxTranslator
    {
        public static XboxState Translate(DualShockState dualShock)
        {
            XboxState xbox = new XboxState();

            //buttons playstation to xbox

            xbox.A = dualShock.Cross;
            xbox.B = dualShock.Circle;
            xbox.X = dualShock.Square;
            xbox.Y = dualShock.Triangle;

            //D=Pad

            xbox.DPad = dualShock.DPad;

            //shoulder and triggers buttons

            xbox.LB = dualShock.L1;
            xbox.RB = dualShock.R1;

            xbox.LT = dualShock.L2Value;
            xbox.RT = dualShock.R2Value;

            //system

            xbox.Back = dualShock.Share;
            xbox.Start = dualShock.Options;

            xbox.LeftThumb = dualShock.L3;
            xbox.RightThumb = dualShock.R3;

            //analog sticks

            xbox.LeftStick = dualShock.LeftStick;
            xbox.RightStick = dualShock.RightStick;

            return xbox;
        }
    }
}