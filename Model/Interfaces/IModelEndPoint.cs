

using Leonardo.Model.Models;
using System.Threading.Tasks;

namespace Leonardo.Model.Interfaces
{
    public interface IModelEndPoint
    {
        Task<string> TrainModel(string name, string datasetId, string instancePrompt);
        Task<string> TrainModel(TrainModelRequest request);
        Task<GetModel> GetModel(string id);
        Task<string> DeleteModel(string id);
    }
}
