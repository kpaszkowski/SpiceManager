using Newtonsoft.Json;
using SpiceManager.Messages;
using SpiceManager.Other;
using SpiceManager.WindowView.ProductWindow;
using SpiceManager.WindowView.SpiceWindow;
using SpiceManager.WindowView.WarehouseWindow;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SpiceManager
{
    class ViewModelMain : ViewModelBase
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

        public RelayCommand AddNewSpiceCommand { get; set; }
        public RelayCommand AddSpiceToBaseCommand { get; set; }
        public RelayCommand RemoveSpiceCommand { get; set; }
        public RelayCommand EditSpiceCommand { get; set; }
        public RelayCommand EditSpiceInBaseCommand { get; set; }

        public RelayCommand AddSpiceToWarehouseCommand { get; set; }
        public RelayCommand AddSpiceToWarehouseInBaseCommand { get; set; }
        public RelayCommand RemoveSpiceFromWarehouseCommand { get; set; }
        public RelayCommand EditSpiceInWarehouseCommand { get; set; }

        public RelayCommand StartProductionCommand { get; set; }
        public RelayCommand ExportToExcelCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CloseWindowCommand { get; set; }
        public RelayCommand ShowInfoCommand { get; set; }
        public RelayCommand ClearValidationFieldCommand { get; set; }
        #endregion

        #region Selected Items

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

            AddNewSpiceCommand = new RelayCommand(AddNewSpice);
            AddSpiceToBaseCommand = new RelayCommand(AddSpiceToBase);
            RemoveSpiceCommand = new RelayCommand(RemoveSpice);
            EditSpiceCommand = new RelayCommand(EditSpice);
            EditSpiceInBaseCommand = new RelayCommand(EditSpiceInBase);

            AddSpiceToWarehouseCommand = new RelayCommand(AddSpiceToWarehouse);
            AddSpiceToWarehouseInBaseCommand = new RelayCommand(AddSpiceToWarehouseInBase);
            RemoveSpiceFromWarehouseCommand = new RelayCommand(RemoveSpiceFromWarehouse);
            EditSpiceInWarehouseCommand = new RelayCommand(EditSpiceInWarehouse);

            StartProductionCommand = new RelayCommand(StartProduction);
            ExportToExcelCommand = new RelayCommand(ExportToExcel);
            SaveCommand = new RelayCommand(Save);
            CloseWindowCommand = new RelayCommand(CloseWindow);
            ShowInfoCommand = new RelayCommand(ShowInfo);
            ClearValidationFieldCommand = new RelayCommand(ClearValidationField);

            Load();
        }

        #region Methods

        #region Spice

        private void EditSpice(object obj)
        {
            if (!ValidateParams(obj))
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
                SetErrorMessage(string.Empty);
                currentWindow.Close();
            }
        }

        private void RemoveSpice(object obj)
        {
            if (!ValidateParams(obj))
            {
                SetErrorMessage(ValidationMessages.NieWybranoElementuDoUsuniecia);
                return;
            }
            var values = (object[])obj;
            Spice spice = (Spice)values[0];
            string errorMessage = string.Empty;
            if (!CanRemoveSpice(spice,ref errorMessage))
            {
                SetErrorMessage(errorMessage);
                return;
            }
            Spices.Remove(spice);
        }

        private bool CanRemoveSpice(Spice spice, ref string message)
        {
            bool canRemove = true;
            foreach (var item in Products.Where(x => x.SpiceList.Any(y => y.Name == spice.Name)))
            {
                message += string.Format(ValidationMessages.NieMoznaUsunacPrzyprawy+"\n", item.Name);
                canRemove = false;
            }
            foreach (var item in Warehouse.Where(x=>x.Name==spice.Name))
            {
                message += ValidationMessages.NieMoznaUsunacPrzyprawyZnajdujeSieWMagazynie+"\n";
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
                SetErrorMessage(string.Empty);
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
                SetErrorMessage(string.Empty);
                currentWindow.Close();
            }
        }

        private void EditProduct(object obj)
        {
            throw new NotImplementedException();
        }

        private void RemoveProduct(object obj)
        {
            if (!ValidateParams(obj))
            {
                SetErrorMessage(ValidationMessages.NieWybranoElementuDoUsuniecia);
            }
            var values = (object[])obj;
            var product = values[0];
            Products.Remove((Product)product);
        }

        private void RemoveProductSpice(object obj)
        {
            Product product = (Product)SelectedProduct;
            product.SpiceList.Remove((Spice)obj);
            mainWindow.ProductSpiceGrid.Items.Refresh();
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
                SetErrorMessage(ValidationMessages.PrzyprawaJużIstenieWPrzepisie);
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
        }

        #endregion

        #region Warehouse

        private void EditSpiceInWarehouse(object obj)
        {
            throw new NotImplementedException();
        }

        private void RemoveSpiceFromWarehouse(object obj)
        {
            if (!ValidateParams(obj))
            {
                SetErrorMessage(ValidationMessages.NieWybranoElementuDoUsuniecia);
            }
            var values = (object[])obj;
            var spice = values[0];
            Warehouse.Remove((Spice)spice);
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
                    Id = Helper.FindMaxValue(Spices, x => x.Id) + 1,
                    Name = spiceName,
                    Amount=spiceAmount,
                    Part=spicePart,
                };
                Warehouse.Add(spice);
                SetErrorMessage(string.Empty);
                currentWindow.Close();
            }
        }

        #endregion

        #region Other

        private void ExportToExcel(object obj)
        {
            throw new NotImplementedException();
        }

        private void StartProduction(object obj)
        {
            throw new NotImplementedException();
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
            SetErrorMessage(String.Empty);
            currentWindow.Close();
        }

        private void ShowInfo(object obj)
        {

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
            if (string.IsNullOrEmpty(message))
            {
                mainWindow.ErrorTexBlock.Text = "";
            }
            else
            {
                mainWindow.ErrorTexBlock.Text = message;
            }
        }

        #endregion

        #endregion
    }
}
