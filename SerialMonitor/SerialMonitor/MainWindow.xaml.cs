using SerialMonitor.Model;
using SerialMonitor.Repository;
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
using System.Runtime.InteropServices;

namespace SerialMonitor
{
    public partial class MainWindow : Window
    {
        public static SerialMonitorModel serialMonitorModel = new SerialMonitorModel();

        CancellationTokenSource cancellationToken = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        public void ConsoleWindow_TextChanged(object sender, TextChangedEventArgs e)
        {
            //serialMonitorModel._consoleWindow = ConsoleWindow.Text;
            ConsoleWindow.CaretIndex = ConsoleWindow.Text.Length;
            ConsoleWindow.ScrollToEnd();
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

        public void StartMonitor_Click(object sender, RoutedEventArgs e)
        {
            Monitor();
        }

        private void StopMonitor_Click(object sender, RoutedEventArgs e)
        {
            cancellationToken.Cancel();
        }

        public async Task Monitor()
        {
            await Task.Run(() =>
            {
                bool isTrue = true;
                while (isTrue)
                {
                    string _line = InitSerial.StartSerialPort();
                    this.Dispatcher.Invoke(() =>
                    {
                        if (!(string.IsNullOrEmpty(_line)))
                        {
                            ConsoleWindow.Text += _line + "\n";
                        }
                    });
                    if (cancellationToken.Token.IsCancellationRequested)
                    {
                        isTrue = false;
                    }
                }
            }, cancellationToken.Token);
        }
    }
}
