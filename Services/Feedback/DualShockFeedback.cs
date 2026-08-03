using HidSharp;
using Chimera.Models.Feedback;

namespace Chimera.Services.Feedback
{
    internal class DualShockFeedback
    {
        private readonly HidStream _stream;

        private readonly OutputReportBuilder _builder;

        private readonly OutputSettings _settings;

        public DualShockFeedback(HidStream stream)
        {
            _stream = stream;

            _builder = new OutputReportBuilder();

            _settings = new OutputSettings();
        }

        public void Update(FeedbackState state)
        {
            byte[] report = _builder.UpdateUsb(state);

            SendReport(report);
        }

        private void SendReport(byte[] report)
        {
            for (int i = 0; i < _settings.SendCount; i++)
            {
                _stream.Write(report);

                _stream.Flush();


            }
        }
    }
}