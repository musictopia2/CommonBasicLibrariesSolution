namespace CommonBasicLibraries.CustomConfigurationProviders;
public class TextConfigurationSource : FileConfigurationSource
{
    public string Delimiter { get; set; } = dd1.VBTab;
    public override IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        EnsureDefaults(builder);
        return new TextConfigurationProvider(this, Delimiter);
    }
}