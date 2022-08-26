using MobileApp.Services;
using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonalPage : ContentPage
    {
        public PersonalPage(ApiFoodService fS)
        {
            BindingContext = new PersonalPageViewModel(fS);
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            try
            {
                await (BindingContext as PersonalPageViewModel).PopulateFoods();
            }
            catch (Exception e)
            {
                Debug.Fail(e.Message);
            }
        }
    }
}