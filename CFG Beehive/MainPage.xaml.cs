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
        TimeSpan timespan = new TimeSpan(0, 0, 10);
        public MainPage()
        {
            InitializeComponent();

            Device.StartTimer(timespan, () =>
            {
                SendLocalization().ContinueWith((task) =>
                {
                    HttpResponseMessage http = task.Result;
                    Environment.Exit(9);
                    DisplayAlert("Status", "Connection return code: " + http.ReasonPhrase, "OK");
                },
                TaskScheduler.FromCurrentSynchronizationContext());
                return true;
            });
        }

        //Pobiera lokalizację, tworzy http client i wysyła lokalizację na host/api
        //metoda zwraca HttpResponseMessage
        async Task<HttpResponseMessage> SendLocalization()
        {
            Location location = await Geolocation.GetLocationAsync();
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
