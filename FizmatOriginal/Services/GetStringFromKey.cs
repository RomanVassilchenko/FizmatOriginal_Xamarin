using System;
using Xamarin.Forms;

namespace FizmatOriginal.ViewModels
{
    class GetStringFromKey
    {

        private string key;
        private string text;
        public GetStringFromKey(string key)
        {
            this.key = key;
        }

        public string GetText()
        {
            if (!Application.Current.Properties.ContainsKey(key))
            {
                text = "";
            }
            else
            {
                try
                {
                    text = Application.Current.Properties[key].ToString();
                }
                catch
                {
                    text = "";
                }
            }
            return text;
        }

    }
}
