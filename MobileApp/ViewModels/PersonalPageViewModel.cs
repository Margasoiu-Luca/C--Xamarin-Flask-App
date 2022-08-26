using MobileApp.Models;
using MobileApp.Navigation;
using MobileApp.Services;
using MobileApp.Views;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    internal class PersonalPageViewModel : ObservableObject
    {
        private ObservableCollection<Food> foods;
        private Food _selectedFood;
        private  ApiFoodService _foodService;
        private int _totalCalories;


        public ICommand DeleteFoodComamnd { get; }
        public ICommand GoBackToSearchCommand { get; }
        public ObservableCollection<Food> Foods
        {
            get => foods;
            set
            {
                foods = value;
                OnPropertyChanged(nameof(Foods));
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

        public int TotalCalories
        {
            get { return _totalCalories; }
            set
            {
                if (_totalCalories != value)
                {
                    _totalCalories = value;
                    OnPropertyChanged(nameof(TotalCalories));

                }
            }
        }


        public Food SelectedFood
        {
            get => _selectedFood;
            set
            {
                if (_selectedFood != value)
                {
                    _selectedFood = value;

                    OnFoodSelected(SelectedFood);
                }
            }
        }


        public PersonalPageViewModel(ApiFoodService fS)
        {


            Foods = new ObservableCollection<Food>();
            FoodService = fS;

            GoBackToSearchCommand = new Command(GoBackToSearch);
            DeleteFoodComamnd = new Command<Food>(async f => await DeleteFood(f));

        }

        public async Task PopulateFoods()
        {
            try
            {
                TotalCalories= 0;
                Foods.Clear();
                var retrievedFoods = await FoodService.GetFoods();
                foreach (var food in retrievedFoods)
                {
                    TotalCalories += food.Calories;
                    Foods.Add(food);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task DeleteFood(Food f)
        {
            var succeded = await FoodService.DeleteFood(f);
            if (succeded)
            {
                await PopulateFoods();
            }
        }

        async void OnFoodSelected(Food f)
        {
            if (f == null)
                return;

            var pageToNavigate = new FoodDetails(f);
            await NavigationDispatcher.Instance.Navigation.PushAsync(pageToNavigate);
        }

        public void GoBackToSearch()
        {
            NavigationDispatcher.Instance.Navigation.PopAsync();

        }
    }
}
