using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace SpiceManager.WindowView
{
    /// <summary>
    /// Interaction logic for ExportToExcelWindow.xaml
    /// </summary>
    public partial class ExportToExcelWindow : Window
    {
        public ExportToExcelWindow()
        {
            InitializeComponent();
        }

        private void HistroyFromRadio_Checked(object sender, RoutedEventArgs e)
        {
            fromDate.IsEnabled = true;
        }

        private void HistroyFromRadio_Unchecked(object sender, RoutedEventArgs e)
        {
            fromDate.IsEnabled = false;
        }

        private void OtherRadio_Unchecked(object sender, RoutedEventArgs e)
        {
            multiCheck.IsEnabled = false;
            WarehouseRadio.IsChecked = false;
            ProductRadio.IsChecked = false;
            SpiceRadio.IsChecked = false;
        }

        private void OtherRadio_Checked(object sender, RoutedEventArgs e)
        {
            multiCheck.IsEnabled = true;
        }
    }
}
