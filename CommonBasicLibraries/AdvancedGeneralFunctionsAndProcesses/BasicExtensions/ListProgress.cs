namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions
{
    public static class ListProgress
    {
        public static string ProgressString<T>(this BasicList<T> thisList, T thisItem, string miscText)
        {
            int index = thisList.IndexOf(thisItem);
            if (index == -1)
            {
                throw new CustomBasicException("Item Not Found");
            }
            index += 1;
            return $"{ miscText} # {index} of {thisList.Count} on {DateTime.Now}";
        }
        public static void WriteProgress(this string thisStr)
        {
            Console.WriteLine($"{thisStr} on {DateTime.Now}");
        }
        public static void WriteProgress<T>(this BasicList<T> thisList, T thisItem, string miscText)
        {
            string thisProgress = thisList.ProgressString(thisItem, miscText);
            Console.WriteLine(thisProgress);
        }
    }
}