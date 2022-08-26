using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MobileApp.Models
{
    public class Food
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("vegan")]
        public string Vegan { get; set; }
        [JsonPropertyName("imageLink")]
        public string ImageLink { get; set; }

        [JsonPropertyName("calories")]
        public int Calories { get; set; }

        [JsonPropertyName("user_name")]
        public string User_name { get; set; }

    }
}
