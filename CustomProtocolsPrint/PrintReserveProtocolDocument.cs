using System.Drawing.Printing;

namespace CustomProtocolsPrint
{
    public class PrintReserveProtocolDocument : PrintDocument
    {
        public PrintReserveProtocolDocument()
        {
            OriginAtMargins = true;
            DefaultPageSettings.Margins = new Margins(40, 30, 30, 30);
            DocumentName = "protocol";
            PrintPage += PrintReserveProtocolDocument_PrintPage;
            DefaultPageSettings.PaperSize.RawKind = 9;
            /*A4 - http://msdn.microsoft.com/en-us/library/system.drawing.printing.papersize.rawkind(v=vs.110).aspx */

        }

        void PrintReserveProtocolDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            
        }
    }
}