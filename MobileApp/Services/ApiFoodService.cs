using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MobileApp.Models;
namespace MobileApp.Services
{
    public class ApiFoodService : IFoodService
    {


        //http (T)hird (P)arty client
        //Used to access the Spoonacular api
        private readonly HttpClient _httpTPClient;
        //http (L)ocal cliet
        //used to access the local flask-python api
        private readonly HttpClient _httpLClient;

        //Both of these are in the same Service file because they both deal with the same data type: Food
        public ApiFoodService(string auth)
        {
            HttpClientHandler insecureHandler = GetInsecureHandler();

            _httpTPClient = new HttpClient(insecureHandler);
            _httpTPClient.BaseAddress = new Uri("https://api.spoonacular.com/");
            _httpTPClient.DefaultRequestHeaders.Add("Accept", "application/json");


            _httpLClient = new HttpClient(insecureHandler);
            //_httpLClient.BaseAddress = new Uri("https://shrouded-wildwood-57711.herokuapp.com/");
            _httpLClient.BaseAddress = new Uri("http://10.0.2.2:5000");
            _httpLClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpLClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", auth);
        }



        public async Task<ObservableCollection<Food>> Search(string query)
        {
            try
            {
                HttpResponseMessage response = await _httpTPClient.GetAsync($"/recipes/complexSearch?apiKey=2d0c6ae62acc44f3ab270441d1628086&query={query}&addRecipeNutrition=true&number=6");
                var responseAsString = await response?.Content?.ReadAsStringAsync();
                var deserializedString = JsonSerializer.Deserialize<FoodDTO.Root>(responseAsString);
                //create a new observablecollection of objects
                //
                var FoodsFromDTO = new ObservableCollection<Food>();
                // for each deserializedstring create a new 
                foreach(var result in deserializedString.results)
                {
                    FoodsFromDTO.Add(new Food
                    {
                        Id = result.id,
                        Name = result.title,
                        Description = FormatDescription(result.summary),
                        Vegan = result.vegan ? "Vegan" : "Not Vegan",
                        ImageLink = result.image,
                        Calories = (int)result.nutrition.nutrients[0].amount,
                        User_name = ""
                    });
                }
                return FoodsFromDTO;
            }
            catch (Exception ex)
            {

                Debug.Fail(ex.Message);
                return new ObservableCollection<Food>();
            }
        }

        public async Task<IEnumerable<Food>> GetFoods()
        {
            try
            {
                var response = await _httpLClient.GetAsync("/api/user/food");
                if (response.IsSuccessStatusCode)
                {
                    var responseAsString = await response?.Content?.ReadAsStringAsync();

                    return JsonSerializer.Deserialize<IEnumerable<Food>>(responseAsString);
                }

            }
            catch (Exception e)
            {
                Debug.Fail(e.Message);
            }

            return new List<Food>();
        }


        public async Task<bool> DeleteFood(Food food)
        {
            try
            {
                var response = await _httpLClient.DeleteAsync($"/api/food/{food.Id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                Debug.Fail(e.Message);
                return false;
            }
        }

        public async Task<bool> AddFood(Food food)
        {
            try
            {
                HttpResponseMessage response = await _httpLClient.PostAsync("/api/food", new StringContent(JsonSerializer.Serialize(food), Encoding.UTF8, "application/json"));
                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                Debug.Fail(e.Message);
                return false;
            }
        }

        //public List<int> findOccurenceIndexes(string str,string substring)
        //{
        //    List<int> indexes = new List<int>();

        //    int index = 0;
        //    while ((index = str.IndexOf(substring, index)) != -1)
        //    {
        //        indexes.Add(index);

        //        index++;
        //    }
        //    return 
        //}


        public string FormatDescription(string desc)
        {
            string temp = desc.Replace("<b>", "").Replace("</b>", "").Replace("<a>", "").Replace("</a>", "");
            int secondPeriodIndex = temp.IndexOf('.', desc.IndexOf('.') + 3);
            return temp.Substring(0, secondPeriodIndex + 1);
        }

        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }


    }


}


        //private List<string,string> FormatResponse(string jsonResponseToString)
        //{
        //"nutrients":[{"name":"Calories"
        //,"id"
        //}


