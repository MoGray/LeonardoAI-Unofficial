

using Leonardo.InitImage.Models;
using System.IO;
using System.Threading.Tasks;

namespace Leonardo.InitImage.Interfaces
{
    public interface IInitImageEndPoint
    {
        Task<bool> InitializeImage(Stream file, string extension);
        Task<UploadInitImageResponse> InitializeImage(string extension);
    }
}
