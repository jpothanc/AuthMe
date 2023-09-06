using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthMe.Basic
{
    internal class DefaultIdentityProvider : IIdentityProvider
    {
        private readonly BasicAuthSettings _settings;
        public DefaultIdentityProvider(IOptions<BasicAuthSettings> settings)
        {
            _settings = settings.Value;
        }

        public DefaultIdentityProvider(BasicAuthSettings settings)
        {
            _settings = settings;
        }

        public Task<Claim[]> Authenticate(string username, string password)
        {
            if(_settings.Users.TryGetValue(username, out var value) && value == password)
            {
                return Task.FromResult(new Claim[]
                {
                   new Claim(ClaimTypes.NameIdentifier, username),
                   new Claim(ClaimTypes.Name, username)
                });
            }

            return Task.FromResult<Claim[]>(null);

        }
    }
}
