using System.Drawing;
using System.Drawing.Printing;

namespace CustomProtocolsPrint
{
    public class PrintReserveProtocolDocument : PrintDocument
    {
        public Protocol Protocol;

        public PrintReserveProtocolDocument()
        {
            OriginAtMargins = true;
            DefaultPageSettings.Margins = new Margins(10, 10, 10, 10);
            DocumentName = "protocol";
            PrintPage += PrintReserveProtocolDocument_PrintPage;
            DefaultPageSettings.PaperSize.RawKind = 9;
            /*A4 - http://msdn.microsoft.com/en-us/library/system.drawing.printing.papersize.rawkind(v=vs.110).aspx */
        }

        void PrintReserveProtocolDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(Properties.Resources.prot,0,0,808,1152);

            Font font = new Font(FontFamily.GenericMonospace, 15, FontStyle.Regular);
            g.DrawString(Protocol.BlankNumber, font, Brushes.Black, 330,190);
            g.DrawString(Protocol.DocNumber, font, Brushes.Black, 260, 680);
        }
    }
}