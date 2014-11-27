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
    public class OAuthGetTokenAuthFilter : IHttpFilter
    {
        private static readonly string AuthHeader = "Authorization";

        private string consumerKey;
        private string consumerSecret;
        private IHttpFilter filter;

        public OAuthGetTokenAuthFilter(
            string consumerKey,
            string consumerSecret,
            IHttpFilter filter)
        {
            this.consumerKey = consumerKey;
            this.consumerSecret = consumerSecret;
            this.filter = filter;
        }

        public IAsyncOperationWithProgress<HttpResponseMessage, HttpProgress>
            SendRequestAsync(HttpRequestMessage request)
        {
            string credentials = CreateTokenCredentials(consumerKey, consumerSecret);
            request.Headers[AuthHeader] = String.Format("Basic {0}", credentials);

            return this.filter.SendRequestAsync(request);
        }

        public void Dispose()
        {
            filter.Dispose();
        }

        private string CreateTokenCredentials(string consumerKey, string consumerSecret)
        {
            string encodedConsumerKey = Uri.EscapeDataString(consumerKey);
            string encodedConsumerSecret = Uri.EscapeDataString(consumerSecret);
            string tokenCredentials = String.Format("{0}:{1}", encodedConsumerKey, encodedConsumerSecret);

            string encodingName = "UTF-8";
            string tokenCredentialsBase64 = ToBase64String(tokenCredentials, encodingName);
            return tokenCredentialsBase64;
        }

        private string ToBase64String(string str, string encodingName)
        {
            var encoding = Encoding.GetEncoding(encodingName);
            byte[] strBytes = encoding.GetBytes(str);
            string strBase64 = Convert.ToBase64String(strBytes);
            return strBase64;
        }
    }
}
