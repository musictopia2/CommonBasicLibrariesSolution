namespace CommonBasicLibraries.BasicUIProcesses;
public static class Execute
{
    extension(Action action)
    {
        public void BeginOnUIThread()
        {
            UIPlatform.CurrentThread.BeginOnUIThread(action);
        }
        public void OnUIThread()
        {
            UIPlatform.CurrentThread.OnUIThread(action);
        }
    }
    extension(Func<Task> action)
    {
        public Task OnUIThreadAsync()
        {
            return UIPlatform.CurrentThread.OnUIThreadAsync(action);
        }
    }
}
