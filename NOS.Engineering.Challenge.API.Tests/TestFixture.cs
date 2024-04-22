using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extension.Configuration;

namespace NOS.Engineering.Challenge.API.Tests
{
    public class TestFixture<TStartup> : IDisposable where TStartup : class
    {
        private readonly TestServer Server;
        private readonly HttpClient _client;
        public readonly IHttpContextAccessor HttpContextAccessor;
        public readonly IServiceProvider ServiceProvider;

        public IDatabase database;
        public IContentsManager manager;

        public TestFixture() 
        {
            var builder = new WebHostBuilder()
                .UseContentRoot("")
                .UseEnvironment("Development")
                .UseConfiguration(new ConfigurationBuilder()
                .SetBasePath("")
                .AddJsonFile("appsettings.Development.json")
                .Build())
                .UseStartup<TStartup>();

            Server = new TestServer(builder);
            HttpContextAccessor = new MockIHttpContextAccessor().HttpContextAccessor;
            ServiceProvider = Server.Host.Services;

            // Add context

            // Add services
            ContentsManager = Server.Host.Services.GetService(typeof(IContentsManager));
        }

        public void Dispose()
        {
            if (_client != null)
            {
                _client.Dispose();
            }
            if (Server != null)
            {
                Server.Dispose();
            }
        }
    }
}
