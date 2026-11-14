namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.TextHandling;
public class TextActionDispatcher
{
    private readonly BasicList<ITextAction> _actions;

    public TextActionDispatcher(IActionCatalog catalog)
    {
        _actions = new();

        foreach (ITextAction action in catalog.GetActions())
        {
            _actions.Add(action);
        }
    }

    public async Task<bool> DispatchAsync(string text)
    {
        ITextAction? matchingAction = null;

        foreach (ITextAction action in _actions)
        {
            if (!action.CanHandle(text))
            {
                continue;
            }

            if (matchingAction is not null)
            {
                throw new CustomBasicException(
                    "More than one text action recognizes these contents.");
            }

            matchingAction = action;
        }

        if (matchingAction is null)
        {
            return false;
        }

        await matchingAction.HandleAsync(text);
        return true;
    }
}