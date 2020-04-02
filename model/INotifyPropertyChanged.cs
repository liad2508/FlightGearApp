using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FlightGearApp
{
    // Defining the delgate type.
    public delegate void PropertyChangedEventHandler(
         Object sender,   PropertyChangedEventArgs e);

    // Defining an interface which has an event field.
    // The following interface is used as a part of the OB DP. His purpose is notifying
    // other objects in the program such as the VM & the View about properties modifications.
    interface INotifyPropertyChanged
    {
        // The field's type is the delegated type which appears above.
        event PropertyChangedEventHandler PropertyChanged;
    }
}
