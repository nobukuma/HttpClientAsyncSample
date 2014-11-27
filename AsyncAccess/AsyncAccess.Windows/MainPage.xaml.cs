using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace AsyncAccess
{
    /// <summary>
    /// Frame 内へナビゲートするために利用する空欄ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // TODO: ConsumerKey, ConsumerSecretを設定する
        private static readonly string ConsumerKey = "";
        private static readonly string ConsumerSecret = "";

        private static readonly string OAuth2TokenUrl = "https://api.twitter.com/oauth2/token";
        private static readonly string SearchTweetsUri = "https://api.twitter.com/1.1/search/tweets.json";
        private static readonly string AuthHeader = "Authorization";

        // http://ufcpp.wordpress.com/2012/04/26/%e9%9d%9e%e5%90%8c%e6%9c%9f%e5%87%a6%e7%90%86%e3%81%a8%e3%83%87%e3%82%a3%e3%82%b9%e3%83%91%e3%83%83%e3%83%81%e3%83%a3%e3%83%bc/
        private System.Threading.SynchronizationContext synchronizationContext;
        private OAuth2AccessToken accessToken;

        public MainPage()
        {
            this.InitializeComponent();
            this.synchronizationContext = System.Threading.SynchronizationContext.Current;
            this.accessToken = null;
        }

        // http://msdn.microsoft.com/en-us/library/system.net.httpwebrequest.begingetrequeststream(v=vs.110).aspx

        private void OAuthGetTokenButton_Click(object sender, RoutedEventArgs e)
        {
            OAuthGetToken_SendRequest();
        }

        private void SearchTimeLineButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTimeLine_SendRequest();
        }

        #region oauth2/tokenの処理
        private void OAuthGetToken_SendRequest()
        {
            var webReq = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(OAuth2TokenUrl);

            webReq.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            webReq.Method = "POST";

            string credentials = OAuthGetToken_CreateTokenCredentials(ConsumerKey, ConsumerSecret);
            webReq.Headers[AuthHeader] = String.Format("Basic {0}", credentials);

            var r = webReq.BeginGetRequestStream(new AsyncCallback(OAuthGetToken_GetRequestStreamCallback), webReq);
        }

        private void OAuthGetToken_GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            var webReq = (System.Net.HttpWebRequest)asynchronousResult.AsyncState;
            var stream = webReq.EndGetRequestStream(asynchronousResult);

            using (System.IO.StreamWriter streamWriter = new StreamWriter(stream))
            {
                streamWriter.Write("grant_type=client_credentials");
            }

            var r = (IAsyncResult)webReq.BeginGetResponse(new AsyncCallback(OAuthGetToken_GetResponseCallback), webReq);
        }

        private string OAuthGetToken_CreateTokenCredentials(string consumerKey, string consumerSecret)
        {
            string encodedConsumerKey = Uri.EscapeDataString(consumerKey);
            string encodedConsumerSecret = Uri.EscapeDataString(consumerSecret);
            string tokenCredentials = String.Format("{0}:{1}", encodedConsumerKey, encodedConsumerSecret);

            string encodingName = "UTF-8";
            string tokenCredentialsBase64 = ToBase64String(tokenCredentials, encodingName);
            return tokenCredentialsBase64;
        }

        private void OAuthGetToken_GetResponseCallback(IAsyncResult asynchronousResult)
        {
            try
            {
                var webReq = (System.Net.HttpWebRequest)asynchronousResult.AsyncState;
                var webRes = (System.Net.HttpWebResponse)webReq.EndGetResponse(asynchronousResult);

                var stream = webRes.GetResponseStream();
                var result = new StreamReader(stream).ReadToEnd();

                // {"token_type":"bearer","access_token":"AAAAAAAAAAAAAAAAAAAAAPPMVwAAAAAA77uFK8FCnOnw5HagSCNxY93HjXo%3D0Ty2nkgdSIE0JSz0gTfaA3P6yaAS7V81fUm9JmdD9LYTzrCgc4"}
                this.accessToken = JsonConvert.DeserializeObject<OAuth2AccessToken>(result);

                this.WriteLog("TokenType={0}, AccessToken={1}", accessToken.token_type, accessToken.access_token);
            }
            catch (Exception ex)
            {
                this.WriteLog("{0}", ex.Message);
            }

        }
        #endregion

        #region search/tweetsの処理
        private void SearchTimeLine_SendRequest()
        {
            var query = "#madobenyok";
            var requestUri = String.Format("{0}?q={1}", SearchTweetsUri, Uri.EscapeDataString(query));

            var webReq = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(requestUri);

            webReq.Method = "GET";

            string credentials = String.Format("Bearer {0}", this.accessToken.access_token);
            webReq.Headers[AuthHeader] = credentials;

            var r = (IAsyncResult)webReq.BeginGetResponse(new AsyncCallback(SearchTimeLine_GetResponseCallback), webReq);
        }

        private void SearchTimeLine_GetResponseCallback(IAsyncResult asynchronousResult)
        {
            try
            {
                var webReq = (System.Net.HttpWebRequest)asynchronousResult.AsyncState;
                var webRes = (System.Net.HttpWebResponse)webReq.EndGetResponse(asynchronousResult);

                var stream = webRes.GetResponseStream();
                var result = new StreamReader(stream).ReadToEnd();

                var searchResult = JsonConvert.DeserializeObject<SearchTweetsResponse>(result);
                this.WriteLog("----------------");
                foreach (var tweet in searchResult.Statuses)
                {
                    this.WriteLog("{0} ({1}):{2}", tweet.CreatedAt, tweet.User.ScreenName, tweet.Text);
                }
                this.WriteLog("----------------");
            }
            catch (Exception ex)
            {
                this.WriteLog("{0}", ex.Message);
            }

        }
        #endregion

        private string ToBase64String(string str, string encodingName)
        {
            var encoding = Encoding.GetEncoding(encodingName);
            byte[] strBytes = encoding.GetBytes(str);
            string strBase64 = Convert.ToBase64String(strBytes);
            return strBase64;
        }

        private void WriteLog(string format, params object[] args)
        {
            this.synchronizationContext.Post(state =>
            {
                this.LogText.Text += String.Format(format, args);
                this.LogText.Text += Environment.NewLine;
            }, null);
        }
    }
}
