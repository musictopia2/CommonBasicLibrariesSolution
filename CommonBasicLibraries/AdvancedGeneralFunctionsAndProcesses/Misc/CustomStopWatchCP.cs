namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.Misc;
public class CustomStopWatchCP
{
    private readonly Stopwatch _thisStop;
    public int MaxTime { get; set; }
    private bool _isStarted;
    public event TimeUpEventHandler? TimeUp;
    public delegate void TimeUpEventHandler();
    public event ProgressEventHandler? Progress;
    private readonly tta1.Timer _timer;
    public delegate void ProgressEventHandler(long TimeLeft); //has to be long now.
    public bool IsRunning => _thisStop.IsRunning;
    public CustomStopWatchCP()
    {
        _thisStop = new Stopwatch();
        _timer = new()
        {
            Interval = 50,
            AutoReset = false
        };
        _timer.Elapsed += TimerElapsed;
    }
    private void TimerElapsed(object? sender, tta1.ElapsedEventArgs e)
    {
        if (_thisStop.IsRunning == false)
        {
            return;
        }
        if (_thisStop.ElapsedMilliseconds > MaxTime)
        {
            _thisStop.Stop();
            Execute.OnUIThread(() =>
            {
                TimeUp?.Invoke();
                });
            return;
        }
        Progress?.Invoke(MaxTime - _thisStop.ElapsedMilliseconds);
        _timer.Start();
    }
    public void StartTimer()
    {
        _isStarted = true;
        _thisStop.Restart();
        PrivateInit();
    }
    public void PauseTimer()
    {
        if (_isStarted == false)
        {
            throw new CustomBasicException("Cannot pause the timer because it was never started");
        }
        _thisStop.Stop();
    }
    public void ContinueTimer()
    {
        if (_isStarted == false)
        {
            throw new CustomBasicException("The timer was never started");
        }
        _thisStop.Start();
        PrivateInit();
    }
    private void PrivateInit()
    {
        _timer.Start();
    }
    public void ManualStop(bool showEvent)
    {
        StopTimer(showEvent);
    }
    public void ManualStop()
    {
        StopTimer();
    }
    private void StopTimer()
    {
        StopTimer(true);
    }
    public long TimeTaken()
    {
        return _thisStop.ElapsedMilliseconds;
    }
    private void StopTimer(bool showEvent)
    {
        _thisStop.Stop();
        if (showEvent)
        {
            TimeUp?.Invoke();
        }
    }
}