using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Web.Http;
using Windows.Foundation;
using Windows.Web.Http.Filters;

namespace HttpClientAsyncAccess
{
    public class OAuth2AuthFilter : IHttpFilter
    {
        private static readonly string AuthHeader = "Authorization";

        private OAuth2AccessToken accessToken;
        private IHttpFilter filter;

        public OAuth2AuthFilter(
            OAuth2AccessToken accessToken,
            IHttpFilter filter)
        {
            this.accessToken = accessToken;
            this.filter = filter;
        }

        public IAsyncOperationWithProgress<HttpResponseMessage, HttpProgress>
            SendRequestAsync(HttpRequestMessage request)
        {
            request.Headers[AuthHeader] = String.Format("Bearer {0}", accessToken.access_token);

            return this.filter.SendRequestAsync(request);
        }

        public void Dispose()
        {
            filter.Dispose();
        }
    }
}
