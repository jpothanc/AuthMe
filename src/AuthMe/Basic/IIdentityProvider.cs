using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("AuthMeTest")]
namespace AuthMe.Basic
{
    internal interface IIdentityProvider
    {
        Task<Claim[]> Authenticate(string username, string password);
    }
}
