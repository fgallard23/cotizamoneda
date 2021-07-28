using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cotizacion.Moneda.Test
{
    public class ClientProvider<TStartup> : IDisposable
    {
        // variable
        private TestServer Server;
        public HttpClient Client { get; }
        
        // required constructor 
        public ClientProvider()
            : this(Path.Combine(""))
        {
        }

        /// <summary>
        /// Get Root Project Path
        /// </summary>
        /// <param name="projectRelativePath"></param>
        /// <param name="startupAssembly"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static string GetProjectPath(string projectRelativePath, Assembly startupAssembly)
        {
            var projectName = startupAssembly.GetName().Name; // assembly project
            var applicationBasePath = AppContext.BaseDirectory; // <main_path_project>\bin\Debug\net5.0\
            var directoryInfo = new DirectoryInfo(applicationBasePath);

            do
            {
                // parent project 
                directoryInfo = directoryInfo.Parent;
                var projectDirectoryInfo = new DirectoryInfo(Path.Combine(directoryInfo.FullName, projectRelativePath));
                
                // .csproject exists 
                if (projectDirectoryInfo.Exists)
                    if (new FileInfo(Path.Combine(projectDirectoryInfo.FullName, projectName, $"{projectName}.csproj")).Exists)
                        return Path.Combine(projectDirectoryInfo.FullName, projectName);
            }
            while (directoryInfo.Parent != null);

            throw new Exception($"Project root could not be located using the application root {applicationBasePath}.");
        }
        
        /// <summary>
        /// Initial Services Config
        /// </summary>
        /// <param name="services"></param>
        protected virtual void InitializeServices(IServiceCollection services)
        {
            var startupAssembly = typeof(TStartup).GetTypeInfo().Assembly;

            var manager = new ApplicationPartManager
            {
                ApplicationParts =
                {
                    new AssemblyPart(startupAssembly)
                },
                FeatureProviders =
                {
                    new ControllerFeatureProvider(),
                    new ViewComponentFeatureProvider()
                }
            };

            services.AddSingleton(manager);
        }

        /// <summary>
        /// Client Provider Config 
        /// </summary>
        /// <param name="relativeTargetProjectParentDir"></param>
        private ClientProvider(string relativeTargetProjectParentDir)
        {
            var startupAssembly = typeof(TStartup).GetTypeInfo().Assembly; // assembly Startup main project
            var contentRoot = GetProjectPath(relativeTargetProjectParentDir, startupAssembly);

            // read json file path
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(contentRoot)
                .AddJsonFile("appsettings.json");

            var webHostBuilder = new WebHostBuilder()
                .UseContentRoot(contentRoot)
                .ConfigureServices(InitializeServices)
                .UseConfiguration(configurationBuilder.Build())
                .UseEnvironment("Development")
                .UseStartup(typeof(TStartup));

            // Create instance of test server
            Server = new TestServer(webHostBuilder);

            // Add configuration for client
            Client = Server.CreateClient();
            Client.BaseAddress = new Uri("http://localhost:5001"); // app and port
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        
        // Dispose
        public void Dispose()
        {
            Client.Dispose();
            Server.Dispose();
        }
    }
}