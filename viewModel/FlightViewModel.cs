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
    // The following class represnts the VM of our program.
    // It implements the INotifyPropertyChanged interface as an Observable,
    // that passes notifications about modified properties from the Model to the View.
    // In addition, the VM is also an Observer because it listens to the Model's notifications.
    class FlightViewModel : INotifyPropertyChanged
    {
        // The VM class composite the Model according the MVVM architecture.
        private IModel model;
        // Constructor.
        public FlightViewModel(IModel m)
        {
            // Using delgate property in order to notify the View.
            this.model = m;
            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_"+ e.PropertyName);
            };


        }

        // Calling the method disconnect of the Model.
        public void VM_Disconnect()
        {
            this.model.disconnect();
        }

        // calling the method connect of the Model.
        public void VM_Connect(string ip, int port)
        {
            this.model.connect(ip, port);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        // Properties: 
        // The purpose of each Getter is to get the notification from the Model to the View.
        // The purpose of each Setter is to pass commands from the View through the VM to the Model.
        private double aileron;
        public double VM_Aileron
        {
            get 
            {
                return this.aileron;
            }
            set
            {
                this.aileron = value;
                Thread t2 = new Thread(() => this.model.changeAileron(this.aileron));
                t2.Start();
            }
        }

        private double elevator;
        public double VM_Elevator
        {
            get 
            {
                return this.elevator;
            }
            set
            {
                this.elevator = value;
                Thread t3 = new Thread(() => this.model.changeElevator(this.elevator));
                t3.Start();
            }
        }

        private double throttle;
        public double VM_Throttle
        {
            get 
            {
                return this.throttle;            
            } 
            set
            {
                this.throttle = value;
                Thread t = new Thread(() => this.model.changeThrottle(this.throttle));
                t.Start();
            }
        }
        private double rudder;
        public double VM_Rudder
        {
            get 
            {
                return this.rudder;  
            }
            set
            {
                this.rudder = value;
                Thread t1 = new Thread(() => this.model.changeRudder(this.rudder));
                t1.Start();
            }
        }

        // The X & Y coordinates can't be set by the user, therefore their's property
        // includes only Getters.
        // The X coordinate.
        public double VM_Lat
        {
            get {
                              
                return model.lat; }
        }
        // The Y coordinate.
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

        public string VM_Dis
        {
            get { return model.dis; }
        }

        public string VM_TimeOut
        {
            get { return model.timeOut; }
        }

    }
}
