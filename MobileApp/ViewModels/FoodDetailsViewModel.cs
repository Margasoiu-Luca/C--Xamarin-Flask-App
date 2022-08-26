using MobileApp.Models;
using MobileApp.Services;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    public class FoodDetailsViewModel : ObservableObject
    {

        private ApiFoodService _foodService;
        private Food _currentFood;
        private string _success;

        public ICommand AddFoodCommand { get; }

        public string Success
        {
            get => _success;
            set
            {
                _success = value;
                OnPropertyChanged(nameof(Success));
            }
        }
        public Food CurrentFood
        {
            get => _currentFood;
            set
            {
                _currentFood = value;
                OnPropertyChanged(nameof(CurrentFood));
            }
        }

        public ApiFoodService FoodService
        {
            get { return _foodService; }
            set
            {
                if (_foodService != value)
                {
                    _foodService = value;
                    OnPropertyChanged(nameof(FoodService));

                }
            }
        }

        public FoodDetailsViewModel(Food f)
        {
            FoodService = null;
            CurrentFood = f;
        }
        public FoodDetailsViewModel(Food f, ApiFoodService fS)
        {
            AddFoodCommand = new Command(async () => await AddFood());
            FoodService = fS;
            CurrentFood = f;
        }

        private async Task AddFood()
        {
            var succeeded = await FoodService.AddFood(CurrentFood);
            Success = succeeded ? "Food added to diet" : "Food already exists in diet";
        }
    }
}
