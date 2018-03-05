using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SpiceManager.Class
{
    public class UniqueValidationRule : ValidationRule
    {
        public ObservableCollection<Product> Products { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            

            var str = value as string;

            if (str==null)
            {
                return new ValidationResult(false, "Please enter some text");
            }
            foreach (var item in Products.Where(x=>x.Name.ToLower()==str.ToLower()))
            {
                return new ValidationResult(false, "Please enter unique text");
            }
            return new ValidationResult(true,null);
        }
    }
}
