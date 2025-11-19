namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.FileFunctions;
public static class FileExtensions
{
    extension(string payLoad)
    {
        public async Task<string?> FixBase64ForFileDataAsync()
        {
            // *** Need to clean up the text in case it got corrupted travelling in an XML file
            // i think its best to have as public.  because its possible its only corrupted because of this.
            // has had the experience before with smart phones.
            // however; with mango and windows phones 7; I can use a compact edition database (which would be very helpful).
            // if doing this; then what would have to happen is I would have to have a method to check back in the music information.
            // maybe needs to be xml afterall (don't know though).  otherwise; may have to do serializing/deserializing.
            // some stuff is iffy at this point.
            // at this point, its proven to work.  so i will keep.  this will be good even for iphones/androids, other operating systems even
            StringBuilder sbText;
            string? thisStr = default;
            await Task.Run(() =>
            {
                sbText = new StringBuilder(payLoad, payLoad.Length);
                sbText.Replace(@"\r\n", string.Empty);
                sbText.Replace(" ", string.Empty);
                thisStr = sbText.ToString();
            });
            return thisStr;
        }
        public string? FixBase64ForFileData()
        {
            StringBuilder sbText;
            string? thisStr;
            sbText = new StringBuilder(payLoad, payLoad.Length);
            sbText.Replace(@"\r\n", string.Empty);
            sbText.Replace(" ", string.Empty);
            thisStr = sbText.ToString();
            return thisStr;
        }
        public async Task SaveBinaryDataAsync(string path)
        {
            using var fins = new FileStream(bb1.GetCleanedPath(path), FileMode.Create);
            byte[] Bytes = Convert.FromBase64String(payLoad);
            await fins.WriteAsync(Bytes);
            await fins.FlushAsync();
        }
    }
}