
using Leonardo.InitImage.Interfaces;
using Leonardo.InitImage.Models;
using Leonardo.GenericModels;
using System.IO;
using System.Threading.Tasks;

namespace Leonardo.InitImage
{
    public sealed class InitImageEndPoint : EndPointBase, IInitImageEndPoint
    {
        protected override string Endpoint { get { return "init-image"; } }
        internal InitImageEndPoint(LeonardoAPI Api) : base(Api) { }

        public async Task<string> InitializeImage(Stream file, string extension, string fileName = null)
        {
            extension = Utilities.TrimPeriodFromExtensions(extension);

            if (file == null)
                throw  new FileNotFoundException("File is Empty");
            if (Utilities.ValidateExtension(extension))
                throw new InvalidDataException("Extension must be in one of these mime types, \"png\", \"jpg\", \"jpeg\", \"webp\"");
            var imageRequest = new UploadInitImageRequest(extension);
            var uploadImageResponse = await HttpPost<UploadInitImageResponse>(postData: imageRequest);
            var imageUpload = new ImageUpload()
            {
                Stream = file,
                Fields = uploadImageResponse.UploadInitImage.Fields,
                FileName = fileName,
                Extension = extension,
                Url = uploadImageResponse.UploadInitImage.Url
            };
            await UploadImage(imageUpload);
            return uploadImageResponse.UploadInitImage.Id;
        }

        public async Task<GetInitImage> GetInitImage(string id)
        {
            var response = await HttpGet<GetInitImageResponse>($"{this.Url}/{id}");
            return response.GetInitImage;
        }

        public async Task<string> DeleteInitImage(string id)
        {
            var response = await HttpDelete<DeleteInitImageResponse>($"{this.Url}/{id}");
            return response.DeleteInitImage.Id;
        }
    }
}
