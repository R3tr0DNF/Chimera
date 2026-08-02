using Chimera.Models;
using Chimera.Services;
using Chimera.Services.Conection;
using Chimera.Services.HidHide;
using Nefarius.Utilities.DeviceManagement;
using Nefarius.Utilities.DeviceManagement.PnP;

namespace Chimera.Modules
{
    internal class HidHideTest : IModule
    {
        public string Name => "HidHide Test";

        public void Run(DualShockManager dualShock)
        {
            Console.Clear();
            HidHideManager hidHide = new();
            


            hidHide.RegisterCurrentApplication();
            
            Console.WriteLine("Hiding ps4 controller...");
            hidHide.HideDualShock(dualShock.Device!);

            Console.WriteLine("Enabling HidHide...");
            hidHide.Enable();

            Console.WriteLine();
            Console.WriteLine("Open joy.cpl and verify");
            Console.WriteLine("Press any key to restore...");
            Console.ReadKey();

            Console.WriteLine("Restoring controller ... ");
            hidHide.UnhideDualShock(dualShock.Device!);

            hidHide.Disable();

            Console.WriteLine("done.. ");
            Console.ReadKey();


        }
    }
}