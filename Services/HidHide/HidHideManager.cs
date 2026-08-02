using HidSharp;
using Nefarius.Drivers.HidHide;
using Nefarius.Utilities.DeviceManagement.PnP;
using Chimera.Models;

namespace Chimera.Services.HidHide
{
    internal class HidHideManager
    {
        private readonly HidHideControlService _service;

        public HidHideManager()
        {
            _service = new HidHideControlService();
        }

        public bool IsInstalled
        {
            get
            {
                return _service.IsInstalled;
            }
        }

        public bool IsOperational
        {
            get
            {
                return _service.IsOperational;
            }
        }

        public bool IsEnabled
        {
            get
            {
                return _service.IsActive;
            }
        }

        public IEnumerable<string> Applications
        {
            get
            {
                return _service.ApplicationPaths;
            }
        }

        public void Enable()
        {
            _service.IsActive = true;
        }

        public void Disable()
        {
            _service.IsActive = false;
        }

        public void AddAplication(string path)
        {
            _service.AddApplicationPath(path);
        }

        public void RemoveApplication(string path)
        {
            _service.RemoveApplicationPath(path);
        }

        public void RegisterCurrentApplication()
        {
            string path = Environment.ProcessPath;

            if (!Applications.Contains(path))
            {
                AddAplication(path);
            }
        }

        public void HideDevice(string devicePath)
        {
            string? instanceId = PnPDevice.GetInstanceIdFromInterfaceId(devicePath);

            if (string.IsNullOrWhiteSpace(instanceId))
            {
                throw new InvalidOperationException("Can't get de device instance ID");
            }

            _service.AddBlockedInstanceId(instanceId);
        }

        public void HideDualShock(DualShockDevice controller)
        {
            HideDevice(controller.Device.DevicePath);
        }

        public void UnHideDevice(string devicePath)
        {
            string? instanceId = PnPDevice.GetInstanceIdFromInterfaceId(devicePath);

            if (string.IsNullOrWhiteSpace(instanceId))
            {
                throw new InvalidOperationException("Can't get de device instance ID");
            }
            _service.RemoveBlockedInstanceId(instanceId);
        }

        public void UnhideDualShock(DualShockDevice controller)
        {
           UnHideDevice(controller.Device.DevicePath);
        }


    }
}