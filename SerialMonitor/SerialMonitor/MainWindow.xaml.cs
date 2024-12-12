using SerialMonitor.Model;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Ports;

namespace SerialMonitor
{
    public partial class MainWindow : Window
    {
        private static SerialMonitorModel serialMonitorModel = new SerialMonitorModel();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConsoleWindow_TextChanged(object sender, TextChangedEventArgs e)
        {
            serialMonitorModel._consoleWindow = ConsoleWindow.Text;
        }

        private void CommPort_TextChanged(object sender, TextChangedEventArgs e)
        {
            serialMonitorModel._commPort = CommPort.Text;
        }

        private void BaudRate_TextChanged(object sender, TextChangedEventArgs e)
        {
            serialMonitorModel._buadRate = BaudRate.Text;
        }

        private void DataBits_TextChanged(object sender, TextChangedEventArgs e)
        {
            serialMonitorModel._dataBits = DataBits.Text;
        }

        private void StopBits_TextChanged(object sender, TextChangedEventArgs e)
        {
            serialMonitorModel._stopBits = StopBits.Text;
        }

        private void Parity_TextChanged(object sender, TextChangedEventArgs e)
        {
            serialMonitorModel._parity = Parity.Text;
        }

        private void StartMonitor_Click(object sender, RoutedEventArgs e)
        {
            StartSerialPort(_port);
        }

            private static readonly string _commPort = serialMonitorModel._commPort;
            private static readonly int _baudRate = Convert.ToInt32(serialMonitorModel._buadRate);
            private static readonly string _parityValue = serialMonitorModel._parity;
            private static readonly Parity _parity = (Parity)Enum.Parse(typeof(Parity), _parityValue);
            private static readonly int _dataBits = Convert.ToInt32(serialMonitorModel._dataBits);
            private static readonly string _stopBitsValue = serialMonitorModel._stopBits;
            private static readonly StopBits _stopBits = (StopBits)Enum.Parse(typeof(StopBits), _stopBitsValue);

            private static SerialPort _port = new SerialPort(portName: _commPort, baudRate: _baudRate, parity: _parity, dataBits: _dataBits, stopBits: _stopBits);

        static void StartSerialPort(SerialPort _port)
        {

            bool _loop = false;
            while (_loop == false)
            {
                try
                {
                    _loop = ReadSerialPort(_port);
                    if (_loop)
                    {
                        StartSerialPort(_port);
                    }
                }
                catch (Exception ex)
                {
                    serialMonitorModel._consoleWindow = ex.ToString() + "\n";
                }
            }
        }
        public static bool ReadSerialPort(SerialPort port)
        {
            bool _condition = false;
            port.Open();
            while (_condition == false)
            {
                string _line = port.ReadLine();
                serialMonitorModel._consoleWindow = _line;
                if (_line == "I'm down.\r")
                {
                    _condition = true;
                }
            }
            port.Close();
            return _condition;
        }
    }
}
