

using Leonardo.User.Models;
using System.Threading.Tasks;

namespace Leonardo.User.Interfaces
{
    public interface IUserEndPoint
    {
        Task<UserDetailsWrapper> GetMyInfo();
    }
}
