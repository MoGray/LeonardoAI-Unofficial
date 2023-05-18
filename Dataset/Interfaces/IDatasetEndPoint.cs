
using Leonardo.Dataset.Models;
using System.IO;
using System.Threading.Tasks;

namespace Leonardo.Dataset.Interfaces
{
    public interface IDatasetEndPoint
    {
        Task<string> CreateDataset(string name, string description);
        Task<GetDatasetResponse> GetDataset(string id);
        Task<string> DeleteDataset(string id);
        Task<string> UploadDatasetImage(string datasetId, Stream file, string extension, string fileName = null);
        Task<string> UploadGeneratedImageToDataset(string datasetId, string generatedImageId);
    }
}
