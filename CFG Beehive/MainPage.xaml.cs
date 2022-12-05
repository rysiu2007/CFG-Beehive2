/*TODO:
 Strony podrzędne Management i Hive Page.*/

using System;
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
            Logout();
        }
        void Logout()
        {
            Navigation.PushAsync(new LoginPage());
        }

        private void Menu_Clicked(object sender, EventArgs e)
        {
            Logout();
        }
    }
}
