using SerialMonitor.Model;
using SerialMonitor;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Net.Http.Headers;

namespace SerialMonitor.Repository
{
    public class InitSerial
    {
        private static readonly string _commPort = MainWindow.serialMonitorModel._commPort;
        private static readonly int _baudRate = Convert.ToInt32(MainWindow.serialMonitorModel._buadRate);
        private static readonly string _parityValue = MainWindow.serialMonitorModel._parity;
        private static readonly Parity _parity = (Parity)Enum.Parse(typeof(Parity), _parityValue);
        private static readonly int _dataBits = Convert.ToInt32(MainWindow.serialMonitorModel._dataBits);
        private static readonly string _stopBitsValue = MainWindow.serialMonitorModel._stopBits;
        private static readonly StopBits _stopBits = (StopBits)Enum.Parse(typeof(StopBits), _stopBitsValue);

        private static SerialPort _port = new SerialPort(portName: _commPort, baudRate: _baudRate, parity: _parity, dataBits: _dataBits, stopBits: _stopBits);

        public static string StartSerialPort()
        {
            try
            {
                string _line = ReadSerialPort(_port);
                return _line;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            
        }
        public static string ReadSerialPort(SerialPort port)
        {
            port.Open();
            string _line = port.ReadExisting();
            //while (string.IsNullOrEmpty(port.ReadExisting()))
            //{

            //}
            //while (true)
            //{
            //string _line = port.ReadLine();
            //if (!(string.IsNullOrEmpty(_line)))
            //{
            //    MainWindow.serialMonitorModel._consoleWindow = _line + "\n";
            //}
            //}
            port.Close();
            return _line;
        }
    }
}
