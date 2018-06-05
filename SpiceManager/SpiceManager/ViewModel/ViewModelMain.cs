using Newtonsoft.Json;
using SpiceManager.Messages;
using SpiceManager.Other;
using SpiceManager.WindowView;
using SpiceManager.WindowView.ProductWindow;
using SpiceManager.WindowView.SpiceWindow;
using SpiceManager.WindowView.WarehouseWindow;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Excel = Microsoft.Office.Interop.Excel;

namespace SpiceManager
{
    public class ViewModelMain : ViewModelBase
    {
        #region Observable Collections

        public ObservableCollection<Spice> Spices { get; set; }
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<HistoryRecord> History { get; set; }
        public ObservableCollection<Spice> Warehouse { get; set; }

        #endregion

        #region Command

        public RelayCommand AddNewProductCommand { get; set; }
        public RelayCommand AddNewProductSpiceCommand { get; set; }
        public RelayCommand AddNewProductSpiceToBaseCommand { get; set; }
        public RelayCommand RemoveProductSpiceCommand { get; set; }
        public RelayCommand AddProductToBaseCommand { get; set; }
        public RelayCommand RemoveProductCommand { get; set; }
        public RelayCommand EditProductCommand { get; set; }
        public RelayCommand EditProductInBaseCommand { get; set; }

        public RelayCommand AddNewSpiceCommand { get; set; }
        public RelayCommand AddSpiceToBaseCommand { get; set; }
        public RelayCommand RemoveSpiceCommand { get; set; }
        public RelayCommand EditSpiceCommand { get; set; }
        public RelayCommand EditSpiceInBaseCommand { get; set; }

        public RelayCommand AddSpiceToWarehouseCommand { get; set; }
        public RelayCommand AddSpiceToWarehouseInBaseCommand { get; set; }
        public RelayCommand RemoveSpiceFromWarehouseCommand { get; set; }
        public RelayCommand EditSpiceInWarehouseCommand { get; set; }
        public RelayCommand EditSpiceInWarehouseInBaseCommand { get; set; }

        public RelayCommand StartProductionCommand { get; set; }
        public RelayCommand OpenWindowProductionCommand { get; set; }
        public RelayCommand ExportToExcelCommand { get; set; }
        public RelayCommand PrintCommand { get; set; }
        public RelayCommand ExportToExcelEndCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CloseWindowCommand { get; set; }
        public RelayCommand RemoveFromHistoryCommand { get; set; }
        public RelayCommand ClearValidationFieldCommand { get; set; }
        public RelayCommand ClearWarehouseCommand { get; set; }
        public RelayCommand ClearHistoryCommand { get; set; }

        #region RadioButton

        #endregion

        #endregion

        #region Selected Items

        private Nullable<DateTime> _DateProp = null;
        public Nullable<DateTime> DateProp
        {
            get
            {
                if (_DateProp == null)
                {
                    _DateProp = DateTime.Today;
                }
                return _DateProp;
            }
            set
            {
                _DateProp = value;
                RaisePropertyChanged("DateProp");
            }
        }

        private Nullable<DateTime> _DateFrom = null;
        public Nullable<DateTime> DateFrom
        {
            get
            {
                if (_DateFrom == null)
                {
                    _DateFrom = DateTime.Today;
                }
                return _DateFrom;
            }
            set
            {
                _DateFrom = value;
                RaisePropertyChanged("DateFrom");
            }
        }

        private Nullable<DateTime> _DateTo = null;
        public Nullable<DateTime> DateTo
        {
            get
            {
                if (_DateTo == null)
                {
                    _DateTo = DateTime.Today;
                }
                return _DateTo;
            }
            set
            {
                _DateTo = value;
                RaisePropertyChanged("DateTo");
            }
        }

        object _SelectedProduct;
        public object SelectedProduct
        {
            get
            {
                return _SelectedProduct;
            }
            set
            {
                if (_SelectedProduct != value)
                {
                    _SelectedProduct = value;
                    RaisePropertyChanged("SelectedProduct");
                }
            }
        }

        object _SelectedSpiceProduct;
        public object SelectedSpiceProduct
        {
            get
            {
                return _SelectedSpiceProduct;
            }
            set
            {
                if (_SelectedSpiceProduct != value)
                {
                    _SelectedSpiceProduct = value;
                    RaisePropertyChanged("SelectedSpiceProduct");
                }
            }
        }

        object _SelectedSpice;
        public object SelectedSpice
        {
            get
            {
                return _SelectedSpice;
            }
            set
            {
                if (_SelectedSpice != value)
                {
                    _SelectedSpice = value;
                    RaisePropertyChanged("SelectedSpice");
                }
            }
        }

        object _SelectedHistoryRecord;
        public object SelectedHistoryRecord
        {
            get
            {
                return _SelectedHistoryRecord;
            }
            set
            {
                if (_SelectedHistoryRecord != value)
                {
                    _SelectedHistoryRecord = value;
                    RaisePropertyChanged("SelectedHistoryRecord");
                }
            }
        }

        object _SelectedWarehouseSpice;
        public object SelectedWarehouseSpice
        {
            get
            {
                return _SelectedWarehouseSpice;
            }
            set
            {
                if (_SelectedWarehouseSpice != value)
                {
                    _SelectedWarehouseSpice = value;
                    RaisePropertyChanged("SelectedWarehouseSpice");
                }
            }
        }

