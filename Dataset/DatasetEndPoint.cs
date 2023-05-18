

using Leonardo.Dataset.Interfaces;
using Leonardo.Dataset.Models;
using Leonardo.GenericModels;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Leonardo.Dataset
{
    public class DatasetEndPoint : EndPointBase, IDatasetEndPoint
    {
        protected override string Endpoint { get { return "datasets"; } }
        internal DatasetEndPoint(LeonardoAPI Api) : base(Api) { }

        public async Task<string> CreateDataset(string name, string description)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name must not be null");
            var createDatasetRequest = new CreateDatasetRequest(name, description);
            var response = await HttpPost<CreateDatasetResponse>(postData: createDatasetRequest);
            return response.CreateDataset.Id;
        }

        public async Task<GetDatasetResponse> GetDataset(string id)
        {
            return await HttpGet<GetDatasetResponse>($"{this.Url}/{id}");
        }
        public async Task<string> DeleteDataset(string id)
        {
            var response = await HttpDelete<DeleteDatasetResponse>($"{this.Url}/{id}");
            return response.DeletedDataset.Id;
        }

        public async Task<string> UploadDatasetImage(string datasetId, Stream file, string extension, string fileName = null)
        {
            extension = Utilities.TrimPeriodFromExtensions(extension);

            if (file == null)
                throw new FileNotFoundException("File is Empty");
            if (Utilities.ValidateExtension(extension))
                throw new InvalidDataException("Extension must be in one of these mime types, \"png\", \"jpg\", \"jpeg\", \"webp\"");
            var imageRequest = new UploadDatasetImageRequest(extension);
            var uploadImageResponse = await HttpPost<UploadDatasetImageResponse>($"{this.Url}/{datasetId}/upload" ,postData: imageRequest);
            var imageUpload = new ImageUpload()
            {
                Stream = file,
                Fields = uploadImageResponse.Image.Fields,
                FileName = fileName,
                Extension = extension,
                Url = uploadImageResponse.Image.Url
            };
            await UploadImage(imageUpload);
            return uploadImageResponse.Image.Id;
        }

        public async Task<string> UploadGeneratedImageToDataset(string datasetId, string generatedImageId)
        {
            var imageRequest = new UploadDatasetGeneratedImageRequest(generatedImageId);
            var uploadImageResponse = await HttpPost<UploadDatasetGeneratedImageResponse>($"{this.Url}/{datasetId}/upload", postData: imageRequest);
            return uploadImageResponse.Image.Id;
        }
    }
}
