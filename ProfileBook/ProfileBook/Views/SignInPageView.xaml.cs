using Prism.Ioc;
using ProfileBook.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace ProfileBook.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPageView : ContentPage
    {
        [Obsolete]
        public SignInPageView()
        {
            InitializeComponent();
        }        
    }
}