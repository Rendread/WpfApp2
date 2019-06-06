using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ClientDemo.Models;

namespace ClientDemo
{
    class AuthStuff
    {
        private const string url = "http://localhost:65458";
        
        public HttpResponseMessage Register(string name , string email, string password, string spec)
        {
            var registerModel = new
            {
                userName = name,
                Email = email,
                Password = password,
                Role = "Преподаватель",
                SpecOrGroup = spec,
                ConfirmPassword = password

            };
            using (var client = new HttpClient())
            {

                Task<HttpResponseMessage> response = null;

                response = client.PostAsJsonAsync(url + "/api/Account/Register", registerModel);
                return response.Result;


            }
        }

       

        // создаем http-клиента с токеном 
        public HttpClient CreateClient(string accessToken = "")
        {
            var client = new HttpClient();
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            }
            return client;
        }


    }



}

