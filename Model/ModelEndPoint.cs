
using Leonardo.Model.Interfaces;
using Leonardo.Model.Models;
using System;
using System.Threading.Tasks;

namespace Leonardo.Model
{
    public class ModelEndPoint : EndPointBase, IModelEndPoint
    {
        protected override string Endpoint { get { return "models"; } }
        internal ModelEndPoint(LeonardoAPI Api) : base(Api) { }

        public async Task<string> TrainModel(string name, string datasetId, string instancePrompt)
        {
            var request = new TrainModelRequest(name, datasetId, instancePrompt);
            return await TrainModel(request);
        }

        public async Task<string> TrainModel(TrainModelRequest request)
        {
            try
            {
                request.Validate();
                var response = await HttpPost<TrainModelResponse>(postData: request);
                return response.TrainedModel.Id;
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message, nameof(e));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<GetModel> GetModel(string id)
        {
            var response = await HttpGet<GetModelResponse>($"{this.Url}/{id}");
            return response.TrainedModel;
        }

        public async Task<string> DeleteModel(string id)
        {
            var response = await HttpDelete<DeleteModelResponse>($"{this.Url}/{id}");
            return response.DeletedModel.Id;
        }
    }
}
