

using Leonardo.Variation.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Leonardo.Variation.Interfaces
{
    public interface IVariationEndPoint
    {
        Task<string> CreateUpscale(string id);
        Task<List<VariationImage>> GetVariationById(string id);
    }
}
