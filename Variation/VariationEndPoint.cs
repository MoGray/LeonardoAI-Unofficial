
using Leonardo.Variation.Interfaces;
using Leonardo.Variation.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Leonardo.Variation
{
    public sealed class VariationEndPoint : EndPointBase, IVariationEndPoint
    {
        protected override string Endpoint { get { return "variations"; } }
        internal VariationEndPoint(LeonardoAPI Api) : base(Api) { }

        public async Task<string> CreateUpscale(string id)
        {
            var createUpscaleRequest = new CreateUpscaleRequest(id);
            var response = await HttpPost<CreateUpscaleResponse>($"{this.Url}/upscale", createUpscaleRequest);
            return response.UpscaleJob.Id;
        }

        public async Task<List<VariationImage>> GetVariationById(string id)
        {
            var response = await HttpGet<GetVariationResponse>($"{this.Url}/{id}");
            return response.Images;
        }
    }
}
