using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace ClientDemo
{
    public partial class Form3 : Form
    {
        string url = "http://localhost:65458";

        //передать в конструктор имейл
        //public Form3() { }
        public Form3()
        {
            InitializeComponent();

        }

        //создать похожий класс для регистрации
        class AuthUser
        {
            [Required(ErrorMessage = "Значение логина должно быть установлено")]
            [Display(Name = "Логин")]
            public string Login { get; set; }

            [Required(ErrorMessage = "Значение пароля должно быть установлено")]
            [Display(Name = "Пароль")]
            [StringLength(50, MinimumLength = 6, ErrorMessage = "Пароль должен быть не короче 6 символов.")]
            public string Pwd { get; set; }

        }

        //кнопка авторизации      
        private void button1_Click(object sender, EventArgs e)
        {
            var user = new AuthUser() { Login = textBox3.Text, Pwd = textBox2.Text };
            var results = new List<ValidationResult>();
            var context = new ValidationContext(user);
            if (!Validator.TryValidateObject(user, context, results, true))
            {
                label4.Text = null;
                foreach (var error in results)
                {
                    label4.Text += error.ErrorMessage + "\n";
                }
            }
            else
            {
                string token = "";
                var pairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>( "grant_type", "password" ),
                    new KeyValuePair<string, string>( "username", textBox3.Text ),
                    new KeyValuePair<string, string> ( "Password", textBox2.Text )
                };

                var content = new FormUrlEncodedContent(pairs);

                using (var client = new HttpClient())
                {
                    //ошибка при попытке подключения к отключенному серверу
                    HttpResponseMessage response;
                    string result;
                    Dictionary<string, string> tokenDictionary = null;
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    try
                    {
                        response = client.PostAsync(url + "/Token", content).Result;

                        result = response.Content.ReadAsStringAsync().Result;

                        tokenDictionary = js.Deserialize<Dictionary<string, string>>(result);

                        token = tokenDictionary["access_token"];
                        Hide();
                        Form2 form2 = new Form2(textBox3.Text, token);
                        
                        form2.ShowDialog();
                        Close();

                    }
                    catch (Exception ex)
                    {

                        if (tokenDictionary != null && tokenDictionary.ContainsKey("error_description"))
                        {
                            label4.Text = tokenDictionary["error_description"];
                        }
                        else
                        {
                            label4.Text = ex.Message;
                        }

                    }

                }

            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            var reg = new Form1();
            reg.ShowDialog();
            
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