        object _SelectedWarehouseSpiceEditWindow;
        public object SelectedWarehouseSpiceEditWindow
        {
            get
            {
                return _SelectedWarehouseSpiceEditWindow;
            }
            set
            {
                if (_SelectedWarehouseSpiceEditWindow != value)
                {
                    _SelectedWarehouseSpiceEditWindow = value;
                    RaisePropertyChanged("SelectedWarehouseSpiceEditWindow");
                }
            }
        }

        #endregion

        #region Global Variables

        MainWindow mainWindow = (MainWindow)App.Current.MainWindow;

        #endregion

        public ViewModelMain()
        {
            InitializeElements();
        }

        private void InitializeElements()
        {
            Spices = new ObservableCollection<Spice>();
            Products = new ObservableCollection<Product>();
            History = new ObservableCollection<HistoryRecord>();
            Warehouse = new ObservableCollection<Spice>();


            AddNewProductCommand = new RelayCommand(AddNewProduct);

            AddNewProductSpiceCommand = new RelayCommand(AddNewProductSpice);
            AddNewProductSpiceToBaseCommand = new RelayCommand(AddNewProductSpiceToBase); 
            RemoveProductSpiceCommand = new RelayCommand(RemoveProductSpice);

            AddProductToBaseCommand = new RelayCommand(AddProductToBase);
            RemoveProductCommand = new RelayCommand(RemoveProduct);
            EditProductCommand = new RelayCommand(EditProduct);
            EditProductInBaseCommand = new RelayCommand(EditProductInBase);

            AddNewSpiceCommand = new RelayCommand(AddNewSpice);
            AddSpiceToBaseCommand = new RelayCommand(AddSpiceToBase);
            RemoveSpiceCommand = new RelayCommand(RemoveSpice);
            EditSpiceCommand = new RelayCommand(EditSpice);
            EditSpiceInBaseCommand = new RelayCommand(EditSpiceInBase);

            AddSpiceToWarehouseCommand = new RelayCommand(AddSpiceToWarehouse);
            AddSpiceToWarehouseInBaseCommand = new RelayCommand(AddSpiceToWarehouseInBase);
            RemoveSpiceFromWarehouseCommand = new RelayCommand(RemoveSpiceFromWarehouse);
            EditSpiceInWarehouseCommand = new RelayCommand(EditSpiceInWarehouse);
            EditSpiceInWarehouseInBaseCommand = new RelayCommand(EditSpiceInWarehouseInBase);

            StartProductionCommand = new RelayCommand(StartProduction);
            OpenWindowProductionCommand = new RelayCommand(OpenProductionWindow);
            ExportToExcelCommand = new RelayCommand(ExportToExcel);
            PrintCommand = new RelayCommand(PrintDocument);
            ExportToExcelEndCommand = new RelayCommand(ExportToExcelEnd);
            SaveCommand = new RelayCommand(Save);
            CloseWindowCommand = new RelayCommand(CloseWindow);
            RemoveFromHistoryCommand = new RelayCommand(RemoveFromHistory);
            ClearValidationFieldCommand = new RelayCommand(ClearValidationField);
            ClearWarehouseCommand = new RelayCommand(ClearWarehouse);
            ClearHistoryCommand = new RelayCommand(ClearHistory);
            Load();
        }

        #region Methods

        #region Spice

        private void EditSpice(object obj)
        {
            if (!ValidateParamsAsObject(obj))
            {
                SetErrorMessage(ValidationMessages.NieWybranoElementuDoEdycji);
            }
            else
            {
                EditSpiceWindow editSpiceWindow = new EditSpiceWindow();
                editSpiceWindow.DataContext = this;
                editSpiceWindow.Owner = mainWindow;
                editSpiceWindow.ShowDialog();
            }
        }

        private void EditSpiceInBase(object obj)
        {
            var values = (object[])obj;
            string newSpiceName = values[1].ToString();
            bool isSpiceNameUnique = (!Spices.Any(x => x.Name.ToLower() == newSpiceName.ToLower()));
            if (string.IsNullOrEmpty(newSpiceName))
            {
                SetErrorMessage(ValidationMessages.NiePodanoNazwy);
            }
            else if (!isSpiceNameUnique)
            {
                SetErrorMessage(ValidationMessages.NazwaPrzyprawyNieJestUnikalna);
            }
            else
            {
                EditSpiceWindow currentWindow = App.Current.Windows.OfType<EditSpiceWindow>().SingleOrDefault(x => x.IsActive);
                Spice currentSpice = (Spice)values[0];
                string currentSpiceName = currentSpice.Name;
                foreach (var item in Spices.Where(x => x.Name == currentSpiceName))
                {
                    item.Name = newSpiceName;
                }
                UpdateProductAndWarehouseGrid(currentSpiceName, newSpiceName);
                SetConfirmMessages(OtherMessages.PomyslnieEdytowanoElement);
                currentWindow.Close();
            }
        }

        private void RemoveSpice(object obj)
        {
            if (!ValidateParamsAsObject(obj))
            {
                SetErrorMessage(ValidationMessages.NieWybranoElementuDoUsuniecia);
                return;
            }
            Spice spice = (Spice)obj;
            string errorMessage = string.Empty;
            if (!CanRemoveSpice(spice,ref errorMessage))
            {
                SetErrorMessage(errorMessage);
                return;
            }
            Spices.Remove(spice);
            SetConfirmMessages(OtherMessages.PomyslnieUsunietoElement);
        }

        private bool CanRemoveSpice(Spice spice, ref string message)
        {
            bool canRemove = true;
            foreach (var item in Products.Where(x => x.SpiceList.Any(y => y.Name == spice.Name)))
            {
                message += string.Format(ValidationMessages.NieMoznaUsunacPrzyprawy, item.Name);
                canRemove = false;
            }
            foreach (var item in Warehouse.Where(x=>x.Name==spice.Name))
            {
                message += ValidationMessages.NieMoznaUsunacPrzyprawyZnajdujeSieWMagazynie;
                canRemove = false;
            }
            return canRemove;
        }

