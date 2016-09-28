using InstaSharp.Endpoints;
using InstaSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace InstaMagic
{
    public class UserProfileViewModel
    {
        public User UserInfo { get; set; }
    }

    public partial class UserProfilePage : ContentPage
    {
        public UserProfilePage()
        {
            InitializeComponent();
            DidAppear();
            App.Current.LoginCompleted += Current_LoginCompleted;
        }

        private async void Current_LoginCompleted(object sender, EventArgs e)
        {
            var users = new Users(App.InstagramConfig, App.Auth);
            
            var userInfo = await users.GetSelf();

            App.Auth.User = userInfo.Data as UserInfo;
            var recentMedia = await users.RecentSelf();

            BindingContext = new UserProfileViewModel
            {
                UserInfo = userInfo.Data
            };
        }

        public bool IsAuthenticated { get { return App.IsAuthenticated; } }

        public virtual void DidAppear()
        {
            if (!IsAuthenticated)
            {
                var loginPage = new LoginPage();
                Navigation.PushModalAsync(loginPage);
            }
        }
    }
}
