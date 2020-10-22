using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.Services.BindingServices
{
    public class BetterCheckBox : CheckBox
    {
        public static BindableProperty IsCheckedCommandProperty = BindableProperty.Create(nameof(IsCheckedCommand),
                                                                 typeof(ICommand), typeof(BetterCheckBox), null);

        public ICommand IsCheckedCommand
        {
            get
            {
                return (ICommand)this.GetValue(IsCheckedCommandProperty);
            }
            set
            {
                this.SetValue(IsCheckedCommandProperty, value);
            }
        }
        public BetterCheckBox()
        {
            this.CheckedChanged += OnCheckBoxCheckedChanged;
        }
        private void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
        }

    }
}
