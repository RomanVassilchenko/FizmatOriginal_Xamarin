using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace FizmatOriginal.ViewModels
{
    class GetContent
    {
        private HttpClient _client = new HttpClient();
        private string URL = "";
        private string content = "";
        private string key = "";
        public GetContent(string URL)
        {
            this.URL = URL;
        }
        public GetContent(string URL, string key)
        {
            this.URL = URL;
            this.key = key;
        }

        public async Task<string> GetContentAsync()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    content = await _client.GetStringAsync(URL);
                    if (key != "")
                    {
                        Application.Current.Properties[key] = content;
                    }
                }
                catch (Exception ey)
                {
                    Crashes.TrackError(ey);
                }
            }
            else
            {
                if (key != "")
                {
                    GetStringFromKey NewsgetTextFromKey = new GetStringFromKey(key);
                    content = NewsgetTextFromKey.GetText();
                }
            }
            return content;
        }
    }
}
