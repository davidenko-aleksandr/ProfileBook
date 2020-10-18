using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Plugin.Popups;
using ProfileBook.Services;
using ProfileBook.Services.AuthenticationServices;
using ProfileBook.Services.AuthorizationServices;
using ProfileBook.SQlite;
using ProfileBook.ViewModels;
using ProfileBook.Views;
using System;
using System.IO;
using Xamarin.Forms;

namespace ProfileBook
{

    public partial class App : PrismApplication
    {
        
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
            //registration of pages and view models
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterPopupNavigationService();

            containerRegistry.RegisterForNavigation<SignInPageView, SignInPageViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPageView, SignUpPageViewModel>();
            containerRegistry.RegisterForNavigation<MainListView, MainListViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfileView, AddEditProfileViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPageView, SettingsPageViewModel>();
            containerRegistry.RegisterForNavigation<ModalProfilePageView, ModalProfilePageViewModel>();

            containerRegistry.RegisterPopupNavigationService();


            //registration of services with interfaces
            containerRegistry.RegisterInstance<ICheckPasswordValid>(Container.Resolve<CheckPasswordValid>());
            containerRegistry.RegisterInstance<ICheckLoginValid>(Container.Resolve<CheckLoginValid>());
            containerRegistry.RegisterInstance<IUserAuthentication>(Container.Resolve<UserAuthentication>());
            containerRegistry.RegisterInstance<IAuthorizationService>(Container.Resolve<AuthorizationService>());

        }
        private static ISettings AppSettings => CrossSettings.Current;
        public static int IdLogTest { get; set; }
        public static int UserLogin // Saving data to understand whether the user is authorized or not
        {
            get => AppSettings.GetValueOrDefault(nameof(UserLogin), IdLogTest);
            set => AppSettings.AddOrUpdateValue(nameof(UserLogin), value);
        }
        protected override void OnInitialized()
        {
            InitializeComponent();
            
            if (UserLogin <= 0)
            {
                NavigationService.NavigateAsync(new System.Uri("http://www.ProfileBook/NavigationPage/SignInPageView", System.UriKind.Absolute));
            }
            else
            {
                NavigationService.NavigateAsync(new System.Uri("http://www.ProfileBook/NavigationPage/MainListView", System.UriKind.Absolute));
            }
            
        }

        /// <summary>
        /// Making a static property for working with the database
        /// </summary>
        public const string DATABASE_USER = "user.db";
        public const string DATABASE_PROFILE = "profile.db";
        public static ProfileRepository databaseProfile;
        public static UserRepository databaseUser;

        public static UserRepository DatabaseUser
        {
            get
            {
                if (databaseUser == null)
                {
                    databaseUser = new UserRepository(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_USER));
                }
                return databaseUser;
            }
        }
        public static ProfileRepository DatabaseProfile
        {
            get
            {
                if (databaseProfile == null)
                {
                    databaseProfile = new ProfileRepository(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_PROFILE));
                }
                return databaseProfile;
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
