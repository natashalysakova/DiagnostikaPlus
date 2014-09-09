using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using adovipavto.Enums;

namespace CustomProtocolsPrint
{
    public class Protocol
    {
        public string BlankNumber { get; set; }
        public string VIN { get; set; }
        public Category Category { get; set; }
        public string Model { get; set; }
        public string GosNumber { get; set; }
        public DateTime DateTime { get; set; }
        public string DocNumber { get; set; }
        public string Pereoborudovanie { get; set; }
        public string EcoLevel { get; set; }
        public DateTime NextDate { get; set; }
    }
}
