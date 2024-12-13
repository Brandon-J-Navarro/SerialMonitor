using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialMonitor.Model
{
    public class SerialMonitorModel
    {
        public string _consoleWindow { get; set; }
        public string _commPort { get; set; }
        public string _buadRate { get; set; }
        public string _parity { get; set; }
        public string _stopBits { get; set; }
        public string _dataBits { get; set; }
    }
}
