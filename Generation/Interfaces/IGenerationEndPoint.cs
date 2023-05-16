

using Leonardo.Generation.Models;
using System.Threading.Tasks;

namespace Leonardo.Generation.Interfaces
{
    public interface IGenerationEndPoint
    {
        Task<CreateGenerationResponse> GenerateImageGeneration(string prompt);
        Task<CreateGenerationResponse> GenerateImageGeneration(CreateGenerationRequest request);
        Task<GetSingleGenerationResponse> GetGenerationImages(string id);
    }
}
