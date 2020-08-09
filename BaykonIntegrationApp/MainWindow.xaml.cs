using System;
using System.Windows;
using System.ComponentModel;
using BaykonIntegrationApp.Service;
using System.Threading;
using BaykonIntegrationApp.WindowContexts;
using BaykonIntegrationApp.Windows;
using System.IO;
using System.Xml.Serialization;
using BaykonIntegrationApp.Models;
using System.Globalization;

namespace BaykonIntegrationApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    //[LicenseProvider(typeof(CommonLibrary.License.OwnLicenseProvider))]
    public partial class MainWindow : Window
    {
        ContextMainWindow context;

        public object ConnectSettinsWindow { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru-RU");
            //Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            Db.DBConnection.SetDefaultSettings();

            context = new ContextMainWindow();
            DataContext = context;
        }

        private void btnAdd_Nomen(object sender, RoutedEventArgs e)
        {
            try
            {
                InputValue value = new InputValue()
                {
                    Name = BaykonIntegrationApp.Resources.Localization.Language.Nomenklatura2
                };
                InputDialog dialog = new InputDialog(value);

                if (dialog.ShowDialog() == true)
                {
                    Nomenclature nomenclature = new Nomenclature(value.Value);
                    context.Nomens.Add(nomenclature);
                    nomenclature.Save();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void btnAdd_Smena(object sender, RoutedEventArgs e)
        {
            try
            {
                InputValue value = new InputValue()
                {
                    Name = BaykonIntegrationApp.Resources.Localization.Language.Smena2
                };
                InputDialog dialog = new InputDialog(value);

                if (dialog.ShowDialog() == true)
                {
                    Shift shift = new Shift(value.Value);
                    context.Shifts.Add(shift);
                    shift.Save();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void btnAdd_ViewBox(object sender, RoutedEventArgs e)
        {
            try
            {
                PackageView package = new PackageView();
                PackageViewAddWindow dialog = new PackageViewAddWindow(package);

                if (dialog.ShowDialog() == true)
                {

                    MessageBox.Show(package.Type.ToString());
                    context.PackageViews.Add(package);
                    package.Save();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void btnAdd_Lot(object sender, RoutedEventArgs e)
        {
            try
            {
                Lot lot = new Lot();

                LotAddWindow dialog = new LotAddWindow(lot);

                if (dialog.ShowDialog() == true)
                {
                    context.Lots.Add(lot);
                    lot.Save();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void btnAdd_ColorConus(object sender, RoutedEventArgs e)
        {
            try
            {
                InputValue value = new InputValue()
                {
                    Name = BaykonIntegrationApp.Resources.Localization.Language.Colorofconus2
                };
                InputDialog dialog = new InputDialog(value);

                if (dialog.ShowDialog() == true)
                {
                    ConeColour cone = new ConeColour(Name);
                    context.ConeColours.Add(cone);
                    cone.Save();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void btn_ConnectToCom(object sender, RoutedEventArgs e)
        {
            try
            {
                context.ConnectToCom();
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void btnOk(object sender, RoutedEventArgs e)
        {
            try
            {
                context.AddNew();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                context.AddNewAndPrint();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void btnPrintAll(object sender, RoutedEventArgs e)
        {
            try
            {
                context.PrintAll();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void mnuItemClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void mnuItemCreate(object sender, RoutedEventArgs e)
        {
            context.CreateData();
        }

        private void btnPrintSelected(object sender, RoutedEventArgs e)
        {
            try
            {
                context.PrintSelected();
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void mnuItemOpen(object sender, RoutedEventArgs e)
        {
            context.OpenDate();
        }

        private void btn_ConnectToPrinter(object sender, RoutedEventArgs e)
        {
            context.ConnectToPrinter();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            context.CloseExcel();
            context.CloseConnect();
        }

        private void mnuItemServices(object sender, RoutedEventArgs e)
        {
            context.ClickServices();
        }
    }
}
