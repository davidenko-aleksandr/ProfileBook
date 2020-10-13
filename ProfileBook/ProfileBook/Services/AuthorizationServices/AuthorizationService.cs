using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;

using ProfileBook.Services.AuthenticationServices;
using ProfileBook.Models;
using System.Security.Cryptography.X509Certificates;

namespace ProfileBook.Services.AuthorizationServices
{
    public class AuthorizationService : IAuthorizationService
    {
       
        private static int logId;
       // public UserAuthentication UserAuthentication { get; set; }
        public int AddUodateUserId(int id)
        {
            logId = id;
            return id;            
        }


        public void ToWriteLoginId()
        {
            App.UserLogin = Login;
        }

        public static int Login
        { 
            get { return logId; }
            set
            {
                if (value != logId)
                    _ = logId;
            }
        }

    }
}
