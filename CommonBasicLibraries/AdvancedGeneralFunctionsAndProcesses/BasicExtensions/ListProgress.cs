namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
public static class ListProgress
{
    extension<T>(BasicList<T> list)
    {
        public string ProgressString(T item, string miscText)
        {
            int index = list.IndexOf(item);
            if (index == -1)
            {
                throw new CustomBasicException("Item Not Found");
            }
            index += 1;
            return $"{miscText} # {index} of {list.Count} on {DateTime.Now}";
        }
        public void WriteProgress(T item, string miscText)
        {
            string thisProgress = list.ProgressString(item, miscText);
            Console.WriteLine(thisProgress);
        }
    }
    extension(string payLoad)
    {
        public void WriteProgress()
        {
            Console.WriteLine($"{payLoad} on {DateTime.Now}");
        }
    }   
}