        private void AddNewSpice(object obj)
        {
            SpiceWindow spiceWindow = new SpiceWindow();
            spiceWindow.DataContext = this;
            spiceWindow.Owner = mainWindow;
            spiceWindow.ShowDialog();
        }

        private void AddSpiceToBase(object obj)
        {
            SpiceWindow currentWindow = App.Current.Windows.OfType<SpiceWindow>().SingleOrDefault(x=>x.IsActive);
            string spiceName = currentWindow.SpiceName.Text.ToString();

            bool isSpiceNameUnique = (!Spices.Any(x => x.Name.ToLower() == spiceName.ToLower()));

            if (string.IsNullOrEmpty(spiceName))
            {
                SetErrorMessage(ValidationMessages.NiePodanoNazwy);
            }
            else if(!isSpiceNameUnique)
            {
                SetErrorMessage(ValidationMessages.NazwaPrzyprawyNieJestUnikalna);
            }
            else
            {
                Spice spice = new Spice
                {
                    Id = Helper.FindMaxValue(Spices, x => x.Id) + 1,
                    Name = spiceName,
                };
                Spices.Add(spice);
                SetConfirmMessages(OtherMessages.PomyslnieDodanoElement);
                currentWindow.Close();
            }
        }

        #endregion

        #region Product

        private void AddNewProduct(object obj)
        {
            AddProductWindow addProductWindow = new AddProductWindow();
            addProductWindow.DataContext = this;
            addProductWindow.Owner = mainWindow;
            addProductWindow.ShowDialog();
        }

        public void AddProductToBase(object obj)
        {
            AddProductWindow currentWindow = App.Current.Windows.OfType<AddProductWindow>().SingleOrDefault(x => x.IsActive);
            string productName = currentWindow.ProductName.Text.ToString();

            bool isSpiceNameUnique = (!Products.Any(x => x.Name.ToLower() == productName.ToLower()));

            if (string.IsNullOrEmpty(productName))
            {
                SetErrorMessage(ValidationMessages.NiePodanoNazwy);
            }
            else if (!isSpiceNameUnique)
            {
                SetErrorMessage(ValidationMessages.NazwaProduktuNieJestUnikalna);
            }
            else
            {
                Product product = new Product
                {
                    Id = Helper.FindMaxValue(Products, x => x.Id) + 1,
                    Name = productName,
                };
                Products.Add(product);
                SetConfirmMessages(OtherMessages.PomyslnieDodanoElement);
                currentWindow.Close();
            }
        }

        private void EditProduct(object obj)
        {
            EditProductWindow editProductWindow = new EditProductWindow();
            editProductWindow.DataContext = this;
            editProductWindow.Owner = mainWindow;
            editProductWindow.ShowDialog();
        }

        private void EditProductInBase(object obj)
        {
            var values = (object[])obj;
            string newProductName = values[1].ToString();
            bool isProductNameUnique = (!Products.Any(x => x.Name.ToLower() == newProductName.ToLower()));
            if (string.IsNullOrEmpty(newProductName))
            {
                SetErrorMessage(ValidationMessages.NiePodanoNazwy);
            }
            else if (!isProductNameUnique)
            {
                SetErrorMessage(ValidationMessages.NazwaProduktuNieJestUnikalna);
            }
            else
            {
                EditProductWindow currentWindow = App.Current.Windows.OfType<EditProductWindow>().SingleOrDefault(x => x.IsActive);
                Product currentProduct = (Product)values[0];
                string currentProductName = currentProduct.Name;
                foreach (var item in Products.Where(x => x.Name == currentProductName))
                {
                    item.Name = newProductName;
                }
                //UpdateProductAndWarehouseGrid(currentProductName, newProductName);
                SetConfirmMessages(OtherMessages.PomyslnieEdytowanoElement);
                currentWindow.Close();
            }
        }

        private void RemoveProduct(object obj)
        {
            if (!ValidateParamsAsObject(obj))
            {
                SetErrorMessage(ValidationMessages.NieWybranoElementuDoUsuniecia);
            }
            var product = obj;
            Products.Remove((Product)product);
            SetConfirmMessages(OtherMessages.PomyslnieUsunietoElement);
        }

        private void RemoveProductSpice(object obj)
        {
            Product product = (Product)SelectedProduct;
            product.SpiceList.Remove((Spice)obj);
            mainWindow.ProductSpiceGrid.Items.Refresh();
            SetConfirmMessages(OtherMessages.PomyslnieUsunietoElement);
        }

        private void AddNewProductSpice(object obj)
        {
            if (SelectedProduct==null)
            {
                SetErrorMessage(ValidationMessages.NieWybranoProduktu);
                return;
            }
            AddProductSpiceWindow addProductSpiceWindow = new AddProductSpiceWindow();
            addProductSpiceWindow.DataContext = this;
            addProductSpiceWindow.Owner = mainWindow;
            addProductSpiceWindow.ShowDialog();
        }

