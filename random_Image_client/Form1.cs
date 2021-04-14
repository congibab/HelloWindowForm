using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using System.IO;
using System.Net;
using System.Text.Json;

namespace random_Image_client
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = "http://localhost/Gachar.php";
            var JsonObject = GetJsonData(url);

            url = "http://localhost/" + JsonObject.id;

            textBox1.Text = "Card Rank : " + JsonObject.Rank;
            pictureBox1.Image = GetImage(url);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        //=================================================================
        //=================================================================
        private Person GetJsonData(string url)
        {
            var request = WebRequest.Create(url);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            var reader = new StreamReader(webStream);

            var data = reader.ReadToEnd();

            var JsonObject = JsonSerializer.Deserialize<Person>(data);

            return JsonObject;
        }

        private Image GetImage(string url)
        {
            using (WebClient client = new WebClient())
            {
                byte[] imgArray;
                imgArray = client.DownloadData(url);

                using (MemoryStream memstr = new MemoryStream(imgArray))
                {
                    Image img = Image.FromStream(memstr);
                    return img;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
