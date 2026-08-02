using Chimera.Services.Conection;

namespace Chimera.Modules
{
    internal interface IModule
    {
        string Name { get; }

        void Run(DualShockManager dualShock);
    }
}

