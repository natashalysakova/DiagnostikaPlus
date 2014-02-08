using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using adovipavto.Classes;
using adovipavto.Enums;

namespace adovipavto
{
    public partial class ProtocolReportForm : Form
    {
        private readonly bool _printNow;
        private PrintProtocolDocument document;

        private readonly ResourceManager rm = new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly());

        public ProtocolReportForm(DataRow protocolRow, DataRow[] mesures, bool printNow = false)
        {
            _printNow = printNow;

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);

            InitializeComponent();

            document = new PrintProtocolDocument(protocolRow, mesures);
            printPreviewControl1.Document = document;

            if (_printNow)
            {
                document.Print();
            }

            this.Text += protocolRow["BlankNumber"];

            this.ResizeEnd += ProtocolReportForm_ResizeEnd;
        }

        void ProtocolReportForm_ResizeEnd(object sender, EventArgs e)
        {
            double widthZoom = (double)printPreviewControl1.Width / printPreviewControl1.Document.DefaultPageSettings.PaperSize.Width;
            double heightZoom = (double)printPreviewControl1.Height / printPreviewControl1.Document.DefaultPageSettings.PaperSize.Height;
            printPreviewControl1.Zoom = widthZoom < heightZoom ? widthZoom : heightZoom;


        }

        private void ProtocolReportForm_Load(object sender, EventArgs e)
        {
            if (_printNow)
            {
                DialogResult = DialogResult.OK;
            }


            printPreviewControl1.MouseWheel += printPreviewControl1_MouseWheel;

            document.DefaultPageSettings.PaperSize.RawKind = 9;
            /*A4 - http://msdn.microsoft.com/en-us/library/system.drawing.printing.papersize.rawkind(v=vs.110).aspx */

            toolStripComboBox1.SelectedIndex = 2;

        }

        void printPreviewControl1_MouseWheel(object sender, MouseEventArgs e)
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
                if (printPreviewControl1.Zoom > 0.15)
                    printPreviewControl1.Zoom -= 0.15;
                else
                {
                    printPreviewControl1.Zoom = 0.1;
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                document.Print();
            }
        }






        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            document.Print();
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
            double zoom =
                Convert.ToDouble(
                    toolStripComboBox1.SelectedItem.ToString().Split(new[] { '%' }, StringSplitOptions.RemoveEmptyEntries)
                        [0]);
            printPreviewControl1.Zoom = zoom / 100;
        }

    }
}
