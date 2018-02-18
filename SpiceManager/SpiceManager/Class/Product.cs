using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SpiceManager
{
    public class Product : INotifyPropertyChanged
    {
        int _Id;
        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                    RaisePropertyChanged("Id");
                }
            }
        }
        string _Name;
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
        List<Spice> _SpiceList;
        public List<Spice> SpiceList
        {
            get
            {
                return _SpiceList;
            }
            set
            {
                if (_SpiceList != value)
                {
                    _SpiceList = value;
                    RaisePropertyChanged("SpiceList");
                }
            }
        }
        void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}