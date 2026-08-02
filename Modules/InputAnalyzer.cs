using Chimera.Models.Inputs;
using Chimera.Services.Conection;
using Chimera.Services.Input;


namespace Chimera.Modules
{
    internal class InputAnalyzer : IModule
    {
        public string Name => "Input Analyzer";

        public void Run(DualShockManager dualShock)
        {
            Console.Clear();

    
            InputMonitor monitor = dualShock.Monitor!;

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }

                if (!monitor.TryRead())
                {
                    continue;
                }

                Console.Clear();

                PrintState(monitor.CurrentState);
            }

            Console.WriteLine();
            Console.WriteLine("Input Analyzer closed.");
        }

        private static void PrintState(DualShockState state)
        {
            Console.WriteLine("INPUT");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Buttons");
            Console.ResetColor();

            Console.WriteLine($"Square   : {state.Square}");
            Console.WriteLine($"Cross    : {state.Cross}");
            Console.WriteLine($"Circle   : {state.Circle}");
            Console.WriteLine($"Triangle : {state.Triangle}");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("D-Pad");
            Console.ResetColor();

            Console.WriteLine(state.DPad);

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Shoulders");
            Console.ResetColor();

            Console.WriteLine($"L1 : {state.L1}");
            Console.WriteLine($"R1 : {state.R1}");

            Console.WriteLine($"L2 Button : {state.L2Button}");
            Console.WriteLine($"R2 Button : {state.R2Button}");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Triggers");
            Console.ResetColor();

            Console.WriteLine($"L2 Value : {state.L2Value}");
            Console.WriteLine($"R2 Value : {state.R2Value}");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("System");
            Console.ResetColor();

            Console.WriteLine($"Share   : {state.Share}");
            Console.WriteLine($"Options : {state.Options}");

            Console.WriteLine($"L3 : {state.L3}");
            Console.WriteLine($"R3 : {state.R3}");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Left Stick");
            Console.ResetColor();

            Console.WriteLine($"X : {state.LeftStick.X}");
            Console.WriteLine($"Y : {state.LeftStick.Y}");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Right Stick");
            Console.ResetColor();

            Console.WriteLine($"X : {state.RightStick.X}");
            Console.WriteLine($"Y : {state.RightStick.Y}");

            Console.WriteLine();
            Console.WriteLine("Press ESC to exit...");
        }
    }
}