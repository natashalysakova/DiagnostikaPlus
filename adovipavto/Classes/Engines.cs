using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using adovipavto.Properties;

namespace adovipavto.Classes
{
    internal class Engines
    {
        private readonly string[] _engines;

        public Engines()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language);
            var rm = new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly());

            _engines = new[]
            {
                /*0*/rm.GetString("gas"),
                /*1*/rm.GetString("diesel")
            };
        }

        public string[] EnginesTitle
        {
            get { return _engines; }
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