        private void AddNewProductSpiceToBase(object obj)
        {
            var values = (object[])obj;
            AddProductSpiceWindow currentWindow = App.Current.Windows.OfType<AddProductSpiceWindow>().SingleOrDefault(x => x.IsActive);
            double n;
            bool isNumeric = double.TryParse(currentWindow.SpiceProductAmount.Text.ToString(), out n);
            if (!isNumeric)
            {
                SetErrorMessage(ValidationMessages.ZleParametry);
                return;
            }
            double spiceAmount = Double.Parse(currentWindow.SpiceProductAmount.Text.ToString());
            Product product = (Product)SelectedProduct;
            if (SelectedSpiceProduct == null)
            {
                SetErrorMessage(ValidationMessages.NieWybranoPrzyprawy);
                return;
            }

            Spice spice = (Spice)values[0];
            string spiceName = spice.Name;
            foreach (var item in product.SpiceList.Where(x=>x.Name.ToLower()==spice.Name.ToLower()))
            {
                SetErrorMessage(ValidationMessages.PrzyprawaJuzIstenieWPrzepisie);
                return;
            }
            Spice spicetoAdd = new Spice
            {
                Id = Helper.FindMaxValue(product.SpiceList, x => x.Id) + 1,
                Name = spiceName,
                Amount = spiceAmount
            };
            string currentProductName = product.Name;
            foreach (var item in Products.Where(x => x.Name.ToLower() == currentProductName.ToLower()))
            {
                item.SpiceList.Add(spicetoAdd);
            }
            currentWindow.Close();
            mainWindow.ProductSpiceGrid.Items.Refresh();
            SetConfirmMessages(OtherMessages.PomyslnieDodanoElement);
        }

        #endregion

        #region Warehouse

        private void EditSpiceInWarehouse(object obj)
        {
            EditWarehouseSpiceWindow editWarehouseSpiceWindow = new EditWarehouseSpiceWindow();             
            editWarehouseSpiceWindow.DataContext = this;
            editWarehouseSpiceWindow.Owner = mainWindow;
            editWarehouseSpiceWindow.ShowDialog();
        }

        private void EditSpiceInWarehouseInBase(object obj)
        {
            if (this.SelectedWarehouseSpice == null)
            {
                SetErrorMessage(ValidationMessages.NieWybranoElementuDoEdycji);
                SelectedWarehouseSpiceEditWindow = null;
                return;
            }
            Spice spice = SelectedWarehouseSpice as Spice;
            EditWarehouseSpiceWindow currentWindow = App.Current.Windows.OfType<EditWarehouseSpiceWindow>().SingleOrDefault(x => x.IsActive);
            double n;
            if (!double.TryParse(currentWindow.SpiceWarehouseAmount.Text.ToString(), out n))
            {
                SetErrorMessage(ValidationMessages.ZleParametry);
                SelectedWarehouseSpiceEditWindow = null;
                return;
            }
            string part = currentWindow.SpiceWarehousePart.Text.ToString();
            if (spice.Part!=part && Warehouse.Any(x => x.Part.ToLower() == part))
            {
                SetErrorMessage(ValidationMessages.NazwaPartiiNieJestUnikalna);
                SelectedWarehouseSpiceEditWindow = null;
                return;
            }
            string name = currentWindow.SpiceWarehouseName.Text.ToString();
            if (string.IsNullOrEmpty(name))
            {
                SetErrorMessage(ValidationMessages.NiePodanoNazwy);
                SelectedWarehouseSpiceEditWindow = null;
                return;
            }
            foreach (Spice item in Warehouse.Where(x => x.Name.ToLower() == spice.Name.ToLower()))
            {
                item.Name = name;
                item.Part = part;
                item.Amount = n;
            }
            SetConfirmMessages(OtherMessages.PomyslnieEdytowanoElement);
            SelectedWarehouseSpiceEditWindow = null;
            currentWindow.Close();
        }

        private void RemoveSpiceFromWarehouse(object obj)
        {
            if (this.SelectedWarehouseSpice == null)
            {
                SetErrorMessage(ValidationMessages.NieWybranoElementuDoUsuniecia);
                return;
            }
            Spice spice = SelectedWarehouseSpice as Spice;
            Warehouse.Remove(spice);
            SetConfirmMessages(OtherMessages.PomyslnieUsunietoElement);
        }

        private void AddSpiceToWarehouse(object obj)
        {
            AddWarehouseSpiceWindow addWarehouseSpiceWindow = new AddWarehouseSpiceWindow();
            addWarehouseSpiceWindow.DataContext = this;
            addWarehouseSpiceWindow.Owner = mainWindow;
            addWarehouseSpiceWindow.ShowDialog();
        }

        private void AddSpiceToWarehouseInBase(object obj)
        {
            AddWarehouseSpiceWindow currentWindow = App.Current.Windows.OfType<AddWarehouseSpiceWindow>().SingleOrDefault(x => x.IsActive);
            double n;
            bool isNumeric = double.TryParse(currentWindow.SpiceWarehouseAmount.Text.ToString(), out n);
            if (!isNumeric)
            {
                SetErrorMessage(ValidationMessages.ZleParametry);
                return;
            }
            string spiceName = currentWindow.SpiceWarehouseName.Text.ToString();
            double spiceAmount = Double.Parse(currentWindow.SpiceWarehouseAmount.Text.ToString());
            string spicePart = currentWindow.SpiceWarehousePart.Text.ToString();
            if (string.IsNullOrEmpty(spiceName) || string.IsNullOrEmpty(spicePart)) 
            {
                SetErrorMessage(ValidationMessages.ZleParametry);
                return;
            }
            bool isSpicePartUnique = (!Warehouse.Any(x => x.Part.ToLower() == spicePart.ToLower()));

            if (string.IsNullOrEmpty(spiceName))
            {
                SetErrorMessage(ValidationMessages.NiePodanoNazwy);
            }
            else if (!isSpicePartUnique)
            {
                SetErrorMessage(ValidationMessages.NazwaPartiiNieJestUnikalna);
            }
            else
            {
                Spice spice = new Spice
                {
                    Id = Helper.FindMaxValue(Warehouse, x => x.Id) + 1,
                    Name = spiceName,
                    Amount=spiceAmount,
                    Part=spicePart,
                };
                Warehouse.Add(spice);
                SetConfirmMessages(OtherMessages.PomyslnieDodanoElement);
                currentWindow.Close();
            }
        }

