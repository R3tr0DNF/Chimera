using Chimera.Models.Xbox;
using Chimera.Services.Conection;
using Chimera.Services.HidHide;
using Chimera.Services.Input;
using Chimera.Services.Translator;
using Chimera.Services.VirtualController;


namespace Chimera.Modules
{
    internal class PlayMode : IModule
    {
        public string Name => "Play Mode";

        public void Run(DualShockManager dualShock)
        {

            InputMonitor monitor = dualShock.Monitor!;

            HidHideManager hidHide = new HidHideManager();

            if (!hidHide.IsInstalled)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("HidHide is not installed");
                Console.ResetColor();

                dualShock.Disconnect();
                return;
            }

            if (!hidHide.IsOperational)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("HidHide is not working");
                Console.ResetColor();

                dualShock.Disconnect();
                return;
            }

            XboxController xbox = new XboxController();

            try
            {
                hidHide.RegisterCurrentApplication();
                hidHide.HideDualShock(dualShock.Device!);
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
                    hidHide.UnhideDualShock(dualShock.Device!);
                }
                catch{}

                try
                {
                    hidHide.Disable();
                }
                catch{}

                try
                {
                    dualShock.Disconnect();
                }
                catch{}

                Console.WriteLine("Play mode closed.");
            }

        }
    }
    
}