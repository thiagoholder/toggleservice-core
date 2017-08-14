using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace ToggleService.API.Helpers
{

    /// <summary>
    /// Extends the HttpRequestMessage collection
    /// </summary>
    public static class HttpRequestMessageExtensions
    {
        /// <summary>
        /// Returns an individual HTTP Header value
        /// </summary>
        /// <param name="request"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetHeader(this HttpRequestMessage request, string key)
        {
            return !request.Headers.TryGetValues(key, out IEnumerable<string> keys) ? null : keys.First();
        }
        
    }
}
