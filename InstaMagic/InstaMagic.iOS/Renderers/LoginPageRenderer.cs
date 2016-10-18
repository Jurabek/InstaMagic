using InstaMagic;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LoginPage), typeof(InstaMagic.iOS.Renderers.LoginPageRenderer))]

namespace InstaMagic.iOS.Renderers
{
    public class LoginPageRenderer : PageRenderer
    {
        public bool IsShown { get; private set; }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            if (Helpers.Settings.TokenSettings != string.Empty)
            {
                App.Current.CompleteLogin(Helpers.Settings.TokenSettings);
            }
            else
            {
                // Fixed the issue that on iOS 8, the modal wouldn't be popped.2
                // url : http://stackoverflow.com/questions/24105390/how-to-login-to-facebook-in-xamarin-forms
                if (!IsShown)
                {

                    IsShown = true;
                    var auth = new OAuth2Authenticator(
                        clientId: App.XamarinAuthSettings.ClientId, // your OAuth2 client id
                        scope: App.XamarinAuthSettings.Scope, // The scopes for the particular API you're accessing. The format for this will vary by API.
                        authorizeUrl: new Uri(App.XamarinAuthSettings.AuthorizeUrl), // the auth URL for the service
                        redirectUrl: new Uri(App.XamarinAuthSettings.RedirectUrl)); // the redirect URL for the service

                    auth.Completed += (sender, eventArgs) => {
                        if (eventArgs.IsAuthenticated)
                        {
                            // Use eventArgs.Account to do wonderful things
                            var token = eventArgs.Account.Properties["access_token"];
                            Helpers.Settings.TokenSettings = token;
                            App.Current.CompleteLogin(token);
                        }
                        else
                        {
                            // The user cancelled
                        }
                    };
                    auth.Error += (s, e) =>
                    {

                    };
                    PresentViewController(auth.GetUI(), true, null);
                }
            }
            
        }
    }
}
