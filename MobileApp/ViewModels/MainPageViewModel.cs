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
    public class MainPageViewModel : ObservableObject
    {

        private ApiFoodService  _foodService;
        private Food _selectedFood;
        private string _auth;
        private string _item;
        private ObservableCollection<Food> foods;

        public ObservableCollection<Food> Foods
        {
            get => foods;
            set
            {
                foods = value;
                OnPropertyChanged(nameof(Foods));
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

        public string Item
        {
            get { return _item; }
            set
            {
                if (_item != value)
                {
                    _item = value;
                    OnPropertyChanged(nameof(Item));

                }
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

        public string Auth
        {
            get { return _auth; }
            set
            {
                if (_auth != value)
                {
                    _auth = value;
                    OnPropertyChanged(nameof(Auth));

                }
            }
        }

        public ICommand GetFoodItemsCommand { get; set; }
        public ICommand NavigateToPersonalPageCommand { get; set; }
        public MainPageViewModel(string auth)
        {
            Foods = new ObservableCollection<Food>();
            FoodService = new ApiFoodService(auth);

            NavigateToPersonalPageCommand = new Command(NavigateToPersonalPage);
            GetFoodItemsCommand = new Command(async () => await GetFoodItems());
            Auth = auth;
        }

        private  void NavigateToPersonalPage()
        {
            var personalPage = new PersonalPage(FoodService);
            NavigationDispatcher.Instance.Navigation.PushAsync(personalPage);
        }

        private async Task GetFoodItems()
        {
            var response = await FoodService.Search(Item);
            Foods.Clear();
            foreach (var food in response)
            {
                Foods.Add(food);
            }
        }
        async void OnFoodSelected(Food f)
        {
            if (f == null)
                return;

            var pageToNavigate = new FoodDetails(f, FoodService);
            await NavigationDispatcher.Instance.Navigation.PushAsync(pageToNavigate);
        }
    }
}
