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
    public class RelationshipViewModel
    {
        public List<User> Follows { get; set; }
    }
    public partial class RelationshipsPage : ContentPage
    {
        public RelationshipViewModel ViewModel { get; set; }
        public UserProfileViewModel UserViewModel { get; private set; }

        public RelationshipsPage(UserProfileViewModel userViewModel)
        {
            InitializeComponent();
            UserViewModel = userViewModel;
            Init();
        }

        private async void Init()
        {
            var relationships = new Relationships(App.InstagramConfig, App.Auth);
            ViewModel = new RelationshipViewModel();
            var userResponce = await relationships.FollowedBy();
            ViewModel.Follows = userResponce.Data;
            
        }
    }
}
