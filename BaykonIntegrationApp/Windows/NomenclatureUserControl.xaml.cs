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
    /// Interaction logic for NomenclatureUserControl.xaml
    /// </summary>
    public partial class NomenclatureUserControl : UserControl
    {
        ObservableCollection<Nomenclature> list;
        public NomenclatureUserControl()
        {
            InitializeComponent();
            list = new ObservableCollection<Nomenclature>();

            using (var db = new Db.BaykonDbContext())
            {
                foreach (var item in db.Nomenclatures)
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
                Nomenclature nomenclature = new Nomenclature(val.Value);
                list.Add(nomenclature);
                nomenclature.Save();
            }
        }

        private void OnEditing(object sender, RoutedEventArgs e)
        {
            if (data.SelectedItem == null)
                return;
            Nomenclature nomenclature = list[data.SelectedIndex];
            InputValue val = new InputValue();
            val.Name = "Nomlanishi";
            val.Value = nomenclature.Name;
            InputDialog window = new InputDialog(val);
            if (window.ShowDialog() == true)
            {
                nomenclature.Name = val.Value;
                nomenclature.Update();
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
