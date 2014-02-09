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
        private List<string> normas;
        private List<int> decimals;

        public Normatives()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);
            ResourceManager rm = new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly());

            normas = new List<string>()
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
                /*19*/rm.GetString("GGBS"),
                /*20*/rm.GetString("ORTSSS"),
                /*21*/rm.GetString("PVS"),
                /*22*/rm.GetString("PPBS"),
                /*23*/rm.GetString("VSHA"),
            };

            decimals = new List<int>
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
                /*19*/3,/**********************/
                /*20*/0,
                /*21*/0,
                /*22*/0,
                /*23*/0,
            };

        }

        public List<string> NormativesTitle
        {
            get { return normas; }
        }

        public List<int> DecimalPoints
        {
            get { return decimals; }
        }

        public int GetNormativeIndex(string title)
        {
            for (int i = 0; i < NormativesTitle.Count; i++)
            {
                if (NormativesTitle[i] == title)
                    return i;
            }

            return -1;
        }
    }
}
