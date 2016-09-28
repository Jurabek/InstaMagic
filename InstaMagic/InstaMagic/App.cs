using InstaSharp;
using InstaSharp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using static InstaSharp.OAuth;

namespace InstaMagic
{
    public class OAuthSettings
    {
        public OAuthSettings(
            string clientId,
            string scope,
            string authorizeUrl,
            string redirectUrl)
        {
            ClientId = clientId;
            Scope = scope;
            AuthorizeUrl = authorizeUrl;
            RedirectUrl = redirectUrl;
        }

        public string ClientId { get; private set; }
        public string Scope { get; private set; }
        public string AuthorizeUrl { get; private set; }
        public string RedirectUrl { get; private set; }
    }

    public class App : Application
    {
        public static InstagramConfig InstagramConfig { get; private set; }

        public static OAuthSettings XamarinAuthSettings { get; private set; }


        public static INavigation Navigation { get; private set; }

        public static string ClientSecret = "8ad7c10ece634fb1b0051d2919e6f7bc";

        public static App Current { get { return Application.Current as App; } }

        /// <summary>
        /// Build scope string for auth uri.
        /// </summary>
        /// <param name="scopes">List of scopes.</param>
        /// <returns>Comma separated scopes.</returns>
        private static string BuildScopeForUri(List<Scope> scopes)
        {
            var scope = new StringBuilder();

            foreach (var s in scopes)
            {
                if (scope.Length > 0)
                {
                    scope.Append("+");
                }
                scope.Append(s);
            }

            return scope.ToString();
        }
        public App()
        {
            var scopes = new List<Scope>
            {
                Scope.Basic
            };

            XamarinAuthSettings =
                new OAuthSettings(
                    clientId: "9713da66074c430aa16b679782d5304d",
                    scope: "basic likes comments relationships ",
                    authorizeUrl: "https://api.instagram.com/oauth/authorize/",
                    redirectUrl: "http://192.168.31.1/InstaApiCallback/Instagram/Callback");


            var naviagationPage = new NavigationPage(new UserProfilePage());


            // The root page of your application
            MainPage = naviagationPage;

            Navigation = naviagationPage.Navigation;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static bool IsAuthenticated
        {
            get { return (!String.IsNullOrWhiteSpace(Token)); }
        }

        internal static string Token;

        internal static OAuthResponse Auth;

        public void CompleteLogin(string token)
        {
            Token = token;

            Auth = new OAuthResponse()
            {
                AccessToken = Token,
                User = new InstaSharp.Models.UserInfo { Id = 1415228826 }
            };
            InstagramConfig = new InstagramConfig(XamarinAuthSettings.ClientId, ClientSecret, XamarinAuthSettings.RedirectUrl);
            Navigation.PopModalAsync();
            OnLoginCompleted();
        }

        public event EventHandler<EventArgs> LoginCompleted;

        public void OnLoginCompleted()
        {
            LoginCompleted?.Invoke(this, EventArgs.Empty);
        }
    }
}
