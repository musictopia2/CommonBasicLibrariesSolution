namespace CommonBasicLibraries.BasicUIProcesses;
/// <summary>
/// this is everything needed so processes can run on ui thread without having to use IProgress interface.
/// </summary>
public interface IUIThread
{
    /// <summary>
    ///   Executes the action on the UI thread asynchronously.
    /// </summary>
    /// <param name="action">The action to execute.</param>
    void BeginOnUIThread(Action action);

    /// <summary>
    ///   Executes the action on the UI thread asynchronously.
    /// </summary>
    /// <param name = "action">The action to execute.</param>
    Task OnUIThreadAsync(Func<Task> action);

    /// <summary>
    ///   Executes the action on the UI thread.
    /// </summary>
    /// <param name = "action">The action to execute.</param>
    void OnUIThread(Action action);
}