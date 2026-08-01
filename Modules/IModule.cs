namespace Chimera.Modules
{
    internal interface IModule
    {
        string Name { get; }

        void Run();
    }
}

