﻿namespace CommonBasicLibraries.CustomConfigurationProviders.Text;
public class TextConfigurationProvider(FileConfigurationSource source, string delimiter) : FileConfigurationProvider(source)
{
    public override void Load(Stream stream)
    {
        Data = TextStreamConfigurationProvider.Read(stream, delimiter);
    }
}