using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;
using System.Threading;
using adovipavto.Properties;

namespace adovipavto.Classes
{
    public class Engines : IEnumerable
    {
        private readonly string[] _engines;

        public Engines()
        {
            if(Settings.Default.Language != "")
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language);
            else
            {
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("RU-ru");
            }
            var rm = new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly());

            _engines = new[]
            {
                /*0*/rm.GetString("gas"),
                /*1*/rm.GetString("diesel"),
                /*2*/""
            };
        }


        public string this[int i]
        {
            get { return _engines[i]; }
        }

        public IEnumerator GetEnumerator()
        {
            return _engines.GetEnumerator();
        }

        public int GetEngineIndex(string title)
        {
            for (int i = 0; i < _engines.Length; i++)
            {
                if (_engines[i] == title)
                    return i;
            }

            return -1;
        }

        public string[] GetAllEngines()
        {
            return _engines;
        }
    }
}