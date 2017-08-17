using Microsoft.Extensions.Configuration;
using System.IO;
using Xunit;

namespace ToggleService.IntegrationTest
{

    public class UniTTest
    {
        public IConfigurationRoot Config { get; set; }

        [Fact]
        public void PegarInformacoes()
        {
            var build = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Config = build.Build();

        }
    }
}
