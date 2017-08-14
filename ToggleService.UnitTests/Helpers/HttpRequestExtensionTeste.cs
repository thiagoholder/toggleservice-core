using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Moq;
using Xunit;

namespace ToggleService.UnitTests.Helpers
{
    public class HttpRequestExtensionTeste
    {
        private Mock moq;
        private Mock<HttpResponseBase> _httpResponse;

        public HttpRequestExtensionTeste()
        {
            _httpResponse = new Mock<HttpResponseBase>();
        }

        [Fact]
        public void TesteHttpResponse()
        {
            _httpResponse.Setup(r => r.AddHeader(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((x, y) => _httpResponse.Object.Headers.Add(x, y));

            
        }
    }
}
