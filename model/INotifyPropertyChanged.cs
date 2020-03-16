using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightGearApp
{
    public delegate void PropertyChangedEventHandler(
         Object sender, EventArgs e);

    interface INotifyPropertyChanged
    {
        event PropertyChangedEventHandler PropertyChanged;
    }


}
