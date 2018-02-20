﻿using SpiceManager.Messages;
using SpiceManager.Other;
using SpiceManager.WindowView.ProductWindow;
using SpiceManager.WindowView.SpiceWindow;
using SpiceManager.WindowView.WarehouseWindow;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            Test();
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
                foreach (var item in Spices.Where(x => x.Id == currentSpice.Id))
                {
                    item.Name = newSpiceName;
                }
                UpdateProductAndWarehouseGrid(currentSpice.Id, newSpiceName);
                SetErrorMessage(string.Empty);
                currentWindow.Close();
            }
        }

        private void RemoveSpice(object obj)
        {
            if (!ValidateParams(obj))
            {
                SetErrorMessage(ValidationMessages.NieWybranoElementuDoUsuniecia);
            }
            var values = (object[])obj;
            var spice = values[0];
            Spices.Remove((Spice)spice);
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
            string spiceName = currentWindow.SpiceWarehouseName.Text.ToString();
            double spiceAmount = Double.Parse(currentWindow.SpiceWarehouseAmount.Text.ToString());
            string spicePart = currentWindow.SpiceWarehousePart.Text.ToString();

            bool isSpiceNameUnique = (!Warehouse.Any(x => x.Name.ToLower() == spiceName.ToLower()));

            if (string.IsNullOrEmpty(spiceName))
            {
                SetErrorMessage(ValidationMessages.NiePodanoNazwy);
            }
            else if (!isSpiceNameUnique)
            {
                SetErrorMessage(ValidationMessages.NazwaPrzyprawyNieJestUnikalna);
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
            throw new NotImplementedException();
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

        public void UpdateProductAndWarehouseGrid(int id, string newSpiceName)
        {
            foreach (var item in Warehouse.Where(x=>x.Id==id))
            {
                item.Name = newSpiceName;
            }
            foreach (var item in Products)
            {
                foreach (var spice in item.SpiceList.Where(x=>x.Id==id))
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
            mainWindow.ErrorTexBlock.Text = message;
        }

        #endregion

        #endregion

        private void Test()
        {
            Spice s = new Spice { Id = 1, Name = "Oregano"};
            Spice s2 = new Spice { Id = 1, Name = "Oregano", Amount = 10, Part = "D2aF" };
            Product p = new Product { Id = 6, Name = "Krakowska", SpiceList = new List<Spice> {s} };
            History.Add(new HistoryRecord { Id = 9, Date = DateTime.Now, SpiceList = new List<Spice> { s}, Text = "sabfds" });
            Spices.Add(s);
            Products.Add(p);
            Warehouse.Add(s2);
        }
    }
}
