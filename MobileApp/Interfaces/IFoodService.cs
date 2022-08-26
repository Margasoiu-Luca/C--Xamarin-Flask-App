using MobileApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Services
{
    internal interface IFoodService
    {
        Task<IEnumerable<Food>> GetFoods();
        Task<ObservableCollection<Food>> Search(string query);
        Task<bool> DeleteFood(Food food);
        Task<bool> AddFood(Food food);
    }
}
