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
using System.Windows.Shapes;

namespace BaykonIntegrationApp.Windows
{
    /// <summary>
    /// Interaction logic for ConnectSettingsWindows.xaml
    /// </summary>
    public partial class PrinterSettingsWindows : Window
    {
        public string printerName { get; private set; }
        public PrinterSettingsWindows(List<string> AvailablePrinters)
        {
            InitializeComponent();

            cmbxPosrts.ItemsSource = AvailablePrinters;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cmbxPosrts.SelectedItem != null)
            {
                printerName = cmbxPosrts.SelectedItem as string;
                DialogResult = true;
            }
            else
            {
                MessageBox.Show(BaykonIntegrationApp.Resources.Localization.Language.ConnectError);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
