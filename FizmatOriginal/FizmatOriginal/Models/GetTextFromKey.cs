using Xamarin.Forms;

namespace FizmatOriginal.Models
{
    class GetTextFromKey
    {

        private string key;
        private string text;
        public GetTextFromKey(string key)
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
