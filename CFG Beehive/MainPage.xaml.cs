/*TODO:
 Strony podrzędne Management i Hive Page.*/
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
namespace CFG_Beehive
{
    public partial class MainPage : ContentPage
    {
        String s = "";
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
            NavigationPage.SetHasNavigationBar(this, false);
            Navigation.PushAsync(new LoginPage());
        }
    }
}
