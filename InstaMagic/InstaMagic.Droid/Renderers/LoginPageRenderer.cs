using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using InstaMagic;
using Xamarin.Auth;

[assembly: ExportRenderer(typeof(LoginPage), typeof(InstaMagic.Droid.Renderers.LoginPageRenderer))]

namespace InstaMagic.Droid.Renderers
{
    public class LoginPageRenderer : PageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            var activity = this.Context as Activity;

            var auth = new OAuth2Authenticator(
                clientId: App.XamarinAuthSettings.ClientId, // your OAuth2 client id
                scope: App.XamarinAuthSettings.Scope, // The scopes for the particular API you're accessing. The format for this will vary by API.
                authorizeUrl: new Uri(App.XamarinAuthSettings.AuthorizeUrl), // the auth URL for the service
                redirectUrl: new Uri(App.XamarinAuthSettings.RedirectUrl)); // the redirect URL for the service

            auth.Completed += async (sender, eventArgs) => {
                if (eventArgs.IsAuthenticated)
                {

                    var a = eventArgs.Account.Properties["access_token"];
                }
                else
                {
                    // The user cancelled
                }
            };

            activity.StartActivity(auth.GetUI(activity));
        }
    }
}