namespace CommonBasicLibraries.BasicUIProcesses;
public class DefaultThread : IUIThread
{
    void IUIThread.BeginOnUIThread(Action action)
    {
        action();
    }
    void IUIThread.OnUIThread(Action action)
    {
        action();
    }
    Task IUIThread.OnUIThreadAsync(Func<Task> action)
    {
        return Task.Factory.StartNew(action);
    }
}