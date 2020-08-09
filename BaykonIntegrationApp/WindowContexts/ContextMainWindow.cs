using BaykonIntegrationApp.Models;
using BaykonIntegrationApp.Service;
using BaykonIntegrationApp.Windows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;
using ZXing;
using System.Drawing.Printing;
using System.Runtime.CompilerServices;
using BaykonIntegrationApp.Db;
using ZXing.Common;

namespace BaykonIntegrationApp.WindowContexts
{
    public class ContextMainWindow : INotifyPropertyChanged
    {
        private bool IsOpen = false;
        ScaleMachine machine;
        public string COM_Name { get; set; }
        public ObservableCollection<DataModel> list { get; set; }

        private DateTime selDate;
        public DateTime Date
        {
            get { return selDate; }
            set { selDate = value; ChangeStateOpenOrCrateExcel(); }
        }

        private PackageView selPackageView;
        public PackageView SelPackageView
        {
            get { return selPackageView; }
            set { selPackageView = value; OnPropertyChanged("SelPackageView"); }
        }

        public ObservableCollection<PackageView> PackageViews { get; set; }

        public ConeColour SelConeColour { get; set; }

        public ObservableCollection<ConeColour> ConeColours { get; set; }

        private Lot selLot;
        public Lot SelLot
        {
            get { return selLot; }
            set
            {
                if (selLot != null)
                    selLot.Update();
                selLot = value;
                OnPropertyChanged("SelLot");
            }
        }

        public ObservableCollection<Lot> Lots { get; set; }

        private Nomenclature selNomen;
        public Nomenclature SelNomen
        {
            get { return selNomen; }
            set { selNomen = value; ChangeStateOpenOrCrateExcel(); }
        }

        public ObservableCollection<Nomenclature> Nomens { get; set; }

        private Shift selShift;
        public Shift SelShift
        {
            get { return selShift; }
            set { selShift = value; ChangeStateOpenOrCrateExcel(); }
        }

        public ObservableCollection<Shift> Shifts { get; set; }

        public int PrintRow { get; set; }

        public double MeanWeight { get; set; }

        public double StandartWeight { get; set; }

        public bool IsStandart { get; set; }

        private string scaleTxt;
        public string ScaleTxt
        {
            get { return scaleTxt; }
            set { scaleTxt = value; OnPropertyChanged("ScaleTxt"); }
        }

        //private double weightBox;
        //public double WeightBox
        //{
        //    get { return weightBox; }
        //    set { weightBox = value;  OnPropertyChanged("WeightBox"); }
        //}

        public ContextMainWindow()
        {
            excel = new Excel.Application();

            IsOpen = false;

            machine = new ScaleMachine();
            machine.worker.ProgressChanged += ProgressChanged;
            printer = new System.Drawing.Printing.PrintDocument();
            printer.PrintPage += PrintPage;

            Date = DateTime.Today;

            init();

            ScaleTxt = CommonLibrary.Resources.Localization.Language.NotСonnected;
        }

        private void init()
        {
            // init
            using (var db = new BaykonDbContext())
            {
                if (Nomens == null)
                    Nomens = new ObservableCollection<Nomenclature>();
                else
                    Nomens.Clear();
                db.Nomenclatures.ForEach(x => { Nomens.Add(x); });
                SelNomen = Nomens[0];
                OnPropertyChanged("SelNomen");

                if (Shifts == null)
                    Shifts = new ObservableCollection<Shift>();
                else
                    Shifts.Clear();
                db.Shifts.ForEach(x => { Shifts.Add(x); });
                SelShift = Shifts[0];
                OnPropertyChanged("SelShift");

                if (PackageViews == null)
                    PackageViews = new ObservableCollection<PackageView>();
                else
                    PackageViews.Clear();
                db.PackageViews.ForEach(x => { PackageViews.Add(x); });
                SelPackageView = PackageViews[0];
                OnPropertyChanged("SelPackageView");

                if (ConeColours == null)
                    ConeColours = new ObservableCollection<ConeColour>();
                else
                    ConeColours.Clear();
                db.ConeColours.ForEach(x => { ConeColours.Add(x); });
                SelConeColour = ConeColours[0];
                OnPropertyChanged("SelConeColour");

                if (Lots == null)
                    Lots = new ObservableCollection<Lot>();
                else
                    Lots.Clear();
                db.Lots.ForEach(x => { Lots.Add(x); });
                SelLot = Lots[0];

                list = new ObservableCollection<DataModel>();
            }
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ScaleTxt = e.UserState.ToString();
            //ScaleTxt = (string)sender;
        }
        
