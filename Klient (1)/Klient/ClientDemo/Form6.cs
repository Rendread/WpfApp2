using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;


namespace ClientDemo
{
    public partial class Form6 : Form
    {

        public Form6(Lector lector)
        {            
            InitializeComponent();
            var barcodeWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new ZXing.QrCode.QrCodeEncodingOptions
                {
                    CharacterSet = "utf-8",
                    Height = 500,
                    Width = 500,
                    Margin = 1


                }

            };

            pictureBox1.Image = barcodeWriter.Write(lector.UniqueCode);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        public class Lector
        {
            public string Name { get; set; }
            public string TableName { get; set; }
            public string Subject { get; set; }
            public string UniqueCode { get; set; }

        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }
    }
}
