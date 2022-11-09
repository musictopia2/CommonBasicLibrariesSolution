namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers;
public static class FileHelpers
{
    public static async Task SaveObjectAsync<T>(string path, T thisObject) //needs generics so it knows that somebody handles the serializing now.
    {
        string thisText = await js1.SerializeObjectAsync(thisObject);
        await ff1.WriteTextAsync(path, thisText!, false);
    }
    public static void SaveObject<T>(string path, T thisObject)
    {
        string text = js1.SerializeObject(thisObject);
        ff1.WriteText(path, text!, false);
    }
    public static async Task<T> RetrieveSavedObjectAsync<T>(string path)
    {
        string thisText;
        thisText = await ff1.AllTextAsync(path);
        T thisT = await js1.DeserializeObjectAsync<T>(thisText);
        return thisT!;
    }
    public static T RetrieveSavedObject<T>(string path)
    {
        string thisText = ff1.AllText(path);
        return js1.DeserializeObject<T>(thisText);
    }
}