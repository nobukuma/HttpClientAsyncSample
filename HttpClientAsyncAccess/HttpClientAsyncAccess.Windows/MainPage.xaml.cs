using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http.Filters;
using Windows.Web.Http;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace HttpClientAsyncAccess
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
        //private static readonly string AuthHeader = "Authorization";

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
        private async void OAuthGetToken_SendRequest()
        {
            var filter = new OAuthGetTokenAuthFilter(ConsumerKey, ConsumerSecret, new HttpBaseProtocolFilter());
            var httpClient = new HttpClient(filter);

            var body = "grant_type=client_credentials";
            var encoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;
            var contentType = "application/x-www-form-urlencoded";
            var content = new HttpStringContent(body, encoding, contentType);

            var response = await httpClient.PostAsync(new Uri(OAuth2TokenUrl), content);
            var result = await response.Content.ReadAsStringAsync();

            this.accessToken = JsonConvert.DeserializeObject<OAuth2AccessToken>(result);

            this.WriteLog("TokenType={0}, AccessToken={1}", accessToken.token_type, accessToken.access_token);
        }
        #endregion

        #region search/tweetsの処理
        private async void SearchTimeLine_SendRequest()
        {
            var filter = new OAuth2AuthFilter(this.accessToken, new HttpBaseProtocolFilter());
            var httpClient = new HttpClient(filter);

            var query = "#madobenyok";
            var requestUri = String.Format("{0}?q={1}", SearchTweetsUri, Uri.EscapeDataString(query));

            var result = await httpClient.GetStringAsync(new Uri(requestUri));

            var searchResult = JsonConvert.DeserializeObject<SearchTweetsResponse>(result);
            this.WriteLog("----------------");
            foreach (var tweet in searchResult.Statuses)
            {
                this.WriteLog("{0} ({1}):{2}", tweet.CreatedAt, tweet.User.ScreenName, tweet.Text);
            }
            this.WriteLog("----------------");

        }
        #endregion

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
