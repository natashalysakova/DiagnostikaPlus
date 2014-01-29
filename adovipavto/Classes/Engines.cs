using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using adovipavto.Properties;

namespace adovipavto.Classes
{
    class Engines
    {
        private string[] engines;

        public Engines()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);
            ResourceManager rm = new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly());

            engines = new[]
            {
                /*0*/rm.GetString("gas"),
                /*1*/rm.GetString("diesel"),
            };
        }

        public string[] EnginesTitle
        {
            get { return engines; }
        }

        public int GetEngineIndex(string title)
        {
            for (int i = 0; i < EnginesTitle.Length; i++)
            {
                if (EnginesTitle[i] == title)
                    return i;
            }

            return -1;
        }
    }
}
