using BaykonIntegrationApp.Models;
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
    /// Interaction logic for PackageViewAddWindow.xaml
    /// </summary>
    public partial class PackageViewAddWindow : Window
    {
        public PackageView PackageView { get; set; }
        public List<string> Types { get; set; }

        public PackageViewAddWindow()
        {
            InitializeComponent();
            Types = new List<string>();
            Types.Add("Quti");
            Types.Add("Qop");
            cmbItems.ItemsSource = Types;

            cmbItems.SelectedIndex = 0;

        }

        public PackageViewAddWindow(PackageView PackageView)
        {
            InitializeComponent();
            Types = new List<string>();
            Types.Add("Quti");
            Types.Add("Qop");
            cmbItems.ItemsSource = Types;

            cmbItems.SelectedIndex = 0;

            DataContext = PackageView;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;   
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
