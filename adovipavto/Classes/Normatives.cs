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
    class Normatives
    {
        private string[] normas;

        public Normatives()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);
            ResourceManager rm = new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly());

            normas = new[]
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
                /*19*/rm.GetString("CHVNMO"),
                /*20*/rm.GetString("CHVNPO"),
                /*21*/rm.GetString("PVS"),
                /*22*/rm.GetString("PPBS"),
                /*23*/rm.GetString("VSHA")
            };
        }

        public string[] NormativesTitle
        {
            get { return normas; }
        }

        public int GetNormativeIndex(string title)
        {
            for (int i = 0; i < NormativesTitle.Length; i++)
            {
                if (NormativesTitle[i] == title)
                    return i;
            }

            return -1;
        }
    }
}
