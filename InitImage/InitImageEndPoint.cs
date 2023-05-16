
using Leonardo.InitImage.Interfaces;
using Leonardo.InitImage.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Leonardo.InitImage
{
    public sealed class InitImageEndPoint : EndPointBase, IInitImageEndPoint
    {
        protected override string Endpoint { get { return "init-image"; } }
        internal InitImageEndPoint(LeonardoAPI Api) : base(Api) { }

        public async Task<bool> InitializeImage(Stream file, string extension)
        {
            extension = Utilities.TrimPeriodFromExtensions(extension);

            if (file == null)
                throw  new FileNotFoundException("File is Empty");
            if (Utilities.ValidateExtension(extension))
                throw new InvalidDataException("Extension must be in one of these formats, \"png\", \"jpg\", \"jpeg\", \"webp\"");
            var imageRequest = new UploadInitImageRequest(extension);
            var uploadImageResponse = await HttpPost<UploadInitImageResponse>(postData: imageRequest);
            
            return true;
        }

        public async Task<UploadInitImageResponse> InitializeImage(string extension)
        {
            extension = Utilities.TrimPeriodFromExtensions(extension);

            if (!Utilities.ValidateExtension(extension))
                throw new InvalidDataException("Extension must be in one of these formats, \"png\", \"jpg\", \"jpeg\", \"webp\"");
            var imageRequest = new UploadInitImageRequest(extension);
            var uploadImageResponse = await HttpPost<UploadInitImageResponse>(postData: imageRequest);
            var fieldsJson = JsonConvert.DeserializeObject<ExpandoObject>(uploadImageResponse.UploadInitImage.Fields);
            var fieldsString = fieldsJson.ToString();
            var fields = uploadImageResponse.UploadInitImage.Fields;
            var url = uploadImageResponse.UploadInitImage.Url;
            try
            {
                using (var form = new MultipartFormDataContent())
                {
                    using (var fileStream = File.OpenRead(@"E:\Downloads\Sword.jpg"))
                    {
                        var streamContent = new StreamContent(fileStream);
                        form.Add(streamContent, "file", @"Sword.jpg");
                        var fieldsContent = new StringContent(fields, Encoding.UTF8);
                        form.Add(fieldsContent, "fields");

                        var postResponse = await HttpPost<object>(url, form, false);
                        //return postResponse;
                    }
                }
            }
            catch(Exception ex)
            {
                var testc = ex;
            }
            return uploadImageResponse;
        }
    }
}
