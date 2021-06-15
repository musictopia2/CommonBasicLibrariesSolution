using CommonBasicLibraries.BasicDataSettingsAndProcesses;
using System;
using System.Threading.Tasks;
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.UIHelpers
{
    public static class Execute
    {
        /// <summary>
        ///   Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public static void BeginOnUIThread(this Action action)
        {
            UIPlatform.CurrentThread.BeginOnUIThread(action);
        }

        /// <summary>
        ///   Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name = "action">The action to execute.</param>
        public static Task OnUIThreadAsync(this Func<Task> action)
        {
            return UIPlatform.CurrentThread.OnUIThreadAsync(action);
        }

        /// <summary>
        ///   Executes the action on the UI thread.
        /// </summary>
        /// <param name = "action">The action to execute.</param>
        public static void OnUIThread(this Action action)
        {
            UIPlatform.CurrentThread.OnUIThread(action);
        }

        //wpf will handle one way.
        //xamarin forms will handle another.
        //if doing via unit tests, then use default.
    }
}
