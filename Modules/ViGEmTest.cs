using Chimera.Modules;
using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;

namespace Chimera.Models 
{
    internal class ViGEmTest : IModule
    {
        public string Name => "ViGEm Test";
        public void Run()
        {
            var client = new ViGEmClient();
            IXbox360Controller controller = client.CreateXbox360Controller();

            controller.Connect();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Virtual Xbox Controller connected! ");
            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("Press any key to disconnect...");
            Console.ReadKey(true);
            
            controller.Disconnect();
        }
    }
}