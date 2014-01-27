using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace adovipavto.Classes
{
    class ToolStripMaskedTextBox : ToolStripControlHost
    {
        public static MaskedTextBox Instance;

        public ToolStripMaskedTextBox():base(Instance)
        {
            Instance = new MaskedTextBox();
        }


    }
}
