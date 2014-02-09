using System;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using adovipavto.Properties;

namespace adovipavto
{
    public sealed partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language);
            InitializeComponent();
            var rm = new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly());
            BackgroundImage = (Image) rm.GetObject("splashScreen2");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(1);

            if (progressBar1.Value == progressBar1.Maximum)
            {
                timer1.Stop();
                Close();
            }
        }
    }
}