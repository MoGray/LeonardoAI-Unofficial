

using Leonardo.Generation.Models;
using System.Threading.Tasks;

namespace Leonardo.Generation.Interfaces
{
    public interface IGenerationEndPoint
    {
        Task<string> GenerateImageGeneration(string prompt);
        Task<string> GenerateImageGeneration(CreateGenerationRequest request);
        Task<GetSingleGenerationResponse> GetGenerationImages(string id);
        Task<UserGenerations> GetGenerationsByUserId(string id, int offset = 0, int limit = 10);
        Task<string> DeleteGeneration(string id);
    }
}
