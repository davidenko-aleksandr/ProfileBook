﻿using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Plugin.Popups;
using ProfileBook.Models;
using ProfileBook.Services;
using ProfileBook.Services.AuthenticationServices;
using ProfileBook.Services.AuthorizationServices;
using ProfileBook.Services.EnumServices;
using ProfileBook.Services.RepositoryService;
using ProfileBook.Themes;
using ProfileBook.ViewModels;
using ProfileBook.Views;
using Xamarin.Forms;

namespace ProfileBook
{
    public partial class App : PrismApplication
    {
        public static bool IsCurrentAppTheme { get; set; }
        public static int IdLogTest { get; set; }

        private static ISettings AppSettings => CrossSettings.Current;

        public static bool IsDarkOrLightTheme
        {
            get => AppSettings.GetValueOrDefault(nameof(IsDarkOrLightTheme), IsCurrentAppTheme);
            set => AppSettings.AddOrUpdateValue(nameof(IsDarkOrLightTheme), value);
        }

        public static int UserLogin
        {
            get => AppSettings.GetValueOrDefault(nameof(UserLogin), IdLogTest);
            set => AppSettings.AddOrUpdateValue(nameof(UserLogin), value);
        }

        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterPopupNavigationService();
            containerRegistry.RegisterForNavigation<NavigationPage>();

            //registration of pages and view models
            containerRegistry.RegisterForNavigation<SignInPageView, SignInPageViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPageView, SignUpPageViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfileView, AddEditProfileViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPageView, SettingsPageViewModel>();
            containerRegistry.RegisterForNavigation<ModalProfilePageView, ModalProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<MainListPageView, MainListPageViewModel>();

            //registration of services with interfaces
            containerRegistry.RegisterInstance<IRepository<User>>(Container.Resolve<Repository<User>>());
            containerRegistry.RegisterInstance<IRepository<Profile>>(Container.Resolve<Repository<Profile>>());
            containerRegistry.RegisterInstance<ICheckPasswordValid>(Container.Resolve<CheckPasswordValid>());
            containerRegistry.RegisterInstance<ICheckLoginValid>(Container.Resolve<CheckLoginValid>());
            containerRegistry.RegisterInstance<IUserAuthentication>(Container.Resolve<UserAuthentication>());
            containerRegistry.RegisterInstance<IAuthorizationService>(Container.Resolve<AuthorizationService>());
            containerRegistry.RegisterInstance<IProfileSort>(Container.Resolve<ProfileSort>());
        }

        protected override void OnInitialized()
        {            
            Device.SetFlags(new string[] { "RadioButton_Experimental" });

            InitializeComponent();

            if (IsDarkOrLightTheme)
            {
                ChangeTheme.TurnOnTheDark();
            }

            if (UserLogin <= 0)
            {
                NavigationService.NavigateAsync("NavigationPage/SignInPageView");
            }
            else
            {
                NavigationService.NavigateAsync("NavigationPage/MainListPageView");
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
