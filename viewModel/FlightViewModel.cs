using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FlightGearApp
{
    class FlightViewModel : INotifyPropertyChanged
    {
        private IModel model;
        // CTR
        public FlightViewModel(IModel m)
        {
            this.model = m;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };

        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                // using downcasting for the object PropertyChangedEventArgs
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        // properties - through the VM the view will recieve the model's data
        public double VM_aileron
        {
            get { return model.aileron; }
        }

        public double VM_elevator
        {
            get { return model.elevator; }
        }

        
    }
}