        Excel.Application excel;
        Excel.Workbook wb;
        Excel._Worksheet ws;
        public string Printer_Name;

        public void CreateData()
        {
            try
            {
                string file = Directory.GetCurrentDirectory() + @"\Templates\Temp1.xlsx";

                excel = new Excel.Application();
                wb = excel.Workbooks.Open(file);
                ws = excel.Sheets[1] as Excel.Worksheet;


                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = CommonLibrary.Resources.Localization.Language.ExcelFilesDialogs;
                if (dialog.ShowDialog() == true)
                {
                    list.Clear();
                    wb.SaveAs(dialog.FileName);
                    excel.Visible = true;
                    IsOpen = true;
                }
                else
                {
                    wb.Close();
                    excel.Quit();
                    wb.Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        public void OpenDate()
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = CommonLibrary.Resources.Localization.Language.ExcelFilesDialogs;
                if (dialog.ShowDialog() == true)
                {
                    excel = new Excel.Application();
                    wb = excel.Workbooks.Open(dialog.FileName);
                    ws = excel.Sheets[1] as Excel.Worksheet;

                    excel.Visible = true;
                    IsOpen = true;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        public void ConnectToPrinter()
        {
            try
            {
                List<string> printers = new List<string>();
                foreach(string printer in PrinterSettings.InstalledPrinters)
                {
                    printers.Add(printer);
                }
                PrinterSettingsWindows window = new PrinterSettingsWindows(printers);
                if (window.ShowDialog() == true)
                {
                    printer.PrinterSettings.PrinterName = window.printerName;
                    if (printer.PrinterSettings.IsValid && printer.PrinterSettings.IsDirectPrintingSupported(System.Drawing.Imaging.ImageFormat.Bmp))
                    {
                        Printer_Name = window.printerName;
                    }
                    //else
                    //    throw new InvalidPrinterException(printer.PrinterSettings);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        public void ClickServices()
        {
            try
            {
                ServicesWindow window = new ServicesWindow();
                window.ShowDialog();
                init();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        public void ConnectToCom()
        {
            try
            {
                ConnectSettingsWindows window = new ConnectSettingsWindows(machine.AvailablePorts);
                if (window.ShowDialog() == true)
                {
                    machine.OpenAndRun(window.portName);
                    if (machine.IsConnected)
                    {
                        COM_Name = window.portName;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        public void AddNew()
        {
            //Testing();
            if (IsOpen && machine.IsConnected)
            {
                int excel_row = ws.UsedRange.Rows.Count + 1;

                //ws.Cells[1, 2] = SelNomen.Name;
                //ws.Cells[2, 2] = SelShift.Name;
                double d;

                string text = scaleTxt;
                text = text.Substring(0, text.Length - 2).Replace(".", ",");
                //text = "15";

                if (double.TryParse(text, out d))
                {
                    //MessageBox.Show(d.ToString());

                    //list.Insert(0, new DataModel(SelNomen, SelShift, list.Count + 1, Date, SelViewBox, SelColorCone, CurrentNumberBox++, SelLot, WeightBox, d));
                    ws.Cells[excel_row, 1] = DateToString(selDate);
                    ws.Cells[excel_row, 2] = SelNomen.Name;
                    ws.Cells[excel_row, 3] = selShift.Name;
                    ws.Cells[excel_row, 4] = SelPackageView.Name;
                    ws.Cells[excel_row, 5] = SelConeColour.Name;
                    ws.Cells[excel_row, 6] = SelLot.LastNumber;
                    SelLot.LastNumber++;
                    ws.Cells[excel_row, 7] = SelLot.Name;
                    ws.Cells[excel_row, 8] = StandartWeight;
                    ws.Cells[excel_row, 9] = selPackageView.Weight;
                    ws.Cells[excel_row, 10] = d;

                    wb.Save();

                    DBConnection.SqlQuery(
                        $"Insert Into Data" +
                        $"(Date, Nomenclature, Shift, Package, Cone, Number, Lot, StandartWeight, PackageWeight, WeightBrutto)" +
                        $"Values (" +
                        $"'{DateToString(selDate)}', " +
                        $"'{selNomen.Name}', " +
                        $"'{selShift.Name}', " +
                        $"'{selPackageView.Name}', " +
                        $"'{SelConeColour.Name}', " +
                        $"{selLot.LastNumber}, " +
                        $"'{selLot.Name}', " +
                        $"{StandartWeight.ToString().Replace(',', '.')}, " +
                        $"{selPackageView.Weight.ToString().Replace(',', '.')}, " +
                        $"{d.ToString().Replace(',', '.')}" +
                        $")");
                }
                else
                    MessageBox.Show(Resources.Localization.Language.ErrorScale);
            }
            else
            {
                MessageBox.Show(CommonLibrary.Resources.Localization.Language.NotСonnected);
            }
        }

        private void Testing()
        {
            IsOpen = true;
            scaleTxt = "15";
        }

        private string DateToString(DateTime date)
        {
            return date.Day + "." + date.Month + "." + date.Year;
        }

        public void PrintSelected()
        {
            Print(GetDataFromExcel(PrintRow));
        }

        public void AddNewAndPrint()
        {
            AddNew();
            Print(GetDataFromExcel(ws.UsedRange.Rows.Count));
        }

        PrintDocument printer;
        DataModel DataModelitem;

        public void Print(DataModel item)
        {
            DataModelitem = item;
            //if (!printer.PrinterSettings.IsValid)
            //{
                printer.Print();
            //}
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                Bitmap bitmap = (Bitmap)Image.FromFile(Directory.GetCurrentDirectory() + @"\Templates\print.png");
                List<Pair> pairs = new List<Pair>();

                double netto = 0;
                double gross = 0;

                if (IsStandart)
                {
                    netto = StandartWeight;
                    gross = netto + selPackageView.Weight;
                }
                else
                {
                    netto = DataModelitem.WeightBrutto - selPackageView.Weight;
                    gross = DataModelitem.WeightBrutto;
                }

                pairs.Add(new Pair()
                {
                    //Text = BaykonIntegrationApp.Resources.Localization.Language.Nomenklatura2,
                    Text = "Material",
                    Value = "Cotton 100%",
                });
                pairs.Add(new Pair()
                {
                    //Text = BaykonIntegrationApp.Resources.Localization.Language.Nomenklatura2,
                    Text = "Lot №",
                    Value = DataModelitem.Lot.Name,
                });
                pairs.Add(new Pair()
                {
                    //Text = BaykonIntegrationApp.Resources.Localization.Language.Smena2,
                    Text = "Count (Ne)",
                    Value = DataModelitem.Nomenclature.Name,
                });

                pairs.Add(new Pair()
                {
                    //Text = BaykonIntegrationApp.Resources.Localization.Language.ViewOfBox,
                    Text = "Bag №",
                    Value = DataModelitem.Lot.LastNumber.ToString(),
                });

                if (SelPackageView.Type == 0)
                {
                    pairs.Add(new Pair()
                    {
                        //Text = BaykonIntegrationApp.Resources.Localization.Language.ViewOfBox,
                        Text = "Cones/Box",
                        Value = DataModelitem.PackageView.Value.ToString(),
                    });
                }
                else
                {
                    pairs.Add(new Pair()
                    {
                        //Text = BaykonIntegrationApp.Resources.Localization.Language.ViewOfBox,
                        Text = "Cones/Bag",
                        Value = DataModelitem.PackageView.Value.ToString(),
                    });
                }

                pairs.Add(new Pair()
                {
                    //Text = BaykonIntegrationApp.Resources.Localization.Language.Conecolor,
                    Text = "Net Wt(Kgs)",
                    Value = netto.ToString(),
                });

                pairs.Add(new Pair()
                {
                    //Text = BaykonIntegrationApp.Resources.Localization.Language.Lot2,
                    Text = "Gross Wt(Kgs)",
                    Value = gross.ToString(),
                });
                pairs.Add(new Pair()
                {
                    //Text = BaykonIntegrationApp.Resources.Localization.Language.BoxNumber,
                    Text = "Date & Shift",
                    Value = DataModelitem.Date.Day + "." + DataModelitem.Date.Month + "." + DataModelitem.Date.Year + " " + DataModelitem.Shift.Name,
                });
                
                int col1 = 15;
                int col2 = 140;

                int row = 50;
                int diff = 22;

                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    using (Font timesFont = new Font("Times New Roman", 12))
                    {
                        pairs.ForEach(x =>
                        {
                            PointF first = new PointF(col1, row);
                            PointF second = new PointF(col2, row);

                            graphics.DrawString(x.Text, timesFont, Brushes.Black, first);
                            graphics.DrawString(x.Value, timesFont, Brushes.Black, second);

                            row += diff;
                        });
                    }

                    ZXing.QrCode.QrCodeEncodingOptions opt = new ZXing.QrCode.QrCodeEncodingOptions
                    {
                        CharacterSet = "utf-8",
                        Height = 112,
                        Width = 112,
                        Margin = 0
                    };
                    IBarcodeWriter writer = new BarcodeWriter
                    {
                        Format = BarcodeFormat.QR_CODE,
                        Options = opt
                    };

                    BitMatrix matrix = writer.Encode(DataModelitem.ToString());

                    Bitmap image = BitMatrixToBitmap(matrix);

                    PointF first1 = new PointF(110, row - 5);

                    graphics.DrawImage(image, first1);
                }

                System.Drawing.Point point = new System.Drawing.Point(1, 1);
                e.Graphics.DrawImage(bitmap, point);

                //bitmap.Save("D:\\print.png");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private Bitmap BitMatrixToBitmap(BitMatrix matrix)
        {
            Bitmap image = new Bitmap(matrix.Width, matrix.Height);
            for (int i = 0; i < matrix.Width; i++)
            {
                for (int j = 0; j < matrix.Height; j++)
                {
                    if (matrix[i, j])
                        image.SetPixel(i, j, Color.Black);
                    else
                        image.SetPixel(i, j, Color.White);
                }
            }
            return image;
        }

        public DataModel GetDataFromExcel(int row, bool IsAll = false)
        {
            DataModel res = new DataModel();
            if (ws.UsedRange.Rows.Count < row)
            {
                return null;
            }
            string str = "";
            DateTime date = DateTime.Today;
            Excel.Range range;
            range = ws.Cells[row, 1] as Excel.Range;
            if (range.get_Value(System.Type.Missing) != null
                && (str = range.get_Value(System.Type.Missing).ToString()) != null
                && DateTime.TryParse(str, out date))
            {
                res.Date = date;
            }
            else
            {
                throw new System.FormatException(CommonLibrary.Resources.Localization.Language.Error);
            }
            range = ws.Cells[row, 2] as Excel.Range;
            if (range.get_Value(Type.Missing) != null &&
                    (str = range.get_Value(Type.Missing).ToString()) != null)
            {
                res.Nomenclature.Name = str;
            }
            else
            {
                throw new System.FormatException(CommonLibrary.Resources.Localization.Language.Error);
            }
            range = ws.Cells[row, 3] as Excel.Range;
            if (range.get_Value(System.Type.Missing) != null &&
                    (str = range.get_Value(System.Type.Missing).ToString()) != null)
            {
                res.Shift.Name = str;
            }
            else
            {
                throw new System.FormatException(CommonLibrary.Resources.Localization.Language.Error);
            }
            
            range = ws.Cells[row, 4] as Excel.Range;
            if (range.get_Value(System.Type.Missing) != null
                && (str = range.get_Value(System.Type.Missing).ToString()) != null)
            {
                res.PackageView.Name = str;
                using (var db = new BaykonDbContext())
                {
                    res.PackageView = db.PackageViews.Find(x => x.Name.ToLower() == str.ToLower());
                }
            }
            else
            {
                throw new System.FormatException(CommonLibrary.Resources.Localization.Language.Error);
            }
            range = ws.Cells[row, 5] as Excel.Range;
            if (range.get_Value(System.Type.Missing) != null
                && (str = range.get_Value(System.Type.Missing).ToString()) != null)
            {
                res.ConeColour.Name = str;
            }
            else
            {
                throw new System.FormatException(CommonLibrary.Resources.Localization.Language.Error);
            }
            int n = 0;
            range = ws.Cells[row, 6] as Excel.Range;
            if (range.get_Value(System.Type.Missing) != null
                && (str = range.get_Value(System.Type.Missing).ToString()) != null
                && int.TryParse(str, out n))
            {
                res.Lot.LastNumber = n;
            }
            else
            {
                throw new System.FormatException(CommonLibrary.Resources.Localization.Language.Error);
            }
            range = ws.Cells[row, 7] as Excel.Range;
            if (range.get_Value(System.Type.Missing) != null
                && (str = range.get_Value(System.Type.Missing).ToString()) != null)
            {
                res.Lot.Name = str;
            }
            else
            {
                throw new System.FormatException(CommonLibrary.Resources.Localization.Language.Error);
            }
            double d = 0;
            range = ws.Cells[row, 8] as Excel.Range;
            if (range.get_Value(System.Type.Missing) != null
                && (str = range.get_Value(System.Type.Missing).ToString()) != null
                && double.TryParse(str, out d))
            {
                res.StandartWeight = d;
            }
            else
            {
                throw new System.FormatException(CommonLibrary.Resources.Localization.Language.Error);
            }

            range = ws.Cells[row, 9] as Excel.Range;
            if (range.get_Value(System.Type.Missing) != null
                && (str = range.get_Value(System.Type.Missing).ToString()) != null
                && double.TryParse(str, out d))
            {
                res.WeightBox = d;
            }
            else
            {
                throw new System.FormatException(CommonLibrary.Resources.Localization.Language.Error);
            }
            range = ws.Cells[row, 10] as Excel.Range;
            if (range.get_Value(System.Type.Missing) != null
                && (str = range.get_Value(System.Type.Missing).ToString()) != null
                && double.TryParse(str, out d))
            {
                res.WeightBrutto = d;
            }
            else
            {
                throw new System.FormatException(CommonLibrary.Resources.Localization.Language.Error);
            }
            return res;
        }

        public void PrintAll()
        {
            for(int i = 4; i <= ws.UsedRange.Rows.Count; i++)
            {
                Print(GetDataFromExcel(i));
            }
        }

        public void CloseConnect()
        {
            machine.serialPort.Close();
        }

        private void ChangeStateOpenOrCrateExcel()
        {
            try
            {
                if (selDate == null || SelShift == null || SelNomen == null)
                    return;

                string nomenclature = SelNomen.Name;
                string date = selDate.Day + "-" + selDate.Month + "-" + selDate.Year;
                string shift = SelShift.Name;

                string name = nomenclature + "-" + date + "-" + shift;
                string path = Directory.GetCurrentDirectory() + @"\Excels\" + name + ".xlsx";

                if (File.Exists(path))
                {
                    CloseExcel();

                    //excel = new Excel.Application();
                    wb = excel.Workbooks.Open(path);
                    ws = excel.Sheets[1] as Excel.Worksheet;
                }
                else
                {
                    CloseExcel();

                    string file = Directory.GetCurrentDirectory() + @"\Templates\Temp1.xlsx";
                    //excel = new Excel.Application();
                    wb = excel.Workbooks.Open(file);
                    ws = excel.Sheets[1] as Excel.Worksheet;
                    wb.SaveAs(path);
                }
                IsOpen = true;
            }
            catch (Exception exc)
            {
                IsOpen = false;
                MessageBox.Show(exc.ToString());
            }
        }

        public void CloseExcel()
        {
            try
            {
                if (wb != null)
                {
                    wb.Close();
                }
                if (excel != null)
                {
                    excel.Quit();
                }
            }
            catch
            {

            }
        }

        #region Prperty change
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        #endregion

        ~ContextMainWindow()
        {
            //if (wb != null)
            //{
            //    wb.Close();
            //}
            //if (excel != null)
            //{
            //    excel.Quit();
            //}
        }
    }

    class Pair
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
}
