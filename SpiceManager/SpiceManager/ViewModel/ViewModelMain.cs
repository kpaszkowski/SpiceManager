using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public RelayCommand RemoveProductCommand { get; set; }
        public RelayCommand EditProductCommand { get; set; }

        public RelayCommand AddNewSpiceCommand { get; set; }
        public RelayCommand RemoveSpiceCommand { get; set; }
        public RelayCommand EditSpiceCommand { get; set; }

        public RelayCommand AddSpiceToWarehouseCommand { get; set; }
        public RelayCommand RemoveSpiceFromWarehouseCommand { get; set; }
        public RelayCommand EditSpiceInWarehouseCommand { get; set; }

        public RelayCommand StartProductionCommand { get; set; }
        public RelayCommand ExportToExcelCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }

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
            RemoveProductCommand = new RelayCommand(RemoveProduct);
            EditProductCommand = new RelayCommand(EditProduct);

            AddNewSpiceCommand = new RelayCommand(AddNewSpice);
            RemoveSpiceCommand = new RelayCommand(RemoveSpice);
            EditSpiceCommand = new RelayCommand(EditSpice);

            AddSpiceToWarehouseCommand = new RelayCommand(AddSpiceToWarehouse);
            RemoveSpiceFromWarehouseCommand = new RelayCommand(RemoveSpiceFromWarehouse);
            EditSpiceInWarehouseCommand = new RelayCommand(EditSpiceInWarehouse);

            StartProductionCommand = new RelayCommand(StartProduction);
            ExportToExcelCommand = new RelayCommand(ExportToExcel);
            SaveCommand = new RelayCommand(Save);
            Test();
        }

        #region Methods

        #region Spice

        private void EditSpice(object obj)
        {
            throw new NotImplementedException();
        }

        private void RemoveSpice(object obj)
        {
            throw new NotImplementedException();
        }

        private void AddNewSpice(object obj)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Product

        private void AddNewProduct(object obj)
        {

        }

        private void EditProduct(object obj)
        {
            throw new NotImplementedException();
        }

        private void RemoveProduct(object obj)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Warehouse

        private void EditSpiceInWarehouse(object obj)
        {
            throw new NotImplementedException();
        }

        private void RemoveSpiceFromWarehouse(object obj)
        {
            throw new NotImplementedException();
        }

        private void AddSpiceToWarehouse(object obj)
        {
            throw new NotImplementedException();
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
        #endregion

        #endregion

        private void Test()
        {
            Spice s = new Spice { Id = 1, Name = "Oregano", Amount = 10, Part = "D2aF" };
            Product p = new Product { Id = 6, Name = "Krakowska", SpiceList = new List<Spice> {s} };
            History.Add(new HistoryRecord { Id = 9, Date = DateTime.Now, SpiceList = new List<Spice> { s}, Text = "sabfds" });
            Spices.Add(s);
            Products.Add(p);
            Warehouse.Add(s);
        }
    }
}
