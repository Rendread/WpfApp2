using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

namespace ClientDemo
{
    public partial class Form5 : Form
    {
        string f5token;
        string f5email;
        string tblname;

        public Form5(string tablename,string token, string email)
        {
            InitializeComponent();

            f5token = token;
            f5email = email;
            tblname = tablename;
            
                     
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            
            AuthStuff auth = new AuthStuff();
            var client = auth.CreateClient(f5token);


            try
            {
                using (client)
                {

                    var response = client.GetAsync("http://localhost:65458/" + "api/DBManage/GetStudents/" + $"?tableName=\"{tblname}\"").Result;
                   
                    dataGridView1.DataSource = JsonConvert.DeserializeObject<List<Table>>(response.Content.ReadAsStringAsync().Result);
                    

                }
            }
            catch
            {
                label1.Text = "Произошла ошибка";
            }
            

        }


        private void button1_Click_1(object sender, EventArgs e)
        {

            AuthStuff auth = new AuthStuff();
            var client = auth.CreateClient(f5token);


            try
            {
                using (client)
                {
                    if (dateTimePicker1.Value.Equals(DateTime.Now))
                    {

                        var response = client.GetAsync("http://localhost:65458/" + "api/DBManage/GetStudents/" + $"?tableName=\"{tblname}\"").Result;

                       dataGridView1.DataSource = JsonConvert.DeserializeObject<List<Table>>(response.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        var response = client.GetAsync("http://localhost:65458/" + "api/DBManage/GetStudents/" + $"?tableName=\"{tblname}\"&visitatdate={dateTimePicker1.Value.ToString("yyyyMMdd")}").Result;

                        dataGridView1.DataSource = JsonConvert.DeserializeObject<List<Table>>(response.Content.ReadAsStringAsync().Result);

                    }

                }
            }
            catch 
            {
                label1.Text = "Произошла ошибка.";
            }

        }

        
    }
    class Table
    {
        public string Name { get; set; }
        public string Group { get; set; }
    }
}
