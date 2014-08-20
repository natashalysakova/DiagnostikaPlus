using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using adovipavto.Properties;

namespace adovipavto.Classes
{
    public class PrintJournalDocument : PrintDocument
    {
        private readonly DateTime _from;

        private readonly ResourceManager _rm = new ResourceManager("adovipavto.StringResource",
            Assembly.GetExecutingAssembly());

        private readonly NewVipAvtoSet.ProtocolsRow[] _rows;
        private readonly NewVipAvtoSet _set;
        private readonly DateTime _to;


        private int _index;

        public PrintJournalDocument(NewVipAvtoSet.ProtocolsRow[] rows, DateTime from, DateTime to, NewVipAvtoSet set)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language);

            _rows = rows;
            _from = from;
            _to = to;
            _set = set;
            OriginAtMargins = true;
            DefaultPageSettings.Margins = new Margins(40, 30, 30, 30);
            DocumentName = _rm.GetString("journal") + from.ToShortDateString() + " - " + to.ToShortDateString();
            PrintPage += PrintProtocolDocument_PrintPage;
            DefaultPageSettings.PaperSize.RawKind = 9;
            /*A4 - http://msdn.microsoft.com/en-us/library/system.drawing.printing.papersize.rawkind(v=vs.110).aspx */
        }

        public DataRow Protocol { get; set; }

        private void PrintProtocolDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;

            var titleFont = new Font(FontFamily.GenericSansSerif, 18F, FontStyle.Bold);
            var normalFont = new Font(FontFamily.GenericSansSerif, 10F, FontStyle.Regular);
            const int lineHeight = 21;
            float cenerPoint = e.PageBounds.Width/2.0f;

            var sf = new StringFormat
            {
                Alignment = StringAlignment.Center
            };

            if (_from.ToShortDateString() == _to.ToShortDateString())
            {
                g.DrawString(_rm.GetString("journal") + " " + _from.ToShortDateString(), titleFont, Brushes.Black,
                    new PointF(cenerPoint, lineHeight), sf);
            }
            else
            {
                g.DrawString(
                    _rm.GetString("journalFrom") + " " + _from.ToShortDateString() + " " + _rm.GetString("to") + " " +
                    _to.ToShortDateString(), titleFont, Brushes.Black, new PointF(cenerPoint, lineHeight), sf);
            }


            g.DrawLine(Pens.Black, 15, (3*lineHeight), 750, (3*lineHeight));

            int i = 0;

            for (int j = _index; j < _rows.Length; j++)
            {
                if (i > 49)
                {
                    _index = j;
                    e.HasMorePages = true;
                    return;
                }


                g.DrawString((j + 1).ToString(), normalFont, Brushes.Black,
                    new PointF(20, lineHeight*(i + 3) + 3));
                g.DrawString(_rows[j].BlankNumber, normalFont, Brushes.Black,
                    new PointF(65, lineHeight*(i + 3) + 3));

                g.DrawString(_set.GetGroupTitle(_rows[j].GroupId), normalFont,
                    Brushes.Black, new PointF(185, lineHeight*(i + 3) + 3));

                g.DrawString(_rows[j].Date.ToShortDateString(), normalFont, Brushes.Black,
                    new PointF(370, lineHeight*(i + 3) + 3));
                string s = _rows[j].Result ? _rm.GetString("check") : _rm.GetString("uncheck");
                g.DrawString(s, normalFont, Brushes.Black, new PointF(450, lineHeight*(i + 3) + 3));
                g.DrawString(_set.GetShortMechanicName(_rows[j].MechanicId), normalFont,
                    Brushes.Black, new PointF(575, lineHeight*(i + 3) + 3));


                g.DrawLine(Pens.Black, 15, ((i + 4)*lineHeight), 750, ((i + 4)*lineHeight));
                g.DrawLine(Pens.Black, 15, (i + 3)*lineHeight, 15, (i + 4)*lineHeight);
                g.DrawLine(Pens.Black, 60, (i + 3)*lineHeight, 60, (i + 4)*lineHeight);
                g.DrawLine(Pens.Black, 180, (i + 3)*lineHeight, 180, (i + 4)*lineHeight);
                g.DrawLine(Pens.Black, 365, (i + 3)*lineHeight, 365, (i + 4)*lineHeight);
                g.DrawLine(Pens.Black, 445, (i + 3)*lineHeight, 445, (i + 4)*lineHeight);
                g.DrawLine(Pens.Black, 570, (i + 3)*lineHeight, 570, (i + 4)*lineHeight);
                g.DrawLine(Pens.Black, 750, (i + 3)*lineHeight, 750, (i + 4)*lineHeight);


                i++;
            }

            g.DrawLine(Pens.Black, 15, ((i + 4)*lineHeight), 750, ((i + 4)*lineHeight));
            //some statistics
            int check = (from dataRow in _rows
                where dataRow.Result
                select dataRow).ToArray().Length;
            int uncheck = _rows.Length - check;
            g.DrawString(_rm.GetString("check") + ": " + check, normalFont, Brushes.Black,
                new PointF(50, lineHeight*(i + 5) + 3));
            g.DrawString(_rm.GetString("uncheck") + ": " + uncheck, normalFont, Brushes.Black,
                new PointF(200, lineHeight*(i + 5) + 3));
            g.DrawString(_rm.GetString("total") + ": " + (check + uncheck), normalFont, Brushes.Black,
                new PointF(350, lineHeight*(i + 5) + 3));
        }
    }
}