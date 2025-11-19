namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
public static class DelegateExtensions
{
    extension (Func<Task>? task)
    {
        public async Task InvokeAsync()
        {
            if (task is null)
            {
                return;
            }
            await task.Invoke();
        }
    }
    extension<T>(Func<T, Task>? task)
    {
        public async Task InvokeAsync(T item)
        {
            if (task is null)
            {
                return;
            }
            await task.Invoke(item);
        }
    }
}