using System;
using System.Threading.Tasks;
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions
{
    public static class DelegateExtensions
    {
        //this means since doing the question mark does not work because of warnings, this fix that.
        public static async Task InvokeAsync(this Func<Task>? task)
        {
            if (task is null)
            {
                return;
            }
            await task.Invoke();
        }
        public static async Task InvokeAsync<T>(this Func<T, Task>? task, T item)
        {
            if (task is null)
            {
                return;
            }
            await task.Invoke(item);
        }
    }
}