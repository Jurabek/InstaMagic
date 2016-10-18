using InstaSharp.Endpoints;
using InstaSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace InstaMagic
{
    public class UserProfileViewModel
    {
        public User UserInfo { get; set; }

        public ICommand OpenFollowersPage { get; set; }

        public ICommand LogOut { get; set; }
        public UserProfileViewModel()
        {
            OpenFollowersPage = new Command(() => 
            {
                App.Navigation.PushAsync(new RelationshipsPage(this));
            });

            LogOut = new Command(() =>
            {
                Helpers.Settings.TokenSettings = string.Empty;
            });
        } 
    }

    public partial class UserProfilePage : ContentPage
    {
        public UserProfileViewModel ViewModel { get; set; }

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

            ViewModel = new UserProfileViewModel
            {
                UserInfo = userInfo.Data
            };
            ToolbarItems.Add(new ToolbarItem { Text = "Followers", Command = ViewModel.OpenFollowersPage });
            ToolbarItems.Add(new ToolbarItem { Text = "LogOut", Command = ViewModel.LogOut });

            BindingContext = ViewModel;
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
