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
    /// Interaction logic for LotUserControl.xaml
    /// </summary>
    public partial class LotUserControl : UserControl
    {
        ObservableCollection<Lot> list;
        public LotUserControl()
        {
            InitializeComponent();
            list = new ObservableCollection<Lot>();

            using (var db = new Db.BaykonDbContext())
            {
                foreach (var item in db.Lots)
                {
                    list.Add(item);
                }
            }
            data.ItemsSource = list;
        }

        private void OnClickNew(object sender, RoutedEventArgs e)
        {
            Lot lot = new Lot();
            LotAddWindow window = new LotAddWindow(lot);
            if (window.ShowDialog() == true)
            {
                list.Add(lot);
                lot.Save();
            }
        }

        private void OnEditing(object sender, RoutedEventArgs e)
        {
            if (data.SelectedItem == null)
                return;
            Lot lot = list[data.SelectedIndex];
            LotAddWindow window = new LotAddWindow(lot);
            if (window.ShowDialog() == true)
            {
                lot.Update();
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
