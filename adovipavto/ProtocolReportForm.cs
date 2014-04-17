using System;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using adovipavto.Classes;

namespace adovipavto
{
    public sealed partial class ProtocolReportForm : Form
    {
        private readonly NewVipAvtoSet _set;
        private readonly bool _printNow;
        private readonly PrintProtocolDocument _document;

        public ProtocolReportForm(NewVipAvtoSet.ProtocolsRow protocolRow, NewVipAvtoSet.MesuresRow[] mesures, NewVipAvtoSet set, bool printNow = false)
        {
            _set = set;
            _printNow = printNow;

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Instance.Language);

            InitializeComponent();

            _document = new PrintProtocolDocument(protocolRow, mesures, _set);
            printPreviewControl1.Document = _document;

            if (_printNow)
            {
                _document.Print();
            }

            Text += protocolRow.BlankNumber;

            ResizeEnd += ProtocolReportForm_ResizeEnd;
        }

        private void ProtocolReportForm_ResizeEnd(object sender, EventArgs e)
        {
            try
            {
                double widthZoom = (double) printPreviewControl1.Width/
                                   printPreviewControl1.Document.DefaultPageSettings.PaperSize.Width;
                double heightZoom = (double) printPreviewControl1.Height/
                                    printPreviewControl1.Document.DefaultPageSettings.PaperSize.Height;
                printPreviewControl1.Zoom = widthZoom < heightZoom ? widthZoom : heightZoom;
            }
            catch
            {
            }
        }

        private void ProtocolReportForm_Load(object sender, EventArgs e)
        {
            if (_printNow)
            {
                DialogResult = DialogResult.OK;
            }


            printPreviewControl1.MouseWheel += printPreviewControl1_MouseWheel;

            _document.DefaultPageSettings.PaperSize.RawKind = 9;
            /*A4 - http://msdn.microsoft.com/en-us/library/system.drawing.printing.papersize.rawkind(v=vs.110).aspx */

            toolStripComboBox1.SelectedIndex = 2;
        }

        private void printPreviewControl1_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Delta > 0)
                    if (printPreviewControl1.Zoom < 2.85)
                        printPreviewControl1.Zoom += 0.15;
                    else
                    {
                        printPreviewControl1.Zoom = 3;
                    }
                else
                {
                    if (printPreviewControl1.Zoom > 0.25)
                        printPreviewControl1.Zoom -= 0.15;
                    else
                    {
                        printPreviewControl1.Zoom = 0.1;
                    }
                }
            }
            catch
            {
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            printDialog1.Document = _document;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                _document.Print();
            }
        }


        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            _document.Print();
        }

        private void ProtocolReportForm_Resize(object sender, EventArgs e)
        {
            ProtocolReportForm_ResizeEnd(null, null);
        }

        private void printPreviewControl1_Click(object sender, EventArgs e)
        {
            printPreviewControl1.Focus();
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                double zoom =
                    Convert.ToDouble(
                        toolStripComboBox1.SelectedItem.ToString().Split(new[] {'%'}, StringSplitOptions.RemoveEmptyEntries)
                            [0]);
                printPreviewControl1.Zoom = zoom/100;
            }
            catch
            {
            }
        }
    }
}