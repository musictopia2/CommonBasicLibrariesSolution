namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers;
public static class FileHelpers
{
    public static async Task SaveObjectAsync<T>(string path, T thisObject) //needs generics so it knows that somebody handles the serializing now.
    {
        string thisText = await js.SerializeObjectAsync(thisObject);
        await ff.WriteTextAsync(path, thisText!, false);
    }
    public static void SaveObject<T>(string path, T thisObject)
    {
        string text = js.SerializeObject(thisObject);
        ff.WriteText(path, text!, false);
    }
    public static async Task<T> RetrieveSavedObjectAsync<T>(string path)
    {
        string thisText;
        thisText = await ff.AllTextAsync(path);
        T thisT = await js.DeserializeObjectAsync<T>(thisText);
        return thisT!;
    }
    public static T RetrieveSavedObject<T>(string path)
    {
        string thisText = ff.AllText(path);
        return js.DeserializeObject<T>(thisText);
    }
}