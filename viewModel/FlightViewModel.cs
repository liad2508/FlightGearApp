using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Threading;

namespace FlightGearApp
{
    class FlightViewModel : INotifyPropertyChanged
    {
        private IModel model;
        // CTR
        public FlightViewModel(IModel m)
        {
            this.model = m;
            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_"+e.PropertyName);
            };


        }

        public void VM_Disconnect()
        {
            this.model.disconnect();
        }

        public void VM_Connect(string ip, int port)
        {
            this.model.connect(ip, port);
        }

        public IModel getModel()
        {
            return this.model;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                // using downcasting for the object PropertyChangedEventArgs
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        // Properties:
        private double aileron;
        public double VM_Aileron
        {
            get 
            {
                Console.WriteLine("Aileron: View <-- VM <-- Model");
                return this.aileron;
            }
            set
            {
                Console.WriteLine("Aileron: View --> VM --> Model" + this.aileron.ToString());
                this.aileron = value;
                Thread t2 = new Thread(() => this.model.changeAileron(this.aileron));
                //this.model.changeAileron(this.aileron);
                t2.Start();
            }
        }

        private double elevator;
        public double VM_Elevator
        {
            get 
            {
                Console.WriteLine("Elevator: View <-- VM <-- Model");
                return this.elevator;
            }
            set
            {
                Console.WriteLine("Elevator: View --> VM --> Model");
                this.elevator = value;
                Thread t3 = new Thread(() => this.model.changeElevator(this.elevator));
                t3.Start();
                //this.model.changeElevator(this.elevator);
            }
        }

        private double throttle;
        public double VM_Throttle
        {
            get 
            {
                Console.WriteLine("Throttle: View <-- VM <-- Model");
                return this.throttle;            
              
            } 
            set
            {
                Console.WriteLine("Throttle: View --> VM --> Model");
                this.throttle = value;
                Thread t = new Thread(() => this.model.changeThrottle(this.throttle));
                //this.model.changeThrottle(this.throttle); 
                t.Start();

            }
        }
        private double rudder;
        public double VM_Rudder
        {
            get 
            {
                Console.WriteLine("Rudder: View <-- VM <-- Model " + this.rudder.ToString());
                return rudder;  
            }
            set
            {
                Console.WriteLine("Rudder: View --> VM --> Model" + this.rudder.ToString());
                this.rudder = value;
                Thread t1 = new Thread(() => this.model.changeRudder(this.rudder));
                //this.model.changeRudder(this.rudder);
                t1.Start();
            }
        }
        public Thickness Margin
        {
            get { return new Thickness(VM_Lat, VM_Lat, VM_Lon, VM_Lon); }
        }

        // The X coordinate
        public double VM_Lat
        {
            get {
                              
                return model.lat; }
        }
        // The Y coordinate
        public double VM_Lon
        {
            get { return model.lon; }
        }

        public string VM_HeadingDegree
        {
            get { return model.heading_Degree; }
        }

        public string VM_Altitude
        {
            get { return model.altitude; }
        }

        public string VM_GroundSpeed
        {
            get { return model.ground_Speed; }
        }

        public string VM_Altimeter
        {
            get { return model.altimeter; }
        }

        
        public string VM_VerticalSpeed
        {
            get { return model.vertical_Speed; }
            
        }

        public string VM_Pitch
        {
            get { return model.pitch; }
        }

        private string vm_air;
        public string VM_AirSpeed
        {
            get {
               
                return model.airSpeed; }   
            set
            {
                vm_air = value;
               
            }
        }

        public string VM_Roll
        {
            get { return model.roll; }
        }

        /*
        public void changeThrottle(double t)
        {

        }*/
    }
}
