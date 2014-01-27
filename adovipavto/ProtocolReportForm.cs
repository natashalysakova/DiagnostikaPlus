﻿using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using adovipavto.Classes;

namespace adovipavto
{
    public partial class ProtocolReportForm : Form
    {
        private DataRow _protocolRow;
        private DataRow[] _mesures;
        private readonly bool _printNow;

        public ProtocolReportForm(DataRow protocolRow, DataRow[] mesures, bool printNow = false)
        {
            _printNow = printNow;
            _mesures = mesures;
            _protocolRow = protocolRow;

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);

            InitializeComponent();

            if (_printNow)
            {
                printDocument1.Print();
            }

            this.ResizeEnd += ProtocolReportForm_ResizeEnd;
        }

        void ProtocolReportForm_ResizeEnd(object sender, EventArgs e)
        {
            double widthZoom = (double)printPreviewControl1.Width / printPreviewControl1.Document.DefaultPageSettings.PaperSize.Width - 0.05;
            double heightZoom = (double)printPreviewControl1.Height / printPreviewControl1.Document.DefaultPageSettings.PaperSize.Height - 0.05;
            printPreviewControl1.Zoom = widthZoom < heightZoom ? widthZoom : heightZoom;


        }

        private void ProtocolReportForm_Load(object sender, EventArgs e)
        {
            if (_printNow)
            {
                DialogResult = DialogResult.OK;
            }

            this.Text += _protocolRow["BlankNumber"];

            printDocument1.DefaultPageSettings.PaperSize.RawKind = 9;
                /*A4 - http://msdn.microsoft.com/en-us/library/system.drawing.printing.papersize.rawkind(v=vs.110).aspx */
            printDocument1.DefaultPageSettings.Margins = new Margins(3, 3, 3, 3);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;

            Font titleFont = new Font(FontFamily.GenericSansSerif, 18F, FontStyle.Bold);
            Font boldFont = new Font(FontFamily.GenericSansSerif, 11F, FontStyle.Bold);
            Font normalFont = new Font(FontFamily.GenericSansSerif, 10F, FontStyle.Regular);
            Font bigFont = new Font(FontFamily.GenericSansSerif, 18F, FontStyle.Bold);
            Font smallFont = new Font(FontFamily.GenericSansSerif, 7F, FontStyle.Regular);
            
            int LineHeight = 21;

            float cenerPoint = e.PageBounds.Width / 2.0f;

            var sf = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };


            //1 столбец - 450, 2 - 150, 3-200, 4 - 70

            g.DrawString("Результаты измерений к бланку № " + _protocolRow["BlankNumber"], titleFont, Brushes.Black, new PointF(cenerPoint, LineHeight), sf);

            g.DrawString("Тромозная система", boldFont, Brushes.Black, new PointF(40F, 2 * LineHeight));
            DrawHeader(g, normalFont, 2 * LineHeight);


            for (int i = 0; i < 7; i++)
            {
                g.DrawString(Constants.NormativesTitles[i], normalFont, Brushes.Black, new PointF(20F, (i + 3) * LineHeight));

                DrawPictorgam(i, g, (i + 3) * LineHeight, normalFont);
            }

            g.DrawString("Рулевое управление", boldFont, Brushes.Black, new PointF(40F, 10 * LineHeight));
            DrawHeader(g, normalFont, 10 * LineHeight);

            g.DrawString(Constants.NormativesTitles[7], normalFont, Brushes.Black, new PointF(20F, (7 + 4) * LineHeight));
            DrawPictorgam(7, g, (7 + 4) * LineHeight, normalFont);


            g.DrawString("Внешние световые приборы", boldFont, Brushes.Black, new PointF(40F, 12 * LineHeight));
            DrawHeader(g, normalFont, 12 * LineHeight);

            for (int i = 8; i < 12; i++)
            {
                g.DrawString(Constants.NormativesTitles[i], normalFont, Brushes.Black, new PointF(20F, (i + 5) * LineHeight));
                DrawPictorgam(i, g, (i + 5) * LineHeight, normalFont);

            }

            g.DrawString("Колёса и шины", boldFont, Brushes.Black, new PointF(40F, 17 * LineHeight));
            DrawHeader(g, normalFont, 17 * LineHeight);

