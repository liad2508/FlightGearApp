using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FlightGearApp
{
    class FlightModel : IModel
    {
        private volatile Boolean stop;
        ITelnetClient telnetClient;
        public event PropertyChangedEventHandler PropertyChanged;

        public FlightModel(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
            this.stop = false;
        }

        // implements all the Properties
        public double elevator
        {
            get { return elevator; }
            set
            {
                elevator = value;
                NotifyPropertyChanged("Elevator");
            }
        }

       /* public double aileron
        {
            get { return aileron; }
            set
            {
                aileron = value;
                NotifyPropertyChanged("Aileron");
            }
        }*/

       /*
        public double throttle
        {
            get { return throttle; }
            set
            {
                throttle = value;
                NotifyPropertyChanged("Throttle");
                
            }
        }*/

        public double rudder
        {
            get { return rudder; }
            set
            {
                //rudder = value;
                NotifyPropertyChanged("Rudder");
            }
        }

        // The X coordinate
        public double lat
        { 
            get { return lat; }
            set {
                lat = value;
                NotifyPropertyChanged("Latitude");
            }
        }

        // The Y coordinate
        public double lon
        { 
            get { return lon; }
            set {
                lon = value;
                NotifyPropertyChanged("Longitude");
            }
        }

        public double heading_Degree
        {
            get { return heading_Degree; }
            set
            {
                heading_Degree = value;
                NotifyPropertyChanged("Heading Degree");
            }
        }

        public double altitude 
        {
            get { return altitude; }
            set
            {
                altitude = value;
                NotifyPropertyChanged("Altitude");
            }
        }

        public double ground_Speed
        {
            get { return ground_Speed; }
            set
            {
                ground_Speed = value;
                NotifyPropertyChanged("Ground Speed");
            }
        }

        public double altimeter
        {
            get { return altimeter; }
            set 
            {
                altimeter = value;
                NotifyPropertyChanged("Altimeter");
            }
        }

        public double vertical_Speed 
        { 
            get { return vertical_Speed; } 
            set
            {
                vertical_Speed = value;
                NotifyPropertyChanged("Vertical Speed");
            }
        }

        public double pitch
        {
            get { return pitch; } 
            set
            {
                pitch = value;
                NotifyPropertyChanged("Pitch");
            }
        }

        public double airSpeed
        {
            get { return airSpeed; }
            set 
            {
                airSpeed = value;
                NotifyPropertyChanged("AirSpeed");
            }
        }
        public double roll
        {
            get { return roll; }
            set
            {
                roll = value;
                NotifyPropertyChanged("Roll");
            } 
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public void connect(string ip, int port)
        {
            this.telnetClient.connect(ip, port);            
        }

        public void disconnect()
        {
            this.stop = true;
            this.telnetClient.disconnect();
        }

        public void start()
        {
            new Thread(delegate ()
            {
                while (!stop)
                {
                    // Gets:
                    this.telnetClient.write("get /controls/flight/aileron \n");
                    //aileron = Double.Parse(telnetClient.read());
                    this.telnetClient.write("get /controls/flight/elevator \n");
                    elevator = Double.Parse(telnetClient.read());
                    this.telnetClient.write("get /controls/flight/rudder \n");
                    rudder = Double.Parse(telnetClient.read());
                    this.telnetClient.write("get /controls/engines/current-engine/throttle \n");
                    //throttle = Double.Parse(telnetClient.read());
                    this.telnetClient.write("get /position/latitude-deg \n");
                    lat = Double.Parse(telnetClient.read());
                    this.telnetClient.write("get /position/longitude-deg \n");
                    lon = Double.Parse(telnetClient.read());
                    this.telnetClient.write("get /instrumentation/heading-indicator/indicated-heading-deg \n");
                    heading_Degree = Double.Parse(telnetClient.read());
                    this.telnetClient.write("get /instrumentation/gps/indicated-altitude-ft \n");
                    altitude = Double.Parse(telnetClient.read());
                    this.telnetClient.write("get /instrumentation/gps/indicated-ground-speed-kt \n");
                    ground_Speed = Double.Parse(telnetClient.read());
                    this.telnetClient.write("get /instrumentation/altimeter/indicated-altitude-ft \n");
                    altimeter = Double.Parse(telnetClient.read());
                    this.telnetClient.write("get /instrumentation/gps/indicated-vertical-speed \n");
                    vertical_Speed = Double.Parse(telnetClient.read());
                    this.telnetClient.write("get /instrumentation/attitude-indicator/internal-pitch-deg \n");
                    pitch = Double.Parse(telnetClient.read());
                    this.telnetClient.write("get /instrumentation/airspeed-indicator/indicated-speed-kt \n");
                    airSpeed = Double.Parse(telnetClient.read());
                    this.telnetClient.write("get /instrumentation/attitude-indicator/internal-roll-deg \n");
                    roll = Double.Parse(telnetClient.read());

                    // Sets: check it
                    //this.telnetClient.write("set /controls/flight/aileron \n");
                    Thread.Sleep(250);
                }

            }).Start();
        }

        // Sets function for the 4th JoyStick's Properties
        public void setRudder(double rudderVal)
        {
            rudder = rudderVal;
            this.telnetClient.write("set /controls/flight/rudder" + rudderVal.ToString() + "\n");
        }

        /*
        public void setThrottle(double thrVal)
        {
            throttle = thrVal;
            this.telnetClient.write("set /controls/engines/current-engine/throttle" + thrVal.ToString() + "\n");
        }*/

        public void setElevator(double eleVal)
        {
            elevator = eleVal;
            this.telnetClient.write("set /controls/flight/elevator" + eleVal.ToString() + "\n");
        }

        /*public void setAileron(double aileronVal)
        {
            aileron = aileronVal;
            this.telnetClient.write("set /controls/flight/aileron" + aileronVal.ToString() + "\n");
        }*/

        public void changeThrottle(double throttle)
        {
            Console.WriteLine("bla bla");
            this.telnetClient.write("set /controls/engines/current-engine/throttle" + throttle.ToString() + "\n");
            
        }

        public void changeAileron(double aileron)
        {
            Console.WriteLine("hiiii");
            this.telnetClient.write("set /controls/flight/aileron" + aileron.ToString() + "\n");
        }
    }
}
