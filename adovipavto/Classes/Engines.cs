using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace adovipavto.Classes
{
    internal class Engines : IEnumerable
    {
        private readonly string[] _engines;

        public Engines()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Instance.Language);
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

        internal string[] GetAllEngines()
        {
            return _engines;
        }
    }
}