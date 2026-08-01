using System.Data;
using System.Runtime.CompilerServices;
using Chimera.Models.Inputs;
using Chimera.Services;

namespace Chimera.Services.Parser
{
    internal static class DualShockParser
    {
        public static DualShockState Parse(byte[] report)
        {
            DualShockState state = new DualShockState();
            //buttons
            state.Square = IsPressed(report[5], 4);
            state.Cross = IsPressed(report[5], 5);
            state.Circle = IsPressed(report[5], 6);
            state.Triangle = IsPressed(report[5], 7);

            //dpad

            state.DPad = DPadAnalyzer.GetDirection(report[5]);

            // shoulder and triggers buttons

            state.L1 = IsPressed(report[6], 0);
            state.R1 = IsPressed(report[6], 1);

            state.L2Button = IsPressed(report[6], 2);
            state.R2Button = IsPressed(report[6], 3);

            // system buttons

            state.Share = IsPressed(report[6], 4);
            state.Options = IsPressed(report[6], 5);

            state.L3 = IsPressed(report[6], 6);
            state.R3 = IsPressed(report[6], 7);

            //analog triggers

            state.L2Value = report[8];
            state.R2Value = report[9];

            //analog sticks

            state.LeftStick = StickAnalyzer.GetLeftStick(report);
            state.RightStick = StickAnalyzer.GetRightStick(report);

            return state;

        }
        
        private static bool IsPressed(byte value, int bit)
        {
            return (value & (1 << bit)) != 0;
        }
    }
}