using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using ClientDemo.Models;
using Newtonsoft.Json;

namespace ClientDemo
{

    public partial class Form2 : Form
    {
        string f2token;
        string f2email;
        List<ProfessorTable> professors { get; set; }


        public Form2(string email, string token)
        {
            InitializeComponent();
            f2token = token;
            f2email = email;
          
        }


        class ProfessorTable
        {
            public string TableName { get; set; }
            public string StudentsCourse { get; set; }
            public string SubjectName { get; set; }
            public string StudentsGroups { get; set; }
            public string UniqueCode { get; set; }
        }


        private void button1_Click(object sender, EventArgs e)
        {

            Form4 form4 = new Form4(f2email,f2token);
            form4.ShowDialog();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            
        }

        private void Form2_Load(object sender, EventArgs e)
        {            
            AuthStuff auth = new AuthStuff();
       
            using (var client = auth.CreateClient(f2token))
            {
                //"http://localhost:65458/api/DBManage/GetTables?prepodname=JJoyce@gmail.com"

                    var response = client.GetAsync("http://localhost:65458/" + "api/DBManage/GetTables/" + $"?prepodname={f2email}").Result;
                //textBox1.Text = response.Content.ReadAsStringAsync().Result;

                    professors = JsonConvert.DeserializeObject<List<ProfessorTable>>(response.Content.ReadAsStringAsync().Result);
                    //comboBox1.Items.Add(myNewObject.TableName);
                    dataGridView1.DataSource = professors;

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            Form5 form5 = new Form5(professors[e.RowIndex].TableName, f2token, f2email);
            Form6 form6 = new Form6(new Form6.Lector { TableName = professors[e.RowIndex].TableName, Name = f2email, Subject = professors[e.RowIndex].SubjectName, UniqueCode = professors[e.RowIndex].UniqueCode });
            form5.Show();
            form6.Show();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            AuthStuff auth = new AuthStuff();

            using (var client = auth.CreateClient(f2token))
            {
                //"http://localhost:65458/api/DBManage/GetTables?prepodname=JJoyce@gmail.com"

                var response = client.GetAsync("http://localhost:65458/" + "api/DBManage/GetTables/" + $"?prepodname={f2email}").Result;
                //textBox1.Text = response.Content.ReadAsStringAsync().Result;

                professors = JsonConvert.DeserializeObject<List<ProfessorTable>>(response.Content.ReadAsStringAsync().Result);
                //comboBox1.Items.Add(myNewObject.TableName);
                dataGridView1.DataSource = professors;

            }
        }
    }

}


