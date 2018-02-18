using System.ComponentModel;

namespace SpiceManager
{
    public class Spice : INotifyPropertyChanged
    {
        int _Id;
        string _Name;
        double _Amount;
        string _Part;
        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id!=value)
                {
                    _Id = value;
                    RaisePropertyChanged("ID");
                }
            }
        }
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }
        public double Amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                if (_Amount != value)
                {
                    _Amount = value;
                    RaisePropertyChanged("Amount");
                }
            }
        }
        public string Part
        {
            get
            {
                return _Part;
            }
            set
            {
                if (_Part != value)
                {
                    _Part = value;
                    RaisePropertyChanged("Part");
                }
            }
        }
        public Spice()
        {

        }
        public Spice(string name, double amount, string part)
        {
            this.Name = name;
            this.Amount = amount;
            this.Part = part;
        }

        void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}