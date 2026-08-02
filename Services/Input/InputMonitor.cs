using Chimera.Models.Inputs;
using Chimera.Services.Parser;
using Chimera.Services;
using HidSharp;
using Chimera.Models;


namespace Chimera.Services.Input
{
    internal class InputMonitor
    {
        private readonly HidStream _stream;

        private readonly byte[] _currentReport;
        private readonly byte[] _previousReport;

        public DualShockState CurrentState { get; private set; }

        public InputMonitor(HidStream stream)
        {
            _stream = stream;

            _currentReport = new byte[64];
            _previousReport = new byte[64];

            CurrentState = new DualShockState();
        }

        public bool TryRead()
        {
            _stream.Read(_currentReport);

            var changes =
                ChangeDetector.DetectChanges(
                    _previousReport,
                    _currentReport);
            
            List<ByteChange> visibleChanges = new();
            
            foreach (ByteChange change in changes)
            {
                if (!ReportFilter.ShouldIgnore(change.Index))
                {
                    visibleChanges.Add(change);
                }
            }

            Array.Copy(_currentReport,_previousReport,_currentReport.Length);

            if(visibleChanges.Count == 0)
            {
                return false;
            }

            CurrentState = DualShockParser.Parse(_currentReport);

            return true;
            
        }
    }
}