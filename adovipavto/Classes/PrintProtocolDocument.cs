using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;

namespace adovipavto.Classes
{
    class PrintProtocolDocument : PrintDocument
    {
        public DataRow Protocol { get; set; }
        private readonly ResourceManager rm = new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly());

        private DataRow _protocolRow;
        private DataRow[] _mesures;

        public PrintProtocolDocument(DataRow protocol, DataRow[] mesures)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);

            _protocolRow = protocol;
            _mesures = mesures;
            OriginAtMargins = true;
            DefaultPageSettings.Margins = new Margins(60, 30, 30, 30);
            DocumentName = rm.GetString("protocol");
            PrintPage+=PrintProtocolDocument_PrintPage;
            
            DefaultPageSettings.PaperSize.RawKind = 9;
            /*A4 - http://msdn.microsoft.com/en-us/library/system.drawing.printing.papersize.rawkind(v=vs.110).aspx */

        }

        private void PrintProtocolDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;

            Font titleFont = new Font(FontFamily.GenericSansSerif, 18F, FontStyle.Bold);
            Font boldFont = new Font(FontFamily.GenericSansSerif, 11F, FontStyle.Bold);
            Font normalFont = new Font(FontFamily.GenericSansSerif, 10F, FontStyle.Regular);
            Font bigFont = new Font(FontFamily.GenericSansSerif, 18F, FontStyle.Bold);
            Font smallFont = new Font(FontFamily.GenericSansSerif, 7F, FontStyle.Regular);

            Normatives norm = new Normatives();

            int LineHeight = 21;

            float cenerPoint = e.PageBounds.Width / 2.0f;

            var sf = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };


            //1 столбец - 450, 2 - 150, 3-200, 4 - 70

            g.DrawString(rm.GetString("results") + _protocolRow["BlankNumber"], titleFont, Brushes.Black, new PointF(cenerPoint, LineHeight), sf);

            g.DrawString(rm.GetString("brakeSystem"), boldFont, Brushes.Black, new PointF(40F, 2 * LineHeight));
            DrawHeader(g, normalFont, 2 * LineHeight);


            for (int i = 0; i < 5; i++)
            {
                g.DrawString(norm.NormativesTitle[i], normalFont, Brushes.Black, new PointF(20F, (i + 3) * LineHeight));

                DrawPictorgam(i, g, (i + 3) * LineHeight, normalFont);
            }

            g.DrawString(norm.NormativesTitle[20], normalFont, Brushes.Black, new PointF(20F, 8 * LineHeight));
            DrawPictorgam(20, g, 8 * LineHeight, normalFont);

            for (int i = 5; i < 7; i++)
            {
                g.DrawString(norm.NormativesTitle[i], normalFont, Brushes.Black, new PointF(20F, (i + 4) * LineHeight));

                DrawPictorgam(i, g, (i + 4) * LineHeight, normalFont);
            }


            g.DrawString(rm.GetString("wheelSystem"), boldFont, Brushes.Black, new PointF(40F, 11 * LineHeight));
            DrawHeader(g, normalFont, 11 * LineHeight);

            g.DrawString(norm.NormativesTitle[7], normalFont, Brushes.Black, new PointF(20F, 12 * LineHeight));
            DrawPictorgam(7, g, 12 * LineHeight, normalFont);


            g.DrawString(rm.GetString("lightSystem"), boldFont, Brushes.Black, new PointF(40F, 13 * LineHeight));
            DrawHeader(g, normalFont, 13 * LineHeight);

            for (int i = 8; i < 12; i++)
            {
                g.DrawString(norm.NormativesTitle[i], normalFont, Brushes.Black, new PointF(20F, (i + 6) * LineHeight));
                DrawPictorgam(i, g, (i + 6) * LineHeight, normalFont);

            }

            g.DrawString(rm.GetString("wheelAndTyres"), boldFont, Brushes.Black, new PointF(40F, 18 * LineHeight));
            DrawHeader(g, normalFont, 18 * LineHeight);

            g.DrawString(norm.NormativesTitle[12], normalFont, Brushes.Black, new PointF(20F, 19 * LineHeight));
            DrawPictorgam(12, g, 19 * LineHeight, normalFont);


            g.DrawString(rm.GetString("engineAndItsSystem"), boldFont, Brushes.Black, new PointF(40F, 20 * LineHeight));
            DrawHeader(g, normalFont, 20 * LineHeight);

            for (int i = 13; i < 17; i++)
            {
                g.DrawString(norm.NormativesTitle[i], normalFont, Brushes.Black, new PointF(20F, (i + 8) * LineHeight));
                DrawPictorgam(i, g, (i + 8) * LineHeight, normalFont);

            }


            g.DrawString(norm.NormativesTitle[17], normalFont, Brushes.Black, new PointF(20F, 25 * LineHeight));
            DrawPictorgam(17, g, 25 * LineHeight, normalFont, true);

            g.DrawString(norm.NormativesTitle[18], normalFont, Brushes.Black, new PointF(20F, 26 * LineHeight));
            DrawPictorgam(18, g, 26 * LineHeight, normalFont, true);



            g.DrawString(rm.GetString("GBO"), boldFont, Brushes.Black, new PointF(40F, 27 * LineHeight));
            DrawHeader(g, normalFont, 27 * LineHeight);


            g.DrawString(norm.NormativesTitle[19], normalFont, Brushes.Black, new PointF(20F, 28 * LineHeight));
            DrawPictorgam(19, g, 28 * LineHeight, normalFont);



            g.DrawString(rm.GetString("glass"), boldFont, Brushes.Black, new PointF(40F, 29 * LineHeight));
            DrawHeader(g, normalFont, 29 * LineHeight);

            for (int i = 21; i < 23; i++)
            {
                g.DrawString(norm.NormativesTitle[i], normalFont, Brushes.Black, new PointF(20F, (i + 9) * LineHeight));
                DrawPictorgam(i, g, (i + 9) * LineHeight, normalFont);

            }

            g.DrawString(rm.GetString("noise"), boldFont, Brushes.Black, new PointF(40F, 32 * LineHeight));
            DrawHeader(g, normalFont, 32 * LineHeight);

            g.DrawString(norm.NormativesTitle[23], normalFont, Brushes.Black, new PointF(20F, 33 * LineHeight));
            DrawPictorgam(23, g, 33 * LineHeight, normalFont);


            g.DrawString(rm.GetString("visualCheck"), boldFont, Brushes.Black, new PointF(40F, 34 * LineHeight));

            string vslChk = (bool)_protocolRow["VisualCheck"] ? rm.GetString("check") : rm.GetString("uncheck");
            g.DrawString(vslChk, boldFont, Brushes.Black, new PointF(660F, 34.25f * LineHeight), new StringFormat() { Alignment = StringAlignment.Center });
            g.DrawString(rm.GetString("visualCheck2"), smallFont, Brushes.Black, new PointF(40F, 34.8f * LineHeight));

            g.DrawLine(Pens.Black, 15, (2 * LineHeight) - 3, 15, (35.5f * LineHeight) - 3);
            g.DrawLine(Pens.Black, 750, (2 * LineHeight) - 3, 750, (35.5f * LineHeight) - 3);
            g.DrawLine(Pens.Black, 450, (2 * LineHeight) - 3, 450, (34 * LineHeight) - 3);
            g.DrawLine(Pens.Black, 570, (2 * LineHeight) - 3, 570, (35.5f * LineHeight) - 3);
            g.DrawLine(Pens.Black, 650, (2 * LineHeight) - 3, 650, (34 * LineHeight) - 3);

            for (int i = 2; i < 35; i++)
            {
                g.DrawLine(Pens.Black, 15, (i * LineHeight) - 3, 750, (i * LineHeight) - 3);
            }
            g.DrawLine(Pens.Black, 15, (35.5f * LineHeight) - 3, 750, (35.5f * LineHeight) - 3);


            string s = (bool)_protocolRow["Result"] ? rm.GetString("check") : rm.GetString("uncheck");

            if (s != null)
            {
                var s1 = rm.GetString("results2");

                if (s1 != null)
                {
                    g.DrawString(s1.ToUpper(), bigFont, Brushes.Black, new PointF(200, 37f * LineHeight), sf);
                    g.DrawString(s.ToUpper(), bigFont, Brushes.Black, new PointF(200, 39f * LineHeight), sf);
                    g.DrawRectangle(Pens.Black, 20, 36f * LineHeight, 370, 4 * LineHeight);
                }
            }

            g.DrawString(rm.GetString("mechanic"), boldFont, Brushes.Black, new PointF(30, 40.5f * LineHeight));
            g.DrawString(Program.VipAvtoDataSet.GetShortMechanicName((int)_protocolRow["IDMechanic"]), boldFont,
                Brushes.Black, new PointF(165f, 42.5f * LineHeight), sf);
            g.DrawLine(Pens.Black, 20, 43 * LineHeight, 350, 43 * LineHeight);
            g.DrawString(rm.GetString("FIO"), smallFont, Brushes.Black, new PointF(165, (44 * LineHeight) - 5), sf);


            g.DrawLine(Pens.Black, 20, 46 * LineHeight, 350, 46 * LineHeight);
            g.DrawString(rm.GetString("signature"), smallFont, Brushes.Black, new PointF(165, (47 * LineHeight) - 5), sf);

            g.DrawString(((DateTime)_protocolRow["Date"]).ToShortDateString(), boldFont,
    Brushes.Black, new PointF(165f, 48.5f * LineHeight), sf);

            g.DrawLine(Pens.Black, 20, 49 * LineHeight, 350, 49 * LineHeight);
            g.DrawString(rm.GetString("data"), smallFont, Brushes.Black, new PointF(165, (50 * LineHeight) - 5), sf);



            var techrect = new Rectangle(420, (36 * LineHeight), 320, LineHeight * 14);
            g.DrawRectangle(Pens.Black, techrect);
            g.DrawString(rm.GetString("techphoto"), smallFont, Brushes.Black, techrect, sf);
            if (_protocolRow["TechPhoto"] != null)
            {
                string path = _protocolRow["TechPhoto"].ToString();
                if (File.Exists(path))
                    g.DrawImage(new Bitmap(path), techrect);
            }



            g.DrawLine(Pens.Black, 10, 51 * LineHeight, 750, 51 * LineHeight);


            g.DrawString(rm.GetString("oboznach"), smallFont, Brushes.Black, new PointF(75, 52 * LineHeight));
            g.DrawString(" - " + rm.GetString("check"), smallFont, Brushes.Black, new PointF(260, 52 * LineHeight));
            g.DrawString(" - " + rm.GetString("uncheck"), smallFont, Brushes.Black, new PointF(460, 52 * LineHeight));
            g.DrawString(" - " + rm.GetString("notCheck"), smallFont, Brushes.Black, new PointF(660, 52 * LineHeight));

            g.DrawImage(Properties.Resources.pass, 240, (int)(52 * LineHeight) - 2, 15, 15);
            g.DrawImage(Properties.Resources.fail, 440, (int)(52 * LineHeight) - 2, 15, 15);
            g.DrawImage(Properties.Resources.none, 640, (int)(52 * LineHeight) - 2, 15, 15);

        }

        private void DrawPictorgam(int i, Graphics graphics, int height, Font normalFont, bool mode = false)
        {
            StringFormat sf = new StringFormat() { Alignment = StringAlignment.Center };

            double[] value =
                (from DataRow item in _mesures where (int)item["NormativeID"] == i select (double)item["Value"])
                    .ToArray();
            if (value.Length == 0)
            {
                graphics.DrawImage(Properties.Resources.none, 690, height, 15, 15);
            }
            else
            {
                if (mode)
                    graphics.DrawString(value[0].ToString() + " (" + GetSmokeVal(value[0]) + ")", normalFont, Brushes.Black, new PointF(610F, height), sf);
                else
                {
                    graphics.DrawString(value[0].ToString(), normalFont, Brushes.Black, new PointF(610F, height), sf);
                }

                double minval =
                    (double)(from DataRow item in Program.VipAvtoDataSet.Tables[Constants.NormativesTableName].Rows
                             where (int)item["IDGroup"] == (int)_protocolRow["IDGroup"] &&
                                   (int)item["Tag"] == i
                             select item["MinValue"]).ToArray()[0];
                double maxval =
                    (double)(from DataRow item in Program.VipAvtoDataSet.Tables[Constants.NormativesTableName].Rows
                             where (int)item["IDGroup"] == (int)_protocolRow["IDGroup"] &&
                                   (int)item["Tag"] == i
                             select item["MaxValue"]).ToArray()[0];

                if (mode)
                    graphics.DrawString(
                        String.Format("[{0};{1}] ({2};{3})", minval, maxval, GetSmokeVal(minval), GetSmokeVal(maxval)),
                        normalFont, Brushes.Black, new PointF(510F, height), sf);
                else
                {
                    graphics.DrawString(String.Format("[{0};{1}]", minval, maxval), normalFont, Brushes.Black, new PointF(510F, height), sf);

                }


                if (minval <= value[0] && value[0] < maxval)
                {
                    graphics.DrawImage(Properties.Resources.pass, 690, height, 15, 15);
                }
                else
                {
                    graphics.DrawImage(Properties.Resources.fail, 690, height, 15, 15);
                }
            }

        }

        private double GetSmokeVal(double SmokeVal)
        {
            return Math.Round((-1.0 / 0.43) * Math.Log(1.0 - (SmokeVal / 100.0)), 3);
        }

        private void DrawHeader(Graphics g, Font normalFont, int lineHeight)
        {
            StringFormat sf = new StringFormat() { Alignment = StringAlignment.Center };

            g.DrawString(rm.GetString("value"), normalFont, Brushes.Black, new PointF(610F, lineHeight), sf);
            g.DrawString(rm.GetString("zakluch"), normalFont, Brushes.Black, new PointF(700F, lineHeight), sf);
            g.DrawString(rm.GetString("norm"), normalFont, Brushes.Black, new PointF(510F, lineHeight), sf);


        }

    }
}
