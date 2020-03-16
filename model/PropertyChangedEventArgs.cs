using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightGearApp
{
    class PropertyChangedEventArgs : EventArgs
    {
        private string property;
        public PropertyChangedEventArgs(string name) {
            this.property = name;
        }

        string propertyGetter()
        {
            return this.property;
        }


    }
}
