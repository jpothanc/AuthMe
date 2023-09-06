using AuthMe.Basic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Xunit;
namespace AuthMeTest
{
    public class AuthTest : BaseTest
    {
        [Fact]
        public void Load_Auth_Settings_Should_Succeed()
        {
            //IConfiguration does not have bind method.
            //You will need to install Microsoft.Extensions.Configuration.Binder
            var settings = new BasicAuthSettings();
            Configuration.GetSection("BasicAuth").Bind(settings);
            Assert.True(settings.Users.Count > 0);
        }

        [Theory]
        [InlineData("user1", "user1_pass")]
        public void Authentication_For_Configured_Users_Using_DI_Should_Succeed(string username, string password)
        {
            var identityProvider = Provider.GetService<IIdentityProvider>();
            var result = identityProvider?.Authenticate(username, password).Result;
            Assert.True(ValidateClaims(result, username));
        }

        [Theory]
        [InlineData("user1", "user1_pass")]
        [InlineData("user2", "user2_pass")]
        public void Authentication_For_Configured_Users_Should_Succeed(string username, string password)
        {
            var identityProvider = new DefaultIdentityProvider(basicAuthSettings);
            var result = identityProvider?.Authenticate(username, password).Result;
            Assert.True(ValidateClaims(result, username));

        }

        [Theory]
        [InlineData("user1", "p1xxxx")]
        [InlineData("user2", "p2xxxx")]
        public void Authentication_For_Configured_Users_With_Wrong_Password_Should_Fail(string username, string password)
        {
            var identityProvider = new DefaultIdentityProvider(basicAuthSettings);
            var result = identityProvider?.Authenticate(username, password).Result;
            Assert.Null(result);
        }

        [Theory]
        [InlineData("u1xxxx", "xxxx")]
        [InlineData("u2xxxx", "xxxx")]
        public void Authentication_For_Unknown_Users_Should_Fail(string username, string password)
        {
            var identityProvider = new DefaultIdentityProvider(basicAuthSettings);
            var result = identityProvider?.Authenticate(username, password).Result;
            Assert.Null(result);
        }

        private bool ValidateClaims(Claim[]? result, string username)
        {
            if(result == null || result?.Length != 2)
                return false;

            if(result?[0].Type != ClaimTypes.NameIdentifier 
                || result?[0].Value != username 
                || result?[1].Type != ClaimTypes.Name 
                || result?[1].Value != username)
                return false;

            return true;

        }
    }
}