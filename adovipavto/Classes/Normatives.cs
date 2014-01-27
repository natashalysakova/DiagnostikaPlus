using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

            normas = new[]
            {
                /*0*/Resources.OUTSRTS,
                /*1*/Resources.OUTSSTS,
                /*2*/Resources.ORTS1,
                /*3*/Resources.ORTS2,
                /*4*/Resources.ORTS3,
                /*5*/Resources.MVSTS,
                /*6*/Resources.KUNOU1,
                /*7*/Resources.SL,
                /*8*/Resources.SSFBS,
                /*9*/Resources.SSFDS,
                /*10*/Resources.SSPF,
                /*11*/Resources.CHPUP,
                /*12*/Resources.OVRP,
                /*13*/Resources.SCOMCHV,
                /*14*/Resources.SCOMACHV,
                /*15*/Resources.SCHMCHV,
                /*16*/Resources.SCHMACHV,
                /*17*/Resources.DVRSUM,
                /*18*/Resources.DVRSUP,
                /*19*/Resources.CHVNMO,
                /*20*/Resources.CHVNPO,
                /*21*/Resources.PVS,
                /*22*/Resources.PPBS,
                /*23*/Resources.VSHA
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
