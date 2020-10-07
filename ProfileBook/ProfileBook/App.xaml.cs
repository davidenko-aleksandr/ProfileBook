using Prism;
using Prism.Ioc;
using Prism.Unity;
using ProfileBook.SQLRepository;
using ProfileBook.ViewModels;
using ProfileBook.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProfileBook
{
    //Сhange the parent class to PrismApplication to apply "Prism"
    public partial class App : PrismApplication
    {
        //add the Prism initializer parameter to the constructor 
        //and override the OnInitialized and RegisterTypes methods
        public App(IPlatformInitializer initializer = null) : base(initializer) { }
        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.NavigateAsync(new System.Uri("http://www.ProfileBook/SignInPageView", System.UriKind.Absolute));
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<SignInPageView, SignInPageViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPageView, SignUpPageViewModel>();
            containerRegistry.RegisterForNavigation<MainListView, MainListViewModel>();
        }
        public const string DATABASE_NAME = "friends.db";
        public static UserRepository dataBase;
        public static UserRepository DataBase
        {
            get
            {
                if (dataBase == null)
                {
                    dataBase = new UserRepository(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return dataBase;
            }
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
    }
}
