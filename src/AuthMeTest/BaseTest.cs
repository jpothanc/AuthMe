using AuthMe.Basic;
using AuthMe.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthMeTest
{
    public abstract class BaseTest
    {
        protected readonly IServiceProvider Provider;
        protected readonly IConfiguration Configuration;
        protected readonly BasicAuthSettings basicAuthSettings;

        public BaseTest()
        {
            var services  = new ServiceCollection();
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Test.json").Build();
            services.AddBasicAuthentication();
            basicAuthSettings = new BasicAuthSettings();
            Configuration.GetSection("BasicAuth").Bind(basicAuthSettings);

            services.Configure<BasicAuthSettings>(c =>
            {
                foreach (var kvp in basicAuthSettings.Users)
                {
                   c.Users.Add(kvp.Key, kvp.Value);
                }
            });
                
            Provider = services.BuildServiceProvider();
        }

    }
}
