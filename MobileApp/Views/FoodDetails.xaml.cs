using MobileApp.Models;
using MobileApp.Services;
using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodDetails : ContentPage
    {
        public FoodDetails(Food f)
        {
            BindingContext = new FoodDetailsViewModel(f);
            InitializeComponent();
        }
        public FoodDetails(Food f, ApiFoodService fS)
        {
            BindingContext = new FoodDetailsViewModel(f, fS);
            InitializeComponent();
        }
    }
}