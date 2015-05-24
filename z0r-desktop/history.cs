using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace z0r_desktop
{
    public partial class history : Form
    {

        public string historyFile = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/z0r/history.txt"; 

        public history()
        {
            InitializeComponent();
        }

        private void history_Load(object sender, EventArgs e)
        {

            StreamReader readText = new StreamReader(historyFile);
            string line;

            while ((line = readText.ReadLine()) != null)
            {
                listBox1.Items.Add(line);
            }

            readText.Close();

        }
    }
}
