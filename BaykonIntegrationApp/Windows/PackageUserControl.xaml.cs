using BaykonIntegrationApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BaykonIntegrationApp.Windows
{
    /// <summary>
    /// Interaction logic for PackageUserControl.xaml
    /// </summary>
    public partial class PackageUserControl : UserControl
    {
        ObservableCollection<PackageView> list;
        public PackageUserControl()
        {
            InitializeComponent();
            list = new ObservableCollection<PackageView>();

            using (var db = new Db.BaykonDbContext())
            {
                foreach (var item in db.PackageViews)
                {
                    list.Add(item);
                }
            }
            data.ItemsSource = list;
        }

        private void OnClickNew(object sender, RoutedEventArgs e)
        {
            PackageView package = new PackageView();
            PackageViewAddWindow window = new PackageViewAddWindow(package);
            if (window.ShowDialog() == true)
            {
                list.Add(package);
                package.Save();
            }
        }

        private void OnEditing(object sender, RoutedEventArgs e)
        {
            if (data.SelectedItem == null)
                return;
            PackageView package = list[data.SelectedIndex];
            PackageViewAddWindow window = new PackageViewAddWindow(package);
            if (window.ShowDialog() == true)
            {
                package.Update();
            }
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            if (data.SelectedItem != null &&
                MessageBox.Show("O'chirmoqchimisiz", "O'chirish", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                list[data.SelectedIndex].Delete();
                list.RemoveAt(data.SelectedIndex);
            }
        }
    }
}
