using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientDemo.Models;

namespace ClientDemo
{
    public partial class Form4 : Form
    {
        FormTableName usertable = new FormTableName() { Groups = new List<string>()};
        string f4token;

        public Form4(string email, string token)
        {
            InitializeComponent();
            f4token = token;
            usertable.Email = email;

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            usertable.Course = (sender as ListBox).SelectedItem.ToString();           
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            usertable.Subject = (sender as ListBox).SelectedItem.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            usertable.Groups = checkedListBox1.CheckedItems.OfType<string>().ToList();


            AuthStuff auth = new AuthStuff();
            try
            {
                using (var client = auth.CreateClient(f4token))
                {

                    var response = client.PostAsJsonAsync("http://localhost:65458/" + "api/DBManage/CreateTable/", usertable).Result;
                    label1.Text = response.Content.ReadAsStringAsync().Result;

                }
            }
            catch (Exception ex)
            {
                label1.Text = ex.Message;
            }

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }

}
