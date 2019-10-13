using Xamarin.Forms;

namespace FizmatOriginal.ViewModels
{
    class GetUrl
    {
        private readonly string NewsUrlAstana = "https://script.google.com/macros/s/AKfycbzkpgPRlnZ18dMC8WlxSeSrlwNIwAo0nwAEr29XYbJHvbQFNMY/exec";
        private readonly string NewsUrlAlmaty = "https://script.google.com/macros/s/AKfycbwo-6hqRECSavZbMGF7IhZo2OaHxqWwXGNJ53mWTPUVOophRuM/exec";

        private readonly string ScheduleUrlAstana = "https://script.google.com/macros/s/AKfycbw_Cf4YMALGRKny552qe9u9f86Ui7Iq7vbGDBVGoFKh2IhYH0w/exec";
        private readonly string ScheduleUrlAlmaty = "https://script.google.com/macros/s/AKfycbzesPBio7ZS5Xvq1h5nj944Y_yalE3gLcJw5ihF21XYyPg3YA1X/exec";

        private readonly string classUrlAstana = "https://script.google.com/macros/s/AKfycbz7ofb88NYRa-hcsyJNkOof_r5vO3qpwBSPdgeLIqXtAAK41Dw/exec";
        private readonly string classUrlAlmaty = "https://script.google.com/macros/s/AKfycbxq7DKZxJAdGo4mESTsLkRLiIH95Q_ydNvaicYlCvBst0br3Lwj/exec";

        public string GetScheduleUrl()
        {
            if (Application.Current.Properties.ContainsKey("switch_town_key"))
            {
                bool.TryParse(Application.Current.Properties["switch_town_key"].ToString(), out bool result);
                if (result)
                {
                    return ScheduleUrlAlmaty;
                }
            }
            return ScheduleUrlAstana;
        }

        public string GetClassUrl()
        {
            if (Application.Current.Properties.ContainsKey("switch_town_key"))
            {
                bool.TryParse(Application.Current.Properties["switch_town_key"].ToString(), out bool result);
                if (result)
                {
                    return classUrlAlmaty;
                }
            }
            return classUrlAstana;
        }

        public string GetNewsUrl()
        {
            if (Application.Current.Properties.ContainsKey("switch_town_key"))
            {
                bool.TryParse(Application.Current.Properties["switch_town_key"].ToString(), out bool result);
                if (result)
                {
                    return NewsUrlAlmaty;
                }
            }
            return NewsUrlAstana;
        }

    }
}
