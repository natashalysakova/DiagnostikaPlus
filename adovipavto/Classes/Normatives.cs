using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using adovipavto.Properties;

namespace adovipavto.Classes
{
    internal class Normatives : IEnumerable
    {
        private readonly List<int> _decimals;
        private readonly List<string> _normas;

        public Normatives()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Instance.Language);
            var rm = new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly());

            _normas = new List<string>
            {
                /*0*/rm.GetString("OUTSRTS"),
                /*1*/rm.GetString("OUTSSTS"),
                /*2*/rm.GetString("ORTS1"),
                /*3*/rm.GetString("ORTS2"),
                /*4*/rm.GetString("ORTS3"),
                /*5*/rm.GetString("MVSTS"),
                /*6*/rm.GetString("KUNOU1"),
                /*7*/rm.GetString("SL"),
                /*8*/rm.GetString("SSFBS"),
                /*9*/rm.GetString("SSFDS"),
                /*10*/rm.GetString("SSPF"),
                /*11*/rm.GetString("CHPUP"),
                /*12*/rm.GetString("OVRP"),
                /*13*/rm.GetString("SCOMCHV"),
                /*14*/rm.GetString("SCOMACHV"),
                /*15*/rm.GetString("SCHMCHV"),
                /*16*/rm.GetString("SCHMACHV"),
                /*17*/rm.GetString("DVRSUM"),
                /*18*/rm.GetString("DVRSUP"),
                /*19*/rm.GetString("ORTSSS"),
                /*20*/rm.GetString("PVS"),
                /*21*/rm.GetString("PPBS"),
                /*22*/rm.GetString("VSHA"),
            };

            _decimals = new List<int>
            {
                /*0*/2,
                /*1*/2,
                /*2*/0,
                /*3*/0,
                /*4*/0,
                /*5*/0,
                /*6*/0,
                /*7*/0,
                /*8*/0,
                /*9*/0,
                /*10*/0,
                /*11*/0,
                /*12*/1,
                /*13*/2,
                /*14*/2,
                /*15*/0,
                /*16*/0,
                /*17*/2,
                /*18*/2,
                /*19*/0,
                /*20*/0,
                /*21*/0,
                /*22*/0,
            };
        }


        public List<int> DecimalPoints
        {
            get { return _decimals; }
        }

        public int GetNormativeIndex(string title)
        {
            for (int i = 0; i < _normas.Count; i++)
            {
                if (_normas[i] == title)
                    return i;
            }

            return -1;
        }

        public string this[int i]
        {
            get { return _normas[i]; }
        }

        public IEnumerator GetEnumerator()
        {
            return _normas.GetEnumerator();
        }

        public int Count
        {
            get { return _normas.Count; }
        }

        internal int IndexOf(string p)
        {
            return _normas.IndexOf(p);
        }

        internal List<string> GetAllNormatives()
        {
            return _normas;
        }
    }
}