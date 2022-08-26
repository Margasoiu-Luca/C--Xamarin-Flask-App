using System;
using System.Collections.Generic;
using System.Text;
using MobileApp.Services;
using MobileApp.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.Json;

namespace MobileApp.Services
{
    public class ApiAccountService : IAccountService
    {

        private readonly HttpClient _httpClient;
        public ApiAccountService(bool isThisTest)
        {
            HttpClientHandler insecureHandler = GetInsecureHandler();
            _httpClient = new HttpClient(insecureHandler);
            //_httpClient.BaseAddress = new Uri("https://shrouded-wildwood-57711.herokuapp.com/");

            _httpClient.BaseAddress = isThisTest ? new Uri("http://localhost:5000/") : new Uri("http://10.0.2.2:5000/");

            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }
        
        public async Task<string> Login(Account accountToLogin)
        {
            HttpResponseMessage response = new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.BadRequest };

            try
            {
                response = await _httpClient.PostAsync("/api/login", new StringContent(JsonSerializer.Serialize(accountToLogin), Encoding.UTF8, "application/json"));
                var responseAsString = await response?.Content?.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    // - 2 for the last character and - 11 for the first 11 characters of the json string so we get the auth header
                    int temp = responseAsString.Length - 13;
                    return responseAsString.Substring(11, temp);
                }
                else if((int)response.StatusCode == 401)
                {
                    return "Incorrect credentials";
                }
                else
                {
                    return "Something went wrong with the api call";
                }
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.Message);
                return "Error occurred";
            }
        }

        public async Task<bool> Delete(string accountName)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/user/{accountName}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public async Task<string> Register(Account accountToRegister)
        {
            HttpResponseMessage response = new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.BadRequest };
            try
            {
                response = await _httpClient.PostAsync("/api/register",
            new StringContent(JsonSerializer.Serialize(accountToRegister), Encoding.UTF8, "application/json"));

            var responseAsString = await response?.Content?.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                    return "Account successfully created";
                else if (responseAsString.Substring(36, 24) == "UNIQUE constraint failed")
                {
                    return "Username already used";
                }
                else
                {
                    return responseAsString;
                }
            }
            catch(Exception ex)
            {
                return "Incorrect data provided";
            }
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
