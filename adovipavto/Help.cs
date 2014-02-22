using System;
using System.Windows.Forms;

namespace adovipavto
{
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
        }

        private void Help_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate(Application.StartupPath + @"\Help\index.html");
        }
    }
}
