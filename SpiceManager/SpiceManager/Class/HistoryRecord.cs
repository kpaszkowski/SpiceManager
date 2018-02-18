using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SpiceManager
{
    public class HistoryRecord : INotifyPropertyChanged
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
        string _Text;
        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                if (_Text != value)
                {
                    _Text = value;
                    RaisePropertyChanged("Text");
                }
            }
        }
        Nullable<DateTime> _Date;
        public Nullable<DateTime> Date
        {
            get
            {
                return _Date;
            }
            set
            {
                if (_Date != value)
                {
                    _Date = value;
                    RaisePropertyChanged("Date");
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