
using Leonardo.InitImage.Models;
using System.IO;
using System.Threading.Tasks;

namespace Leonardo.InitImage.Interfaces
{
    public interface IInitImageEndPoint
    {
        Task<string> InitializeImage(Stream file, string extension, string fileName = null);
        Task<GetInitImage> GetInitImage(string id);
        Task<string> DeleteInitImage(string id);
    }
}