            g.DrawString(Constants.NormativesTitles[12], normalFont, Brushes.Black, new PointF(20F, (12 + 6) * LineHeight));
            DrawPictorgam(12, g, (12 + 6) * LineHeight, normalFont);


            g.DrawString("Двигатель и его системы", boldFont, Brushes.Black, new PointF(40F, 19 * LineHeight));
            DrawHeader(g, normalFont, 19 * LineHeight);

            for (int i = 13; i < 21; i++)
            {
                g.DrawString(Constants.NormativesTitles[i], normalFont, Brushes.Black, new PointF(20F, (i + 7) * LineHeight));
                DrawPictorgam(i, g, (i + 7) * LineHeight, normalFont);

            }

            g.DrawString("Светопропускание стекол", boldFont, Brushes.Black, new PointF(40F, 28 * LineHeight));
            DrawHeader(g, normalFont, 28 * LineHeight);

            for (int i = 21; i < 23; i++)
            {
                g.DrawString(Constants.NormativesTitles[i], normalFont, Brushes.Black, new PointF(20F, (i + 8) * LineHeight));
                DrawPictorgam(i, g, (i + 8) * LineHeight, normalFont);

            }

            g.DrawString("Внешний шум АТС", boldFont, Brushes.Black, new PointF(40F, 31 * LineHeight));
            DrawHeader(g, normalFont, 31 * LineHeight);

            g.DrawString(Constants.NormativesTitles[23], normalFont, Brushes.Black, new PointF(20F, 32 * LineHeight));
            DrawPictorgam(23, g, 32 * LineHeight, normalFont);


            g.DrawString("Визуальные проверки на соответствие ГОСТ, ДСТУ", boldFont, Brushes.Black, new PointF(40F, 33 * LineHeight));

            string vslChk = (bool) _protocolRow["VisualCheck"] ? "Соответствует" : "Не соответствует";
            g.DrawString(vslChk, boldFont, Brushes.Black, new PointF(600F, 33 * LineHeight), new StringFormat(){Alignment = StringAlignment.Center});


            g.DrawLine(Pens.Black, 15, (2*LineHeight)-3, 15, (34*LineHeight)-3);
            g.DrawLine(Pens.Black, 750, (2 * LineHeight)-3, 750, (34 * LineHeight)-3);
            g.DrawLine(Pens.Black, 450, (2 * LineHeight) - 3, 450, (34 * LineHeight) - 3);
            g.DrawLine(Pens.Black, 530, (2 * LineHeight) - 3, 530, (33 * LineHeight) - 3);
            g.DrawLine(Pens.Black, 650, (2 * LineHeight) - 3, 650, (33 * LineHeight) - 3);

            for (int i = 2; i < 35; i++)
            {
                g.DrawLine(Pens.Black, 15, (i * LineHeight)-3, 750, (i * LineHeight)-3);
            }




            g.DrawString("Результат проверки: ".ToUpper() + ((bool)_protocolRow["Result"] ? "Соответствует" : "Не соответствует").ToUpper(), bigFont, Brushes.Black, new PointF(375, 37 * LineHeight), sf);

            g.DrawString("Механик-контролёр", boldFont, Brushes.Black, new PointF(30, 40 * LineHeight));
            g.DrawString(Program.VipAvtoDataSet.GetShortMechanicName((int) _protocolRow["IDMechanic"]), boldFont,
                Brushes.Black, new PointF(165f, 42.5f*LineHeight), sf);
            g.DrawLine(Pens.Black, 20, 43 * LineHeight, 350, 43 * LineHeight);
            g.DrawString("Ф.И.О.", smallFont, Brushes.Black, new PointF(165, (44 * LineHeight) - 5), sf);


            g.DrawLine(Pens.Black, 20, 46 * LineHeight, 350, 46 * LineHeight);
            g.DrawString("Подпись", smallFont, Brushes.Black, new PointF(165, (47 * LineHeight) - 5), sf);

            g.DrawString(((DateTime)_protocolRow["Date"]).ToShortDateString(), boldFont,
    Brushes.Black, new PointF(165f, 48.5f * LineHeight), sf);

