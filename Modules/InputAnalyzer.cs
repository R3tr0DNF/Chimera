using System;
using Chimera.Models;
using Chimera.Models.Inputs;
using Chimera.Services;
using Chimera.Services.Parser;
using HidSharp;

namespace Chimera.Modules
{
    internal class InputAnalyzer : IModule
    {
        public string Name => "Input Analyzer";

        public void Run()
        {
            Console.Clear();

            Console.WriteLine("Searching DualShock 4... ");

            DualShockDevice? controller = HidScanner.FindDualShock();

            if(controller == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No PS4 Controller found... :c");
                Console.ResetColor();

                return;
            }

            HidStream? stream = DualshockConnection.Open(controller);

            if(stream == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Can't open conection with ps4 controller :c");
                Console.ResetColor();
                return;
            }

            byte[] report = new byte [64];

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    if(Console.ReadKey(true).Key == ConsoleKey.Escape)
                    break;
                }

                stream.Read(report);

                DualShockState state = DualShockParser.Parse(report);

                Console.Clear();

                PrintState(state);
            }

            stream.Close();

        }

        private void PrintState(DualShockState state)
        {
            Console.WriteLine("Input:");
            Console.WriteLine();

            Console.WriteLine("Buttons: ");
            Console.WriteLine($"Square : {state.Square}");
            Console.WriteLine($"Cross : {state.Cross}");
            Console.WriteLine($"Circle : {state.Circle}");
            Console.WriteLine($"Triangle : {state.Triangle}");

            Console.WriteLine();

            Console.WriteLine($"Dpad : {state.DPad}");
            
            Console.WriteLine();

            Console.WriteLine("Shoulders: ");
            Console.WriteLine($"L1 : {state.L1}");
            Console.WriteLine($"R1 : {state.R1}");

            Console.WriteLine($"L2 Button : {state.L2Button}");
            Console.WriteLine($"R2 Button : {state.R2Button}");

            Console.WriteLine();

            Console.WriteLine("Triggers: ");

            Console.WriteLine($"L2 Value : {state.L2Value}");
            Console.WriteLine($"R2 Value : {state.R2Value}");

            Console.WriteLine();

            Console.WriteLine("System: ");
            Console.WriteLine($"Share : {state.Share}");
            Console.WriteLine($"Options : {state.Options}");

            Console.WriteLine($"L3 : {state.L3}");
            Console.WriteLine($"R3 : {state.R3}");

            Console.WriteLine();

            Console.WriteLine("Left Stick: ");
            Console.WriteLine($"X : {state.LeftStick.X}");
            Console.WriteLine($"Y : {state.LeftStick.Y}");

            Console.WriteLine("Right Stick: ");
            Console.WriteLine($"X : {state.RightStick.X}");
            Console.WriteLine($"Y : {state.RightStick.Y}");

            Console.WriteLine();
            Console.WriteLine("Press ESC to exit");
        }
    }
}