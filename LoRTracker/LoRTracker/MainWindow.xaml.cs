using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoRTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LoRConnector ClientConnector;

        public MainWindow()
        {
            InitializeComponent();

            ClientConnector = new LoRConnector(21337);
            ClientConnector.StateChanged += ClientStatusChanged;

            ClientConnector.Connect();
        }

        void ClientStatusChanged(LoRConnector sender, StateChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                lblGameConnectionStatus.Content = "Client: " + e.NextState.ToString();

                switch (e.NextState)
                {
                    case ClientConnectionStatus.CONNECTED:
                        lblGameConnectionStatus.Foreground = new SolidColorBrush(Colors.Green);
                        break;
                    case ClientConnectionStatus.CONNECTING:
                        lblGameConnectionStatus.Foreground = new SolidColorBrush(Colors.DarkOrange);
                        break;
                    case ClientConnectionStatus.DISCONNECTED:
                        lblGameConnectionStatus.Foreground = new SolidColorBrush(Colors.Red);
                        break;
                    case ClientConnectionStatus.UNKNOWN:
                        lblGameConnectionStatus.Foreground = new SolidColorBrush(Colors.Black);
                        break;
                }
            });
        }
    }
}
