using Microsoft.AppCenter.Crashes;
using Plugin.Connectivity;
using System;
using System.Net.Http;

namespace FizmatOriginal.Models
{
    class GetContent
    {
        private string URL = "";
        private readonly HttpClient _client = new HttpClient();
        private string content = "";
        public GetContent(string URL)
        {
            this.URL = URL;
        }

        public async System.Threading.Tasks.Task<string> getContentAsync()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    content = await _client.GetStringAsync(URL);
                }
                catch (Exception ey)
                {
                    Crashes.TrackError(ey);
                }
            }
            return content;
        }
    }
}
