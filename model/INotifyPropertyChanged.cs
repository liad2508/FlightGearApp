using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FlightGearApp
{
    public delegate void PropertyChangedEventHandler(
         Object sender,   PropertyChangedEventArgs e);

    interface INotifyPropertyChanged
    {
        event PropertyChangedEventHandler PropertyChanged;
    }
}
