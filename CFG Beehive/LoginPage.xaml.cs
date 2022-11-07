using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;

namespace CFG_Beehive
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        string login, password,host = "", api = "/api/auth/login";
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(host+api);
            login = Login.Text;
            password = Password.Text;
            StringContent httpContent = new StringContent($"{{ \"username\": \"{login}\", \"password\": \"{password}\"}}");
            HttpResponseMessage httpResponse = await client.PostAsync(host+api,httpContent);
            if(httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Navigation.PopAsync(true);
            }
        }
    }
}