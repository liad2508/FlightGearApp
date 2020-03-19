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

        // Properties:
        public double VM_Aileron
        {
            get 
            {
                Console.WriteLine("Aileron: View <-- VM <-- Model");
                NotifyPropertyChanged("VM_Aileron");
                return model.aileron; 
            }
            set
            {
                Console.WriteLine("Aileron: View --> VM --> Model");
                model.setAileron(value);
            }
        }

        public double VM_Elevator
        {
            get 
            {
                Console.WriteLine("Elevator: View <-- VM <-- Model");
                NotifyPropertyChanged("VM_Elevator");
                return model.elevator; 
            }
            set
            {
                Console.WriteLine("Elevator: View --> VM --> Model");
                model.setElevator(value);
            }
        }

        public double VM_Throttle
        {
            get 
            {
                Console.WriteLine("Throttle: View <-- VM <-- Model");
                NotifyPropertyChanged("VM_Throttle");
                return model.throttle; 
            } 
            set
            {
                Console.WriteLine("Throttle: View --> VM --> Model");
                model.setThrottle(value);
            }
        }

        public double VM_Rudder
        {
            get 
            {
                Console.WriteLine("Rudder: View <-- VM <-- Model");
                NotifyPropertyChanged("VM_Rudder");
                return model.rudder;  
            }
            set
            {
                Console.WriteLine("Rudder: View --> VM --> Model");
                model.setRudder(value);
            }
        }

        public double VM_Lat
        {
            get { return model.lat; }
        }

        public double VM_Lon
        {
            get { return model.lon; }
        }

        public double VM_HeadingDegree
        {
            get { return model.heading_Degree; }
        }

        public double VM_Altitude
        {
            get { return model.altitude; }
        }

        public double VM_GroundSpeed
        {
            get { return model.ground_Speed; }
        }

        public double VM_Altimeter
        {
            get { return model.altimeter; }
        }

        public double VM_VerticalSpeed
        {
            get { return model.vertical_Speed; }
        }

        public double VM_Pitch
        {
            get { return model.pitch; }
        }

        public double VM_AirSpeed
        {
            get { return model.airSpeed; }
        }

        public double VM_Roll
        {
            get { return model.roll; }
        }

    }
}