            g.DrawLine(Pens.Black, 20, 49 * LineHeight, 350, 49 * LineHeight);
            g.DrawString("Дата", smallFont, Brushes.Black, new PointF(165, (50 * LineHeight) - 5), sf);

            var photorect = new Rectangle(380, 40*LineHeight, 160, LineHeight*10);
            g.DrawRectangle(Pens.Black, photorect);
            g.DrawString("Фото", smallFont, Brushes.Black, photorect , sf);

            if (_protocolRow["CarPhoto"] != null)
            {
                string path = _protocolRow["CarPhoto"].ToString();
                if(File.Exists(path))
                    g.DrawImage(new Bitmap(path), photorect);
            }


            var techrect = new Rectangle(560, 40*LineHeight, 160, LineHeight*10);
            g.DrawRectangle(Pens.Black, techrect);
            g.DrawString("Техпаспорт", smallFont, Brushes.Black, techrect, sf);
            if (_protocolRow["TechPhoto"] != null)
            {
                string path = _protocolRow["TechPhoto"].ToString();
                if (File.Exists(path))
                    g.DrawImage(new Bitmap(path), techrect);
            }



            g.DrawLine(Pens.Black, 10, 51 * LineHeight, 810, 51 * LineHeight);

            g.DrawString("Обозначения\t\t\t\t - соответствует\t\t\t\t - не соответствует\t\t\t - не проверялось", smallFont, Brushes.Black, new PointF(75, 52 * LineHeight));
            g.DrawImage(Properties.Resources.pass, 240, (int)(52 * LineHeight) - 2, 15, 15);
            g.DrawImage(Properties.Resources.fail, 440, (int)(52 * LineHeight) - 2, 15, 15);
            g.DrawImage(Properties.Resources.none, 640, (int)(52 * LineHeight) - 2, 15, 15);

        }

        private static void DrawHeader(Graphics g, Font normalFont, int lineHeight)
        {
            StringFormat sf = new StringFormat() { Alignment = StringAlignment.Center };

            g.DrawString("Значение", normalFont, Brushes.Black, new PointF(490F, lineHeight), sf);
            g.DrawString("Норматив", normalFont, Brushes.Black, new PointF(590F, lineHeight), sf);
            g.DrawString("Заключение", normalFont, Brushes.Black, new PointF(700F, lineHeight), sf);
        }

        private void DrawPictorgam(int i, Graphics g, int height , Font normalFont)
        {
            StringFormat sf = new StringFormat(){Alignment = StringAlignment.Center};

            double[] value =
                (from DataRow item in _mesures where (int) item["NormativeID"] == i select (double) item["Value"])
                    .ToArray();
            if (value.Length == 0)
            {
                g.DrawImage(Properties.Resources.none, 700, height+2, 15, 15);
            }
            else
            {
                g.DrawString(value[0].ToString(), normalFont, Brushes.Black, new PointF(490F, height),sf);

                double minval =
                    (double) (from DataRow item in Program.VipAvtoDataSet.Tables[Constants.NormativesTableName].Rows
                        where (int) item["IDGroup"] == (int) _protocolRow["IDGroup"] &&
                              (int) item["Title"] == i
                        select item["MinValue"]).ToArray()[0];
                double maxval =
                    (double) (from DataRow item in Program.VipAvtoDataSet.Tables[Constants.NormativesTableName].Rows
                        where (int) item["IDGroup"] == (int) _protocolRow["IDGroup"] &&
                              (int) item["Title"] == i
                        select item["MaxValue"]).ToArray()[0];

                g.DrawString(String.Format("[{0};{1}]", minval,maxval), normalFont, Brushes.Black, new PointF(590F, height), sf);


                if (minval <= value[0] && value[0] < maxval)
                {
                    g.DrawImage(Properties.Resources.pass, 700, height + 2, 15, 15);
                }
                else
                {
                    g.DrawImage(Properties.Resources.fail, 700, height + 2, 15, 15);
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void ProtocolReportForm_Resize(object sender, EventArgs e)
        {
            ProtocolReportForm_ResizeEnd(null, null);
        }

    }
}
