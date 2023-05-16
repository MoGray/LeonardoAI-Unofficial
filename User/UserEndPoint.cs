
using Leonardo.User.Interfaces;
using Leonardo.User.Models;
using System.Threading.Tasks;

namespace Leonardo.User
{
    public sealed class UserEndPoint : EndPointBase, IUserEndPoint
    {
        protected override string Endpoint { get { return "me"; } }
        internal UserEndPoint(LeonardoAPI Api) : base(Api) { }

        public async Task<UserDetailsWrapper> GetMyInfo()
        {
            return await HttpGet<UserDetailsWrapper>(this.Url);
        }
    }
}
