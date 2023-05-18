
using System.IO;

namespace Leonardo.GenericModels
{
    internal class ImageUpload
    {
        public Stream Stream { get; set; }
        public string Fields { get; set; }
        public string Url { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
    }
}
