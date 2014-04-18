using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using adovipavto.Enums;
using adovipavto.Properties;

namespace adovipavto.Classes
{
    internal class PrintProtocolDocument : PrintDocument
    {
        private readonly DateTime _from;
        private readonly NewVipAvtoSet.MesuresRow[] _mesures;
        private readonly NewVipAvtoSet.ProtocolsRow _protocolRow;

        private readonly ResourceManager _rm = new ResourceManager("adovipavto.StringResource",
            Assembly.GetExecutingAssembly());

        private readonly NewVipAvtoSet.ProtocolsRow[] _rows;
        private readonly NewVipAvtoSet _set;
        private readonly DateTime _to;


        private int _index;

        public PrintProtocolDocument(NewVipAvtoSet.ProtocolsRow protocol, NewVipAvtoSet.MesuresRow[] mesures, NewVipAvtoSet set)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Instance.Language);

            _protocolRow = protocol;
            _mesures = mesures;
            _set = set;
            OriginAtMargins = true;
            DefaultPageSettings.Margins = new Margins(60, 30, 30, 30);
            DocumentName = _rm.GetString("protocol");
            PrintPage += PrintProtocolDocument_PrintPage;

            DefaultPageSettings.PaperSize.RawKind = 9;
            /*A4 - http://msdn.microsoft.com/en-us/library/system.drawing.printing.papersize.rawkind(v=vs.110).aspx */
        }

        public PrintProtocolDocument(NewVipAvtoSet.ProtocolsRow[] rows, DateTime from, DateTime to, NewVipAvtoSet set)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Instance.Language);

            _rows = rows;
            _from = from;
            _to = to;
            _set = set;
            OriginAtMargins = true;
            DefaultPageSettings.Margins = new Margins(40, 30, 30, 30);
            DocumentName = _rm.GetString("protocol");
            PrintPage += PrintProtocolDocument_PrintPage2;
            DefaultPageSettings.PaperSize.RawKind = 9;
            /*A4 - http://msdn.microsoft.com/en-us/library/system.drawing.printing.papersize.rawkind(v=vs.110).aspx */
        }

        public DataRow Protocol { get; set; }

        private void PrintProtocolDocument_PrintPage2(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;

            var titleFont = new Font(FontFamily.GenericSansSerif, 18F, FontStyle.Bold);
            var normalFont = new Font(FontFamily.GenericSansSerif, 10F, FontStyle.Regular);
            const int lineHeight = 21;
            float cenerPoint = e.PageBounds.Width / 2.0f;

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


            g.DrawLine(Pens.Black, 15, (3 * lineHeight), 750, (3 * lineHeight));

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
                    new PointF(20, lineHeight * (i + 3) + 3));
                g.DrawString(_rows[j].BlankNumber, normalFont, Brushes.Black,
                    new PointF(65, lineHeight * (i + 3) + 3));

                g.DrawString(_set.GetGroupTitle(_rows[j].GroupId), normalFont,
                    Brushes.Black, new PointF(185, lineHeight * (i + 3) + 3));

                g.DrawString(_rows[j].Date.ToShortDateString(), normalFont, Brushes.Black,
                    new PointF(370, lineHeight * (i + 3) + 3));
                string s = _rows[j].Result ? _rm.GetString("check") : _rm.GetString("uncheck");
                g.DrawString(s, normalFont, Brushes.Black, new PointF(450, lineHeight * (i + 3) + 3));
                g.DrawString(_set.GetShortMechanicName(_rows[j].MechanicId), normalFont,
                    Brushes.Black, new PointF(575, lineHeight * (i + 3) + 3));


                g.DrawLine(Pens.Black, 15, ((i + 4) * lineHeight), 750, ((i + 4) * lineHeight));
                g.DrawLine(Pens.Black, 15, (i + 3) * lineHeight, 15, (i + 4) * lineHeight);
                g.DrawLine(Pens.Black, 60, (i + 3) * lineHeight, 60, (i + 4) * lineHeight);
                g.DrawLine(Pens.Black, 180, (i + 3) * lineHeight, 180, (i + 4) * lineHeight);
                g.DrawLine(Pens.Black, 365, (i + 3) * lineHeight, 365, (i + 4) * lineHeight);
                g.DrawLine(Pens.Black, 445, (i + 3) * lineHeight, 445, (i + 4) * lineHeight);
                g.DrawLine(Pens.Black, 570, (i + 3) * lineHeight, 570, (i + 4) * lineHeight);
                g.DrawLine(Pens.Black, 750, (i + 3) * lineHeight, 750, (i + 4) * lineHeight);


                i++;
            }

            g.DrawLine(Pens.Black, 15, ((i + 4) * lineHeight), 750, ((i + 4) * lineHeight));
            //some statistics
            int check = (from dataRow in _rows
                         where dataRow.Result
                         select dataRow).ToArray().Length;
            int uncheck = _rows.Length - check;
            g.DrawString(_rm.GetString("check") + ": " + check, normalFont, Brushes.Black,
                new PointF(50, lineHeight * (i + 5) + 3));
            g.DrawString(_rm.GetString("uncheck") + ": " + uncheck, normalFont, Brushes.Black,
                new PointF(200, lineHeight * (i + 5) + 3));
            g.DrawString(_rm.GetString("total") + ": " + (check + uncheck), normalFont, Brushes.Black,
                new PointF(350, lineHeight * (i + 5) + 3));
        }


        private void PrintProtocolDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;

            var titleFont = new Font(FontFamily.GenericSansSerif, 18F, FontStyle.Bold);
            var boldFont = new Font(FontFamily.GenericSansSerif, 11F, FontStyle.Bold);
            var normalFont = new Font(FontFamily.GenericSansSerif, 10F, FontStyle.Regular);
            var bigFont = new Font(FontFamily.GenericSansSerif, 18F, FontStyle.Bold);
            var smallFont = new Font(FontFamily.GenericSansSerif, 7F, FontStyle.Regular);

            var norm = new Normatives();

            int LineHeight = 21;

            float cenerPoint = e.PageBounds.Width / 2.0f;

            var sf = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };


            //1 столбец - 450, 2 - 150, 3-200, 4 - 70

            g.DrawString(_rm.GetString("results") + _protocolRow.BlankNumber, titleFont, Brushes.Black,
                new PointF(cenerPoint, LineHeight), sf);

            g.DrawString(_rm.GetString("brakeSystem"), boldFont, Brushes.Black, new PointF(40F, 2 * LineHeight));
            DrawHeader(g, normalFont, 2 * LineHeight);


            for (int i = 0; i < 5; i++)
            {
                g.DrawString(norm[i], normalFont, Brushes.Black, new PointF(20F, (i + 3) * LineHeight));

                DrawPictorgam(i, g, (i + 3) * LineHeight, normalFont, i);
            }

            g.DrawString(norm[23], normalFont, Brushes.Black, new PointF(20F, 8 * LineHeight));
            DrawPictorgam(23, g, 8 * LineHeight, normalFont, 23);

            g.DrawString(norm[19], normalFont, Brushes.Black, new PointF(20F, 9 * LineHeight));
            DrawPictorgam(19, g, 9 * LineHeight, normalFont, 19);


            for (int i = 5; i < 7; i++)
            {
                g.DrawString(norm[i], normalFont, Brushes.Black, new PointF(20F, (i + 5) * LineHeight));

                DrawPictorgam(i, g, (i + 5) * LineHeight, normalFont, i);
            }


            g.DrawString(_rm.GetString("wheelSystem"), boldFont, Brushes.Black, new PointF(40F, 12 * LineHeight));
            DrawHeader(g, normalFont, 12 * LineHeight);

            g.DrawString(norm[7], normalFont, Brushes.Black, new PointF(20F, 13 * LineHeight));
            DrawPictorgam(7, g, 13 * LineHeight, normalFont, 7);


            g.DrawString(_rm.GetString("lightSystem"), boldFont, Brushes.Black, new PointF(40F, 14 * LineHeight));
            DrawHeader(g, normalFont, 14 * LineHeight);

            for (int i = 8; i < 12; i++)
            {
                g.DrawString(norm[i], normalFont, Brushes.Black, new PointF(20F, (i + 7) * LineHeight));
                DrawPictorgam(i, g, (i + 7) * LineHeight, normalFont, i);
            }

            g.DrawString(_rm.GetString("wheelAndTyres"), boldFont, Brushes.Black, new PointF(40F, 19 * LineHeight));
            DrawHeader(g, normalFont, 19 * LineHeight);

            g.DrawString(norm[12], normalFont, Brushes.Black, new PointF(20F, 20 * LineHeight));
            DrawPictorgam(12, g, 20 * LineHeight, normalFont, 12);


            g.DrawString(_rm.GetString("engineAndItsSystem"), boldFont, Brushes.Black, new PointF(40F, 21 * LineHeight));
            DrawHeader(g, normalFont, 21 * LineHeight);

            for (int i = 13; i < 17; i++)
            {
                g.DrawString(norm[i], normalFont, Brushes.Black, new PointF(20F, (i + 9) * LineHeight));
                DrawPictorgam(i, g, (i + 9) * LineHeight, normalFont, i);
            }


            g.DrawString(norm[17], normalFont, Brushes.Black, new PointF(20F, 26 * LineHeight));
            DrawPictorgam(17, g, 26 * LineHeight, normalFont, 17, true);

            g.DrawString(norm[18], normalFont, Brushes.Black, new PointF(20F, 27 * LineHeight));
            DrawPictorgam(18, g, 27 * LineHeight, normalFont, 18, true);


            g.DrawString(_rm.GetString("glass"), boldFont, Brushes.Black, new PointF(40F, 28 * LineHeight));
            DrawHeader(g, normalFont, 28 * LineHeight);

            for (int i = 20; i < 22; i++)
            {
                g.DrawString(norm[i], normalFont, Brushes.Black, new PointF(20F, (i + 9) * LineHeight));
                DrawPictorgam(i, g, (i + 9) * LineHeight, normalFont, i);
            }

            g.DrawString(_rm.GetString("noise"), boldFont, Brushes.Black, new PointF(40F, 31 * LineHeight));
            DrawHeader(g, normalFont, 31 * LineHeight);

            g.DrawString(norm[22], normalFont, Brushes.Black, new PointF(20F, 32 * LineHeight));
            DrawPictorgam(22, g, 32 * LineHeight, normalFont, 22);


            g.DrawString(_rm.GetString("GGBS"), boldFont, Brushes.Black, new PointF(40F, 33 * LineHeight));

            int gbo = _protocolRow.GBO;
            string stringGbo;
            switch (gbo)
            {
                case (int)Gbo.NotChecked:
                    stringGbo = _rm.GetString("notCheck");
                    break;
                case (int)Gbo.Germetical:
                    stringGbo = _rm.GetString("germ");
                    break;
                default:
                    stringGbo = _rm.GetString("nogerm");
                    break;
            }

            g.DrawString(stringGbo, boldFont, Brushes.Black, new PointF(660F, 33 * LineHeight),
                new StringFormat { Alignment = StringAlignment.Center });


            g.DrawString(_rm.GetString("visualCheck"), boldFont, Brushes.Black, new PointF(40F, 34 * LineHeight));

            string vslChk = _protocolRow.VisualCheck ? _rm.GetString("check") : _rm.GetString("uncheck");
            g.DrawString(vslChk, boldFont, Brushes.Black, new PointF(660F, 34.25f * LineHeight),
                new StringFormat { Alignment = StringAlignment.Center });
            g.DrawString(_rm.GetString("visualCheck2"), smallFont, Brushes.Black, new PointF(40F, 34.8f * LineHeight));

            g.DrawLine(Pens.Black, 15, (2 * LineHeight) - 3, 15, (35.5f * LineHeight) - 3);
            g.DrawLine(Pens.Black, 750, (2 * LineHeight) - 3, 750, (35.5f * LineHeight) - 3);
            g.DrawLine(Pens.Black, 450, (2 * LineHeight) - 3, 450, (33 * LineHeight) - 3);
            g.DrawLine(Pens.Black, 570, (2 * LineHeight) - 3, 570, (35.5f * LineHeight) - 3);
            g.DrawLine(Pens.Black, 650, (2 * LineHeight) - 3, 650, (33 * LineHeight) - 3);

            for (int i = 2; i < 35; i++)
            {
                g.DrawLine(Pens.Black, 15, (i * LineHeight) - 3, 750, (i * LineHeight) - 3);
            }
            g.DrawLine(Pens.Black, 15, (35.5f * LineHeight) - 3, 750, (35.5f * LineHeight) - 3);


            string s = _protocolRow.Result ? _rm.GetString("check") : _rm.GetString("uncheck");

            if (s != null)
            {
                string s1 = _rm.GetString("results2");

                if (s1 != null)
                {
                    g.DrawString(s1.ToUpper(), bigFont, Brushes.Black, new PointF(200, 37f * LineHeight), sf);
                    g.DrawString(s.ToUpper(), bigFont, Brushes.Black, new PointF(200, 39f * LineHeight), sf);
                    g.DrawRectangle(Pens.Black, 20, 36f * LineHeight, 370, 4 * LineHeight);
                }
            }

            g.DrawString(_rm.GetString("mechanic"), boldFont, Brushes.Black, new PointF(30, 40.5f * LineHeight));
            g.DrawString(_set.GetShortMechanicName(_protocolRow.MechanicId), boldFont,
                Brushes.Black, new PointF(165f, 42.5f * LineHeight), sf);
            g.DrawLine(Pens.Black, 20, 43 * LineHeight, 350, 43 * LineHeight);
            g.DrawString(_rm.GetString("FIO"), smallFont, Brushes.Black, new PointF(165, (44 * LineHeight) - 5), sf);


            g.DrawLine(Pens.Black, 20, 46 * LineHeight, 350, 46 * LineHeight);
            g.DrawString(_rm.GetString("signature"), smallFont, Brushes.Black, new PointF(165, (47 * LineHeight) - 5), sf);

            g.DrawString(_protocolRow.Date.ToShortDateString(), boldFont,
                Brushes.Black, new PointF(165f, 48.5f * LineHeight), sf);

            g.DrawLine(Pens.Black, 20, 49 * LineHeight, 350, 49 * LineHeight);
            g.DrawString(_rm.GetString("data"), smallFont, Brushes.Black, new PointF(165, (50 * LineHeight) - 5), sf);


            var techrect = new Rectangle(420, (36 * LineHeight), 320, LineHeight * 14);
            g.DrawRectangle(Pens.Black, techrect);
            g.DrawString(_rm.GetString("techphoto"), smallFont, Brushes.Black, techrect, sf);


            if (!_protocolRow.IsTechPhotoNull())
            {
                TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
                var b1 = (Bitmap)tc.ConvertFrom(_protocolRow.TechPhoto);
                if (b1 != null)
                {
                    DrawScaledArImage(b1, g, techrect);
                }
            }



            g.DrawLine(Pens.Black, 10, 51 * LineHeight, 750, 51 * LineHeight);


            g.DrawString(_rm.GetString("oboznach"), smallFont, Brushes.Black, new PointF(75, 52 * LineHeight));
            g.DrawString(" - " + _rm.GetString("check"), smallFont, Brushes.Black, new PointF(260, 52 * LineHeight));
            g.DrawString(" - " + _rm.GetString("uncheck"), smallFont, Brushes.Black, new PointF(460, 52 * LineHeight));
            g.DrawString(" - " + _rm.GetString("notCheck"), smallFont, Brushes.Black, new PointF(660, 52 * LineHeight));

            g.DrawImage(Resources.pass, 240, 52 * LineHeight - 2, 15, 15);
            g.DrawImage(Resources.fail, 440, 52 * LineHeight - 2, 15, 15);
            g.DrawImage(Resources.none, 640, 52 * LineHeight - 2, 15, 15);
        }

        /// <summary>
        /// Рисование изображения, вписанного в прямоугольник, с правильными пропорциями
        /// </summary>
        /// <param name="sourceImage">Изображение</param>
        /// <param name="context">Графический контекст</param>
        /// <param name="sourceRect">Прямоугольник, в который нужно вписать изображение</param>
        private static void DrawScaledArImage(Image sourceImage, Graphics context, Rectangle sourceRect)
        {
            int width = sourceImage.Width;
            int height = sourceImage.Height;
            double ratio;
            int destWidth;
            int destHeight;
            context.InterpolationMode = InterpolationMode.HighQualityBicubic;

            if (width > height)
            {
                ratio = (double) height/(double) width;
                destWidth = sourceRect.Width;
                destHeight = Convert.ToInt32(sourceRect.Height * ratio);
                context.DrawImage(sourceImage,
                    new Rectangle(
                        sourceRect.X,
                        sourceRect.Y + ((sourceRect.Height - destHeight) / 2),
                        destWidth, destHeight),
                    new Rectangle(0, 0, sourceImage.Width, sourceImage.Height),
                    GraphicsUnit.Pixel);
            }
            else
            {
                ratio = (double) width/(double) height;
                destWidth = Convert.ToInt32(sourceRect.Width * ratio);
                destHeight = sourceRect.Height;
                context.DrawImage(sourceImage,
                    new Rectangle(
                        sourceRect.X + ((sourceRect.Width - destWidth) / 2),
                        sourceRect.Y,
                        destWidth, destHeight),
                    new Rectangle(0, 0, sourceImage.Width, sourceImage.Height),
                    GraphicsUnit.Pixel);
            }
        }

        private void DrawPictorgam(int i, Graphics graphics, int height, Font normalFont, int ind, bool mode = false)
        {
            var sf = new StringFormat { Alignment = StringAlignment.Center };
            Normatives norms = new Normatives();


            NewVipAvtoSet.MesuresRow[] value =
                (from NewVipAvtoSet.MesuresRow item in _mesures where item.NormativeTag == i select item)
                    .ToArray();
            if (value.Length == 0)
            {
                graphics.DrawImage(Resources.none, 690, height, 15, 15);
            }
            else
            {
                if (mode)
                    graphics.DrawString(
                        value[0].Value + " (" + Math.Round(GetSmokeVal(value[0].Value), norms.DecimalPoints[ind]) + ")", normalFont, Brushes.Black, new PointF(610F, height), sf);
                else
                {
                    graphics.DrawString(value[0].Value.ToString(), normalFont, Brushes.Black, new PointF(610F, height), sf);
                }

                double minval = value[0].MinValue;
                double maxval = norms.HardNorms[ind];

                if (mode)
                    graphics.DrawString(
                        String.Format("[{0};{1}] ({2};{3})", minval, maxval,
                            Math.Round(GetSmokeVal(minval), norms.DecimalPoints[ind]),
                            Math.Round(GetSmokeVal(maxval), norms.DecimalPoints[ind])),
                        normalFont, Brushes.Black, new PointF(510F, height), sf);
                else
                {
                    graphics.DrawString(String.Format("[{0};{1}]", minval, maxval), normalFont, Brushes.Black,
                        new PointF(510F, height), sf);
                }


                if (minval <= value[0].Value && value[0].Value < maxval)
                {
                    graphics.DrawImage(Resources.pass, 690, height, 15, 15);
                }
                else
                {
                    graphics.DrawImage(Resources.fail, 690, height, 15, 15);
                }
            }
        }

        private double GetSmokeVal(double smokeVal)
        {
            return Math.Round((-1.0 / 0.43) * Math.Log(1.0 - (smokeVal / 100.0)), 3);
        }

        private void DrawHeader(Graphics g, Font normalFont, int lineHeight)
        {
            var sf = new StringFormat { Alignment = StringAlignment.Center };

            g.DrawString(_rm.GetString("value"), normalFont, Brushes.Black, new PointF(610F, lineHeight), sf);
            g.DrawString(_rm.GetString("zakluch"), normalFont, Brushes.Black, new PointF(700F, lineHeight), sf);
            g.DrawString(_rm.GetString("norm"), normalFont, Brushes.Black, new PointF(510F, lineHeight), sf);
        }
    }
}