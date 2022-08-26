using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;


namespace MobileApp.Models
{
    public  class Account
    {
        [JsonPropertyName("user")]
        public string User { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        //Unused
        [JsonPropertyName("gender")]
        public bool Gender { get; set; }

    }
}