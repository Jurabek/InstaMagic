using InstaSharp;
using InstaSharp.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InstaMagic.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            string username = "jurabek.a";
            string password = "bek199328";

            const string clientId = "9713da66074c430aa16b679782d5304d";
            const string clientSecret = "8ad7c10ece634fb1b0051d2919e6f7bc";
            const string redirectUri = "http://localhost/InstaApiCallback/Instagram/Callback";

            var config = new InstagramConfig(clientId, clientSecret, redirectUri);
            var scopes = new List<OAuth.Scope>()
            {
                OAuth.Scope.Basic
            };

            var auth = Instagram.AuthByCredentials(username, password, config, scopes);

            var users = new Users(config, auth);
            var userFeed = await users.Feed(null, null, null);
        }
    }
}
