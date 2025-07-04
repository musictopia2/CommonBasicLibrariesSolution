namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.UploadHelpers;
public static class ContentType
{
    public static class Image
    {
        public const string Jpeg = "image/jpeg";
        public const string Png = "image/png";
        public const string Webp = "image/webp";
        public const string Gif = "image/gif";
        public const string Bmp = "image/bmp";
        public const string Tiff = "image/tiff"; // <-- you mentioned this
        public const string Svg = "image/svg+xml";
    }

    public static class Video
    {
        public const string Mp4 = "video/mp4";
        public const string Webm = "video/webm";
        public const string Ogg = "video/ogg";
        public const string Wmv = "video/x-ms-wmv"; // <-- you mentioned this
        public const string Avi = "video/x-msvideo";
        public const string Mpeg = "video/mpeg";
    }

    public static class Audio
    {
        public const string Mp3 = "audio/mpeg"; // <-- you mentioned this
        public const string Wav = "audio/wav";
        public const string Ogg = "audio/ogg";
        public const string Webm = "audio/webm";
        public const string Midi = "audio/midi";
    }

    public static class Application
    {
        public const string Pdf = "application/pdf";
        public const string Json = "application/json";
        public const string Zip = "application/zip";
        public const string Xml = "application/xml";
        public const string MsWord = "application/msword";
        public const string MsExcel = "application/vnd.ms-excel";
        public const string MsPowerPoint = "application/vnd.ms-powerpoint";
        public const string OctetStream = "application/octet-stream";
    }

    public static class Text
    {
        public const string Plain = "text/plain";
        public const string Html = "text/html";
        public const string Css = "text/css";
        public const string Csv = "text/csv";
        public const string Markdown = "text/markdown";
    }
}