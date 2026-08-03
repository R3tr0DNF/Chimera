using Chimera.Models;
using Chimera.Models.Xbox;
using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;
using Chimera.Services.Translator;
using Chimera.Models.Feedback;
using Chimera.Services.Feedback;
using HidSharp;

namespace Chimera.Services.VirtualController
{
    internal class XboxController
    {
        private readonly ViGEmClient _client;
        private readonly IXbox360Controller _controller;
        private readonly DualShockFeedback _feedback;
        
        public XboxController(HidStream stream)
        {
            _client = new ViGEmClient();

            _controller = _client.CreateXbox360Controller();
            _controller.FeedbackReceived += Controller_FeedbackReceived;
            _feedback = new DualShockFeedback(stream);
        }

        public void Connect()
        {
            _controller.Connect();
        }

        public void Disconnect()
        {
            _controller.Disconnect();
        }

        public void Update(XboxState state)
        {
            _controller.ResetReport();

            UpdateButtons(state);
            UpdateDPad(state.DPad);
            UpdateTriggers(state);
            UpdateSticks(state);

            _controller.SubmitReport();
        }

        private void UpdateButtons(XboxState state)
        {
            //buttons
            _controller.SetButtonState(Xbox360Button.A, state.A);
            _controller.SetButtonState(Xbox360Button.B, state.B);
            _controller.SetButtonState(Xbox360Button.X, state.X);
            _controller.SetButtonState(Xbox360Button.Y, state.Y);

            //shoulder buttons
            _controller.SetButtonState(Xbox360Button.LeftShoulder, state.LB);
            _controller.SetButtonState(Xbox360Button.RightShoulder, state.RB);

            //System
            _controller.SetButtonState(Xbox360Button.Back, state.Back);
            _controller.SetButtonState(Xbox360Button.Start, state.Start);
            
            //thumb 
            _controller.SetButtonState(Xbox360Button.LeftThumb, state.LeftThumb);
            _controller.SetButtonState(Xbox360Button.RightThumb, state.RightThumb);
        }

        private void UpdateDPad(DPadDirection direction)
        {
            _controller.SetButtonState(Xbox360Button.Up, false);
            _controller.SetButtonState(Xbox360Button.Down, false);
            _controller.SetButtonState(Xbox360Button.Left, false);
            _controller.SetButtonState(Xbox360Button.Right, false);

            switch (direction)
            {
                case DPadDirection.Up:
                    _controller.SetButtonState(Xbox360Button.Up, true);
                    break;
                
                case DPadDirection.Down:
                    _controller.SetButtonState(Xbox360Button.Down, true);
                    break;
                
                case DPadDirection.Left:
                    _controller.SetButtonState(Xbox360Button.Left, true);
                    break;
                
                case DPadDirection.Right:
                    _controller.SetButtonState(Xbox360Button.Right, true);
                    break;
                
                case DPadDirection.UpLeft:
                    _controller.SetButtonState(Xbox360Button.Up, true);
                    _controller.SetButtonState(Xbox360Button.Left, true);
                    break;
                
                case DPadDirection.UpRight:
                    _controller.SetButtonState(Xbox360Button.Up, true);
                    _controller.SetButtonState(Xbox360Button.Right, true);
                    break;
                
                case DPadDirection.DownLeft:
                    _controller.SetButtonState(Xbox360Button.Down, true);
                    _controller.SetButtonState(Xbox360Button.Left, true);
                    break;

                case DPadDirection.DownRight:
                    _controller.SetButtonState(Xbox360Button.Down, true);
                    _controller.SetButtonState(Xbox360Button.Right, true);
                    break;
                
                case DPadDirection.Neutral:
                default:
                     break;
                

            }

        }

        private void UpdateTriggers(XboxState state)
        {
            _controller.SetSliderValue(Xbox360Slider.LeftTrigger, state.LT);
            _controller.SetSliderValue(Xbox360Slider.RightTrigger, state.RT);
        }

        private void UpdateSticks(XboxState state)
        {
            _controller.SetAxisValue(Xbox360Axis.LeftThumbX, AxisTranslator.ToXboxAxis(state.LeftStick.X));
            _controller.SetAxisValue(Xbox360Axis.LeftThumbY, AxisTranslator.ToXboxAxis(state.LeftStick.Y,true));

            _controller.SetAxisValue(Xbox360Axis.RightThumbX, AxisTranslator.ToXboxAxis(state.RightStick.X));
            _controller.SetAxisValue(Xbox360Axis.RightThumbY, AxisTranslator.ToXboxAxis(state.RightStick.Y,true));

        }

        private void Controller_FeedbackReceived(object sender, Xbox360FeedbackReceivedEventArgs e)
        {
            FeedbackState state = new FeedbackState()
            {
                LargeMotor = e.LargeMotor,
                SmallMotor = e.SmallMotor,
                LedNumber = e.LedNumber
            };
            
            _feedback.Update(state);
            
  
        }

        




    }
}