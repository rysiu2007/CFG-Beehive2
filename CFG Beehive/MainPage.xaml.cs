using Android;
using Android.Content.PM;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CFG_Beehive
{
    public partial class MainPage : ContentPage
    {
        const string host = "www.google.com";
        const string uri = "/";
        //Częstotliwość wysyłania danych: h, m, s
        TimeSpan timespan = new TimeSpan(0, 0, 5);
        bool z;
        public MainPage()
        {
            //    string text = "1";
            //  status.Text = text
            InitializeComponent();
             
            HttpResponseMessage http = null;
            Device.StartTimer(timespan, () =>
            {
                SendLocalization().ContinueWith((task) => {
                    http = task.Result;
                   // task.Wait();
                });//.ContinueWith((task) =>
                   //{
                   //    http = task.Result;
                   //    text = http.StatusCode.ToString();
                   //},
                   //TaskScheduler.FromCurrentSynchronizationContext());
                if (z)
                    DisplayAlert("Błąd", "Aplikacja potrzebuje zgody na lokalizację.", "Rozumiem");
                if (http != null)
                status.Text = http.StatusCode.ToString();
                return true;
            });
        }

        //Pobiera lokalizację, tworzy http client i wysyła lokalizację na host/api
        //metoda zwraca HttpResponseMessage
        async Task<HttpResponseMessage> SendLocalization()
        {

            //  var status = Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            z = status != PermissionStatus.Granted;
            Location location = null;
                location = await Geolocation.GetLocationAsync();
            if (location == null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
            
                HttpClient client = new HttpClient();
            client.Timeout = timespan;
            HttpResponseMessage httpResponse;
            StringContent stringContent = new StringContent(
                $"{{" +
                $"\"Longitude\":\"{location.Longitude}\"," +
                $"\"Latitude\":\"{location.Latitude}\"," +
                $"\"DateTime\":\"{location.Timestamp.UtcDateTime}\"" +
                $"}}");
            try
            {
                httpResponse = await client.PostAsync(host + uri, stringContent);
            }
            catch
            {
                httpResponse = new HttpResponseMessage(System.Net.HttpStatusCode.ExpectationFailed);
            }
            return httpResponse;

        }
    }
}
