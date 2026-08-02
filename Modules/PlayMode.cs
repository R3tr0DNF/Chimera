using System.Net.Http.Headers;
using Chimera.Models;
using Chimera.Models.Inputs;
using Chimera.Models.Xbox;
using Chimera.Services;
using Chimera.Services.HidHide;
using Chimera.Services.Input;
using Chimera.Services.Translator;
using Chimera.Services.VirtualController;
using HidSharp;

namespace Chimera.Modules
{
    internal class PlayMode : IModule
    {
        public string Name => "Play Mode";

        public void Run()
        {
            Console.Clear();
            
            Console.WriteLine("Searching for Dualshock 4 ...");

            DualShockDevice? dualShock = HidScanner.FindDualShock();

            if (dualShock == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Dualshock 4 not found... :c ");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("DualShock 4 found pal enjoy :D");
            Console.ResetColor();

            HidStream? stream = DualshockConnection.Open(dualShock);

            if (stream == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to open controller :c..");
                Console.ResetColor();
                return;
            }

            InputMonitor monitor = new InputMonitor(stream);

            HidHideManager hidHide = new HidHideManager();

            if (!hidHide.IsInstalled)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("HidHide is not installed");
                Console.ResetColor();

                stream.Close();
                return;
            }

            if (!hidHide.IsOperational)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("HidHide is not working");
                Console.ResetColor();

                stream.Close();
                return;
            }

            XboxController xbox = new XboxController();

            try
            {
                hidHide.RegisterCurrentApplication();
                hidHide.HideDualShock(dualShock);
                hidHide.Enable();

                xbox.Connect();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Play mode started!! Xbox controller conected succesfully, if you are unsure please check joy.cpl");
                Console.ResetColor();


                Console.WriteLine("Press ESC to exit... ");
                Console.WriteLine();

                while (true)
                {
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true);

                        if (key.Key == ConsoleKey.Escape)
                        {
                            break;
                        }
                }

                if (monitor.TryRead())
                {
                    XboxState xboxState = XboxTranslator.Translate(monitor.CurrentState);
                    xbox.Update(xboxState);
                }
            }
        }
        finally
            {
                Console.WriteLine();
                Console.WriteLine("Cleaning up ...");

                try
                {
                    xbox.Disconnect();
                }
                catch{ }

                try
                {
                    hidHide.UnhideDualShock(dualShock);
                }
                catch{}

                try
                {
                    hidHide.Disable();
                }
                catch{}

                try
                {
                    stream.Close();
                }
                catch{}

                Console.WriteLine("Play mode closed.");
            }

        }
    }
    
}