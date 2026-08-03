using Chimera.Models.Feedback;

namespace Chimera.Services.Feedback
{
    internal class OutputReportBuilder
    {
        // USB
        private const int UsbReportLength = 32;
        private const byte UsbReportId = 0x05;

        // Bluetooth
        private const int BluetoothReportLength = 78;
        private const byte BluetoothReportId = 0x11;

        private readonly DualShockOutputState _state;

        private readonly byte[] _usbReport;
        private readonly byte[] _bluetoothReport;

        public OutputReportBuilder()
        {
            _state = new DualShockOutputState();

            _usbReport = new byte[UsbReportLength];
            _bluetoothReport = new byte[BluetoothReportLength];

            InitializeUsb();
            InitializeBluetooth();
        }

        private void InitializeUsb()
        {
            _usbReport[0] = UsbReportId;
        }

        private void InitializeBluetooth()
        {
            _bluetoothReport[0] = BluetoothReportId;
        }

        public byte[] UpdateUsb(FeedbackState feedback)
        {
            _state.Apply(feedback);

            SetUsbFlags();
            SetUsbMotors();
            SetUsbLed();
            SetUsbFlash();

            return _usbReport;
        }

        public byte[] UpdateBluetooth(FeedbackState feedback)
        {
            _state.Apply(feedback);

            return _bluetoothReport;
        }

        private void SetUsbFlags()
        {
            // Motor + LightBar
            _usbReport[1] = 0x03;

            // Extra Flags
            _usbReport[2] = 0x00;
        }

        private void SetUsbMotors()
        {
            _usbReport[4] = _state.SmallMotor;
            _usbReport[5] = _state.LargeMotor;
        }

        private void SetUsbLed()
        {
            _usbReport[6] = _state.Red;
            _usbReport[7] = _state.Green;
            _usbReport[8] = _state.Blue;
        }

        private void SetUsbFlash()
        {
            _usbReport[9] = _state.FlashOn;
            _usbReport[10] = _state.FlashOff;
        }
    }
}