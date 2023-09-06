using AuthMe.Basic;
using AuthMe.Internal;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace AuthMe.Extensions
{
    public static class BasicExtensions 
    {
        public static void AddBasicAuthentication(this IServiceCollection services)
        {
            services.AddSingleton<IIdentityProvider, DefaultIdentityProvider>();
            services.AddAuthentication(Constants.BasicAutheticationScheme).AddScheme<AuthenticationSchemeOptions,
                BasicAuthenticationHandler>(Constants.BasicAutheticationScheme, null);
        }
        public static void AddBasicAuthentication(this IServiceCollection services, BasicAuthSettings settings)
        {
            services.AddSingleton<IIdentityProvider>(x => new DefaultIdentityProvider(settings));
            services.AddAuthentication(Constants.BasicAutheticationScheme).AddScheme<AuthenticationSchemeOptions,
                BasicAuthenticationHandler>(Constants.BasicAutheticationScheme, null);
        }
        public static void AddBasicAuthentication<T>(this IServiceCollection services)
        {
            services.AddSingleton<IIdentityProvider, DefaultIdentityProvider>();
            services.AddAuthentication(Constants.BasicAutheticationScheme).AddScheme<AuthenticationSchemeOptions,
                BasicAuthenticationHandler>(Constants.BasicAutheticationScheme, null);
        }
    }
}
