namespace CommonBasicLibraries.CustomConfigurationProviders;
public class TextStreamConfigurationSource : StreamConfigurationSource
{
    public string Delimiter { get; set; } = dd1.VBTab;
    public override IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new TextStreamConfigurationProvider(this, Delimiter);
    }
}