namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.TextHandling;
public abstract class FlatTextAction<T> : ITextAction
    where T : class, IFlatMarker, new()
{
    private const string _delimiter = "\t";
    protected abstract string Prefix { get; }
    private string PayloadPrefix => Prefix + "|";
    protected abstract Task ExecuteAsync(T item);
    public string Serialize(T item)
    {
        ArgumentNullException.ThrowIfNull(item);

        BasicList<T> entries = new();
        entries.Add(item);

        return PayloadPrefix + entries.SerializeText(_delimiter);
    }

    bool ITextAction.CanHandle(string text)
    {
        return text.StartsWith(PayloadPrefix, StringComparison.Ordinal);
    }

    Task ITextAction.HandleAsync(string text)
    {
        string prefix = PayloadPrefix;

        if (!text.StartsWith(prefix, StringComparison.Ordinal))
        {
            throw new CustomBasicException(
                "The contents do not have the expected action prefix.");
        }

        string contents = text[prefix.Length..];

        BasicList<T> entries =
            contents.DeserializeDelimitedTextList<T>(_delimiter);

        if (entries.Count != 1)
        {
            throw new CustomBasicException(
                "Expected exactly one entry for this text action.");
        }

        return ExecuteAsync(entries[0]);
    }
}