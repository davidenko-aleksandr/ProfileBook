﻿using Prism;
using Prism.Ioc;
using Prism.Unity;
using ProfileBook.Services;
using ProfileBook.SQlite;
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

            containerRegistry.Register<ICheckPasswordValid, CheckPasswordValid>();
            containerRegistry.Register<ICheckLoginValid, CheckLoginValid>();
        }

        public const string DATABASE_NAME = "user.db";
        public static UserRepository database;
        public static UserRepository Database
        {
            get
            {
                if (database == null)
                {
                    database = new UserRepository(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return database;
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
