using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using WebClient.Models;

namespace WebClient
{
    public class LoginModel : PageModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public async Task<LoginInfo> CheckLogin()
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:4201/")
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/guest/login", new LoginModel() { Password = this.Password, Username = this.Username });
            
                var result = await response.Content.ReadAsAsync<LoginInfo>();
                return result;
        }



    }


}
