using ClientDemo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientDemo
{
    public partial class Form1 : Form
    {
        AuthStuff auth;
        private const string url = "http://localhost:65458";

        public Form1()
        {
            InitializeComponent();
        }


        //кнопка регистрации
        private async void button1_ClickAsync(object sender, EventArgs e)
        {

            var registerModel = new ProfessorRegisterModel() { Email = textBox3.Text, SpecOrGroup = textBox2.Text, userName = textBox1.Text, Password = textBox4.Text };
            var results = new List<ValidationResult>();
            var context = new ValidationContext(registerModel);
            if (!Validator.TryValidateObject(registerModel, context, results, true))
            {
                label5.Text = null;
                foreach (var error in results)
                {
                    label5.Text += error.ErrorMessage + "\n";
                }
            }

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = null;

                try
                {
                    response = await client.PostAsJsonAsync(url + "/api/Account/Register", registerModel);

                    label5.Text = "Регистрация прошла успешно.";

                    Thread.Sleep(1000);

                }
                catch (Exception ex)
                {
                    if (response != null)
                    {
                        label5.Text = response.Content.ReadAsStringAsync().ToString() + ex.Message;
                    }
                    else
                    {
                        label5.Text = ex.Message;
                    }
                }


            }

        }

    }
}
