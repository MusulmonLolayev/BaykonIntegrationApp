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
    /// Interaction logic for ChamgeUserControl.xaml
    /// </summary>
    public partial class ChamgeUserControl : UserControl
    {
        ObservableCollection<Shift> list;
        public ChamgeUserControl()
        {
            InitializeComponent();
            list = new ObservableCollection<Shift>();

            using (var db = new Db.BaykonDbContext())
            {
                foreach (var item in db.Shifts)
                {
                    list.Add(item);
                }
            }
            data.ItemsSource = list;
        }

        private void OnClickNew(object sender, RoutedEventArgs e)
        {
            InputValue val = new InputValue();
            val.Name = "Nomilanishi";
            InputDialog window = new InputDialog(val);
            if (window.ShowDialog() == true)
            {
                Shift Shift = new Shift(val.Value);
                list.Add(Shift);
                Shift.Save();
            }
        }

        private void OnEditing(object sender, RoutedEventArgs e)
        {
            if (data.SelectedItem == null)
                return;
            Shift Shift = list[data.SelectedIndex];
            InputValue val = new InputValue();
            val.Name = "Nomlanishi";
            val.Value = Shift.Name;
            InputDialog window = new InputDialog(val);
            if (window.ShowDialog() == true)
            {
                Shift.Name = val.Value;
                Shift.Update();
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
