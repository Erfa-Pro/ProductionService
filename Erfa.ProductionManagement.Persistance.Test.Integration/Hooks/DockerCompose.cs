using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Services;
using Erfa.ProductionManagement.Persistance.Test.Integration.Hooks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



namespace Erfa.ProductionManagement.Persistence.Test.Integration.Hooks
{
    public class DockerCompose<TStartup> : WebApplicationFactory<IStartup> where TStartup : class
    {
        private static Dictionary<string, ICompositeService> _services = new Dictionary<string, ICompositeService>();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            builder.ConfigureTestServices(services =>
            {
                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<ErfaDbContext>();

                    if (context.Database.EnsureCreated())
                    {
                        context.Database.EnsureDeleted();
                    }
                    context.Database.EnsureCreated();
                    try
                    {
                        Utilities.PopulateProducts(context);
                    }
                    catch
                    {
                        throw;
                    }
                }

                base.ConfigureWebHost(builder);
            });
        }
        public static void DockerComposeUp(string repository)
        {
            var config = LoadConfiguration();

            var dockerComposeFileName = config[$"DockerComposeFile"];
            var dockerComposePath = GetDockerComposeLocation(dockerComposeFileName);

            var compositeService = new Builder()
                .UseContainer()
                .UseCompose()
                .RemoveAllImages()
                .FromFile(dockerComposePath)
                .ServiceName($"itg-test-{Guid.NewGuid()}")
                .RemoveOrphans()
                .Build()
                .Start();

            _services.Add(repository, compositeService);

            Assert.IsTrue(compositeService.State.Equals(ServiceRunningState.Running));
            Assert.IsTrue(compositeService.Containers.First().State.Equals(ServiceRunningState.Running));
            ;
        }

        public static void DockerComposeDown(string repository)
        {
            var compositeService = _services.FirstOrDefault(s => s.Key == repository);
            compositeService.Value.Stop();
            compositeService.Value.Remove(true);
            compositeService.Value.Dispose();
            Assert.IsTrue(compositeService.Value.State.Equals(ServiceRunningState.Removed));

        }

        private static IConfiguration LoadConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }
        private static string GetDockerComposeLocation(string dockerComposeFileName)
        {
            var directory = Directory.GetCurrentDirectory();
            while (!Directory.EnumerateFiles(directory, "*.yml").Any(s => s.EndsWith(dockerComposeFileName)))
            {
                directory = directory.Substring(0, directory.LastIndexOf(Path.DirectorySeparatorChar));
            }
            return Path.Combine(directory, dockerComposeFileName);
        }

    }
}