        private void ClearWarehouse(object obj)
        {
            var itemToRemove = Warehouse.Where(x => x.Amount == 0).ToList();

            if (!itemToRemove.Any())
            {
                SetErrorMessage(ValidationMessages.BrakElementowDoUsuniecia);
                return;
            }
            foreach (var item in itemToRemove)
            {
                Warehouse.Remove(item);
            }
            SetConfirmMessages(OtherMessages.PomyslnieWyczyszczonoMagazyn);
        }

        #endregion

        #region Other

        private void PrintDocument(object obj)
        {
            SetConfirmMessages(OtherMessages.DrukowanieRozpoczete);
            PrintDialog printDlg = new PrintDialog();
            FlowDocument doc = CreateFlowDocument();
            doc.PageHeight = printDlg.PrintableAreaHeight;
            doc.PageWidth = printDlg.PrintableAreaWidth;
            doc.Name = "FlowDoc";
            IDocumentPaginatorSource idpSource = doc;
            printDlg.PrintDocument(idpSource.DocumentPaginator, "Historia produkcji");
            SetConfirmMessages(OtherMessages.DrukowanieZakonczoneSuksesem);
        }

        private FlowDocument CreateFlowDocument()
        {
            FlowDocument doc = new FlowDocument();
            Section sec = new Section();

            Table mainTable = new Table();
            int numberOfColumns = 3;
            
            for (int x = 0; x < numberOfColumns; x++)
            {
                mainTable.Columns.Add(new TableColumn());
            }
            TableRow currentRow; 
            int rowCounter = 0;

            for (int i = 0; i < History.Count; i++)
            {
                mainTable.RowGroups.Add(new TableRowGroup());
                mainTable.RowGroups[0].Rows.Add(new TableRow());
                currentRow = mainTable.RowGroups[0].Rows[rowCounter];
                rowCounter++;
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(History[i].Text))));
                currentRow.Cells[0].ColumnSpan = 2;
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(History[i].Date?.ToString("MM/dd/yyyy")))));
                currentRow.Cells[1].TextAlignment = TextAlignment.Right;
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(" "))));
                currentRow.FontWeight = FontWeights.Bold;
                currentRow.Background = Brushes.Silver;
                currentRow.FontSize = 8;

                for (int j = 0; j < History[i].SpiceList.Count; j++)
                {
                    mainTable.RowGroups[0].Rows.Add(new TableRow());
                    currentRow = mainTable.RowGroups[0].Rows[rowCounter];
                    rowCounter++;
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(History[i].SpiceList[j].Name))));
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(History[i].SpiceList[j].Amount.ToString("0.00")+" kg"))));
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run("partia " + History[i].SpiceList[j].Part))));
                    currentRow.FontSize = 8;
                }
            }

            doc.Blocks.Add(mainTable);

            FlowDocumentReader myFlowDocumentReader = new FlowDocumentReader();
            myFlowDocumentReader.Document = doc;

            return doc;
        }

        private void ExportToExcel(object obj)
        {
            ExportToExcelWindow exportToExcelWindow = new ExportToExcelWindow();
            exportToExcelWindow.DataContext = this;
            exportToExcelWindow.Owner = mainWindow;
            exportToExcelWindow.ShowDialog();
        }

        private void ExportToExcelEnd(object obj)
        {
            ExportToExcelWindow currentWindow = App.Current.Windows.OfType<ExportToExcelWindow>().SingleOrDefault(x => x.IsActive);
            var desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            if (string.IsNullOrEmpty(currentWindow.fileName.Text.ToString()))
            {
                SetErrorMessage(ValidationMessages.NiePodanoNazwy);
                return;
            }
            string fileName = currentWindow.fileName.Text.ToString() + ".xls";

            Excel.Application xlApp = new Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("Program Excel nie został wykryty na twoim komputerze.");
                return;
            }

            Excel.Workbook xlWorkBook;
            object misValue = System.Reflection.Missing.Value;
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            if (currentWindow.HistoryRadio.IsChecked == true)
            {
                Excel.Sheets worksheets = xlWorkBook.Worksheets;
                var xlNewSheet = (Excel.Worksheet)worksheets.Add(worksheets[1], Type.Missing, Type.Missing, Type.Missing);
                Excel.Range range;
                xlNewSheet.Name = "Historia produkcji";
                xlNewSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlNewSheet.Select();

                int spiceListColumn = 2;
                int productRow=2;

                xlNewSheet.Cells[1, 2] = "Przyprawa";
                xlNewSheet.Cells[1, 3] = "Ilość";
                xlNewSheet.Cells[1, 4] = "Partia";
                xlNewSheet.Cells[1, 2].EntireRow.Font.Bold = true;
                xlNewSheet.Cells[1, 3].EntireRow.Font.Bold = true;
                xlNewSheet.Cells[1, 4].EntireRow.Font.Bold = true;
                for (int i = 0; i < History.Count; i++)
                {
                    int counter = productRow;
                    xlNewSheet.Cells[productRow, 1] = History[i].Text;
                    xlNewSheet.Cells[productRow+1, 1] = History[i].Date;
                    xlNewSheet.Cells[productRow + 1, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    for (int j = 0; j < History[i].SpiceList.Count; j++)
                    {
                        xlNewSheet.Cells[productRow + j, spiceListColumn] = History[i].SpiceList[j].Name;
                        xlNewSheet.Cells[productRow + j, spiceListColumn+1] = History[i].SpiceList[j].Amount;
                        xlNewSheet.Cells[productRow + j, spiceListColumn+2] = History[i].SpiceList[j].Part;
                    }
                    counter += History[i].SpiceList.Count-1;
                    Excel.Range borderRange;
                    borderRange = xlNewSheet.get_Range("B" + productRow, "D" + counter);
                    borderRange.Borders.Color = System.Drawing.Color.Black.ToArgb();
                    productRow += History[i].SpiceList.Count+1;
                }
                range = xlNewSheet.get_Range("A1", "D" + productRow);
                range.Columns.AutoFit();
                Marshal.ReleaseComObject(xlNewSheet);
            }
            else if(currentWindow.HistroyFromRadio.IsChecked == true)
            {
                var fromDate = currentWindow.fromDate.SelectedDate.Value;
                var toDate = currentWindow.toDate.SelectedDate.Value;
                Excel.Sheets worksheets = xlWorkBook.Worksheets;
                var xlNewSheet = (Excel.Worksheet)worksheets.Add(worksheets[1], Type.Missing, Type.Missing, Type.Missing);
                Excel.Range range;
                xlNewSheet.Name = "Historia produkcji";
                xlNewSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlNewSheet.Select();

                int spiceListColumn = 2;
                int productRow = 2;
                xlNewSheet.Cells[1, 1] = "Od " +fromDate.Date.ToShortDateString() + " do " +toDate.Date.ToShortDateString();
                xlNewSheet.Cells[1, 2] = "Przyprawa";
                xlNewSheet.Cells[1, 3] = "Ilość";
                xlNewSheet.Cells[1, 4] = "Partia";
                xlNewSheet.Cells[1, 2].EntireRow.Font.Bold = true;
                xlNewSheet.Cells[1, 3].EntireRow.Font.Bold = true;
                xlNewSheet.Cells[1, 4].EntireRow.Font.Bold = true;
                ObservableCollection<HistoryRecord> newHistoryList = new ObservableCollection<HistoryRecord>();
                foreach (var item in History.Where(x => x.Date >= fromDate && x.Date <= toDate))
                {
                    newHistoryList.Add(item);
                }
                if (!newHistoryList.Any())
                {
                    SetErrorMessage(string.Format(ValidationMessages.NieMaCzegoEksportowac,fromDate.Date,toDate.Date));
                    return;
                }
                for (int i = 0; i < newHistoryList.Count; i++)
                {
                    int counter = productRow;
                    xlNewSheet.Cells[productRow, 1] = newHistoryList[i].Text;
                    xlNewSheet.Cells[productRow+1, 1] = newHistoryList[i].Date;
                    xlNewSheet.Cells[productRow + 1, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    for (int j = 0; j < newHistoryList[i].SpiceList.Count; j++)
                    {
                        xlNewSheet.Cells[productRow + j, spiceListColumn] = newHistoryList[i].SpiceList[j].Name;
                        xlNewSheet.Cells[productRow + j, spiceListColumn + 1] = newHistoryList[i].SpiceList[j].Amount;
                        xlNewSheet.Cells[productRow + j, spiceListColumn + 2] = newHistoryList[i].SpiceList[j].Part;
                    }
                    counter += newHistoryList[i].SpiceList.Count - 1;
                    Excel.Range borderRange;
                    borderRange = xlNewSheet.get_Range("B" + productRow, "D" + counter);
                    borderRange.Borders.Color = System.Drawing.Color.Black.ToArgb();
                    productRow += newHistoryList[i].SpiceList.Count+1;
                }
                range = xlNewSheet.get_Range("A1", "D" + productRow);
                range.Columns.AutoFit();
                Marshal.ReleaseComObject(xlNewSheet);
            }
            else
            {
                int counter = 1;
                if (currentWindow.ProductRadio.IsChecked == true) 
                {
                    Excel.Sheets worksheets = xlWorkBook.Worksheets;
                    var xlNewSheet = (Excel.Worksheet)worksheets.Add(worksheets[counter], Type.Missing, Type.Missing, Type.Missing);
                    xlNewSheet.Name = "Zestawienie";
                    xlNewSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(counter);
                    xlNewSheet.Select();
                    Excel.Range range;
                    for (int i = 1; i <= Products.Count; i++)
                    {
                        xlNewSheet.Cells[1, i+1] = Products[i - 1].Name;
                    }
                    for (int i = 1; i <= Spices.Count; i++)
                    {
                        xlNewSheet.Cells[i+1, 1] = Spices[i - 1].Name;
                    }
                    for (int i = 0; i < Products.Count; i++)
                    {
                        for (int j = 0; j < Spices.Count; j++)
                        {
                            if (Products[i].SpiceList.Any(x => x.Name.ToLower() == Spices[j].Name.ToLower()))
                            {
                                var currentAmount = Products[i].SpiceList.FirstOrDefault(x => x.Name.ToLower() == Spices[j].Name.ToLower());
                                xlNewSheet.Cells[j + 2, i + 2] = currentAmount != null ? currentAmount.Amount.ToString() : "";
                            }
                        }
                    }
                    range = xlNewSheet.get_Range("A1", "D" + Spices.Count);
                    range.Columns.AutoFit();
                    Marshal.ReleaseComObject(xlNewSheet);
                    counter++;
                }
                if(currentWindow.WarehouseRadio.IsChecked == true)
                {
                    Excel.Range range;
                    Excel.Sheets worksheets = xlWorkBook.Worksheets;
                    var xlNewSheet = (Excel.Worksheet)worksheets.Add(worksheets[counter], Type.Missing, Type.Missing, Type.Missing);
                    xlNewSheet.Name = "Stan Magazynu";
                    xlNewSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(counter);
                    xlNewSheet.Select();
                    int startRow = 2;
                    xlNewSheet.Cells[1, 1] = "Stan magazynu na dzień " + DateTime.Now.ToShortDateString();
                    xlNewSheet.Cells[1, 2] = "Przyprawa";
                    xlNewSheet.Cells[1, 3] = "Ilość";
                    xlNewSheet.Cells[1, 4] = "Partia";
                    xlNewSheet.Cells[1, 2].EntireRow.Font.Bold = true;
                    xlNewSheet.Cells[1, 3].EntireRow.Font.Bold = true;
                    xlNewSheet.Cells[1, 4].EntireRow.Font.Bold = true; ;
                    for (int i = 0; i < Warehouse.Count; i++)
                    {
                        xlNewSheet.Cells[startRow, 2] = Warehouse[i].Name;
                        xlNewSheet.Cells[startRow, 3] = Warehouse[i].Amount;
                        xlNewSheet.Cells[startRow, 4] = Warehouse[i].Part;
                        startRow++;
                    }
                    range = xlNewSheet.get_Range("A1", "D" + Warehouse.Count);
                    range.Columns.AutoFit();
                    Marshal.ReleaseComObject(xlNewSheet);
                }
            }            

            xlWorkBook.SaveAs(desktopFolder.ToString()+"\\"+fileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);
            SetConfirmMessages(string.Format(OtherMessages.EksportZakonczonySuksesem, fileName));
            currentWindow.Close();
        }

        private void StartProduction(object obj)
        {
            if (!ValidateParams(obj))
            {
                SetErrorMessage(ValidationMessages.ZleParametry);
                return;
            }
            ProductionWindow productionWindow = App.Current.Windows.OfType<ProductionWindow>().SingleOrDefault(x => x.IsActive);
            string productionAmountTry = productionWindow.ProductionAmount.Text.ToString();
            double n;
            bool isNumeric = double.TryParse(productionAmountTry.ToString(), out n);
            if (!isNumeric)
            {
                SetErrorMessage(ValidationMessages.ZleParametry);
                return;
            }
            var values = (object[])obj;
            Product currentProduct = (Product)values[0];
            var date = (Nullable<DateTime>)values[1];
            double productionAmout = double.Parse(productionAmountTry);
            productionAmout /= 100;
            bool isWarehouseEmpty = false;
            string errorMessage = string.Empty;
            foreach (Spice item in currentProduct.SpiceList)
            {
                double spiceAmount = item.Amount * productionAmout;
                List<Spice> spiceList = Warehouse.Where(x => x.Name.ToLower() == item.Name.ToLower() && x.Amount > 0).ToList();
                double summaryAmount = 0;
                foreach (Spice s in spiceList)
                {
                    summaryAmount += s.Amount;
                }
                if (spiceAmount>summaryAmount)
                {
                    isWarehouseEmpty = true;
                    errorMessage += string.Format(ValidationMessages.BrakPrzyprawy, (item.Amount * productionAmout) - summaryAmount, item.Name);
                }
            }
            if (!isWarehouseEmpty)
            {
                HistoryRecord historyRecord = new HistoryRecord();
                historyRecord.Text = string.Format(OtherMessages.WyprodukowanoKG, productionAmout*100, currentProduct.Name);
                historyRecord.Id = Helper.FindMaxValue(History, x => x.Id)+1;
                historyRecord.Date = date;
                SetConfirmMessages(OtherMessages.ProdukcjaZakonczonaSuksesem);
                foreach (Spice item in currentProduct.SpiceList)
                {
                    double itemAmount = item.Amount * productionAmout;
                    List<Spice> spiceList = Warehouse.Where(x => x.Name.ToLower() == item.Name.ToLower() && x.Amount > 0).ToList();
                    if (spiceList!=null)
                    {
                        for (int i = 0; i < spiceList.Count; i++)
                        {
                            if (itemAmount==0)
                            {
                                break;
                            }
                            if (itemAmount<=spiceList[i].Amount)//od pierwszej
                            {
                                spiceList[i].Amount -= itemAmount;
                                historyRecord.SpiceList.Add(new Spice
                                {
                                    Id = Helper.FindMaxValue(historyRecord.SpiceList, x => x.Id) + 1,
                                    Name = spiceList[i].Name,
                                    Amount = itemAmount,
                                    Part = spiceList[i].Part
                                });
                                itemAmount = 0;
                            }
                            else
                            {
                                itemAmount -= spiceList[i].Amount;
                                historyRecord.SpiceList.Add(new Spice
                                {
                                    Id = Helper.FindMaxValue(historyRecord.SpiceList, x => x.Id) + 1,
                                    Name = spiceList[i].Name,
                                    Amount = spiceList[i].Amount,
                                    Part = spiceList[i].Part
                                });
                                spiceList[i].Amount = 0;
                            }

                        }
                    }
                }
                History.Add(historyRecord);
            }
            if (!string.IsNullOrEmpty(errorMessage))
            {
                SetErrorMessage(errorMessage);
            }
            productionWindow.Close();
        }

        private void OpenProductionWindow(object obj)
        {
            ProductionWindow productionWindow = new ProductionWindow();
            productionWindow.DataContext = this;
            productionWindow.Owner = mainWindow;
            productionWindow.ShowDialog();
        }

        private void Save(object obj)
        {
            //produkty
            string json = JsonConvert.SerializeObject(Products);
            string fileName = "products.txt";
            string path = Path.Combine(Environment.CurrentDirectory, fileName);
            System.IO.File.WriteAllText(path, json);

            //przyprawy
            json = JsonConvert.SerializeObject(Spices);
            fileName = "spices.txt";
            path = Path.Combine(Environment.CurrentDirectory, fileName);
            System.IO.File.WriteAllText(path, json);

            //magazyn
            json = JsonConvert.SerializeObject(Warehouse);
            fileName = "warehouse.txt";
            path = Path.Combine(Environment.CurrentDirectory, fileName);
            System.IO.File.WriteAllText(path, json);

            //hisotria
            json = JsonConvert.SerializeObject(History);
            fileName = "history.txt";
            path = Path.Combine(Environment.CurrentDirectory, fileName);
            System.IO.File.WriteAllText(path, json);

            SetConfirmMessages(OtherMessages.ZapisZakonczonySuksesem);
        }

        private void Load()
        {
            string fileName = "products.txt";
            string path = Path.Combine(Environment.CurrentDirectory, fileName);
            string json = System.IO.File.ReadAllText(path);
            var resultProduct = JsonConvert.DeserializeObject<ObservableCollection<Product>>(json);
            if (resultProduct != null)
            {
                Products = new ObservableCollection<Product>(resultProduct);
            }
            fileName = "spices.txt";
            path = Path.Combine(Environment.CurrentDirectory, fileName);
            json = System.IO.File.ReadAllText(path);
            var resultSpice = JsonConvert.DeserializeObject<ObservableCollection<Spice>>(json);
            if (resultSpice != null)
            {
                Spices = new ObservableCollection<Spice>(resultSpice);
            }
            fileName = "warehouse.txt";
            path = Path.Combine(Environment.CurrentDirectory, fileName);
            json = System.IO.File.ReadAllText(path);
            var resultWarehouse = JsonConvert.DeserializeObject<ObservableCollection<Spice>>(json);
            if (resultWarehouse != null)
            {
                Warehouse = new ObservableCollection<Spice>(resultWarehouse);
            }
            fileName = "history.txt";
            path = Path.Combine(Environment.CurrentDirectory, fileName);
            json = System.IO.File.ReadAllText(path);
            var resultHistory = JsonConvert.DeserializeObject<ObservableCollection<HistoryRecord>>(json);
            if (resultHistory != null)
            {
                History = new ObservableCollection<HistoryRecord>(resultHistory);
            }

        }

        private void CloseWindow(object obj)
        {
            var currentWindow=App.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            //SetErrorMessage(String.Empty);
            currentWindow.Close();
        }

        private void RemoveFromHistory(object obj)
        {
            RemoveHistoryWindow removeHistoryWindow = App.Current.Windows.OfType<RemoveHistoryWindow>().SingleOrDefault(x => x.IsActive);

            var itemToRemove = History.Where(x => x.Date < removeHistoryWindow.dpCalendar1.SelectedDate).ToList();

            if (!itemToRemove.Any())
            {
                SetErrorMessage(ValidationMessages.BrakElementowDoUsuniecia);
                return;
            }
            foreach (var item in itemToRemove)
            {
                History.Remove(item);
            }
            SetConfirmMessages(string.Format(OtherMessages.PomyslnieWyczyszczonoHistorie,itemToRemove.Count,removeHistoryWindow.dpCalendar1.SelectedDate.Value.ToShortDateString()));
        }

        private void ClearValidationField(object obj)
        {
            SetErrorMessage(string.Empty);
        }

        public void UpdateProductAndWarehouseGrid(string currentSpiceName, string newSpiceName)
        {
            foreach (var item in Warehouse.Where(x=>x.Name.ToLower()==currentSpiceName.ToLower()))
            {
                item.Name = newSpiceName;
            }
            foreach (var item in Products)
            {
                foreach (var spice in item.SpiceList.Where(x=>x.Name.ToLower()==currentSpiceName.ToLower()))
                {
                    spice.Name = newSpiceName;
                }
            }
        }

        public bool ValidateParams(object parameter)
        {
            if (parameter == null)
            {
                return false;
            }
            var values = (object[])parameter;
            foreach (var item in values)
            {
                if (item == null)
                {
                    return false;
                }
                if (item as String == string.Empty)
                {
                    return false;
                }
            }
            return true;
        }

        public bool ValidateParamsAsObject(object parameter)
        {
            if (parameter == null)
            {
                return false;
            }
            return true;
        }

        private void SetErrorMessage(string message)
        {
            mainWindow.ErrorTexBlock.Foreground = Brushes.Red;
            if (string.IsNullOrEmpty(message))
            {
                mainWindow.ErrorTexBlock.Text = "";
            }
            else
            {
                mainWindow.ErrorTexBlock.Text = message;
            }
        }

        private void SetConfirmMessages(string message)
        {
            mainWindow.ErrorTexBlock.Foreground = Brushes.Green;
            if (string.IsNullOrEmpty(message))
            {
                mainWindow.ErrorTexBlock.Text = "";
            }
            else
            {
                mainWindow.ErrorTexBlock.Text = message;
            }
        }

        private void ClearHistory(object obj)
        {
            RemoveHistoryWindow removeHistoryWindow = new RemoveHistoryWindow();
            removeHistoryWindow.DataContext = this;
            removeHistoryWindow.Owner = mainWindow;
            removeHistoryWindow.ShowDialog();
        }

        #endregion

        #endregion
    }
}
