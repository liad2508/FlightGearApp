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
            this.air = 70;
            this.altitude = 30;
            
            
        }

        // implements all the Properties
        private double _elevator;
        public double elevator
        {
            get { return _elevator; }
            set
            {
                _elevator = value;
                NotifyPropertyChanged("Elevator");
            }
        }

        private double _aileron;
         public double aileron
         {
             get { return _aileron; }
             set
             {
                 _aileron = value;
                 NotifyPropertyChanged("Aileron");
             }
         }

        private double _throttle;
         public double throttle
         {
             get { return _throttle; }
             set
             {
                 _throttle = value;
                 NotifyPropertyChanged("Throttle");

             }
         }

        private double _rudder;
        public double rudder
        {
            get { return _rudder; }
            set
            {
                _rudder = value;
                NotifyPropertyChanged("Rudder");
            }
        }

        // The X coordinate
        private double x;
        public double lat
        { 
            get { return x; }
            set {
                x = value;
                NotifyPropertyChanged("Latitude");
            }
        }

        // The Y coordinate
        private double y;
        public double lon
        { 
            get { return y; }
            set {
                y = value;
                NotifyPropertyChanged("Longitude");
            }
        }

        private double headingDegree;
        public double heading_Degree
        {
            get { return headingDegree; }
            set
            {
                headingDegree = value;
                NotifyPropertyChanged("Heading Degree");
            }
        }

        private double _altitude;
        public double altitude 
        {
            get { return _altitude; }
            set
            {
                _altitude = value;
                NotifyPropertyChanged("Altitude");
            }
        }

        private double groundSpeed;
        public double ground_Speed
        {
            get { return groundSpeed; }
            set
            {
                groundSpeed = value;
                NotifyPropertyChanged("Ground Speed");
            }
        }

        private double _altimeter;
        public double altimeter
        {
            get { return _altimeter; }
            set 
            {
                _altimeter = value;
                NotifyPropertyChanged("Altimeter");
            }
        }

        private double verticalSpeed;
        public double vertical_Speed 
        { 
            get { return verticalSpeed; } 
            set
            {
                verticalSpeed = value;
                NotifyPropertyChanged("Vertical Speed");
            }
        }

        private double _pitch;
        public double pitch
        {
            get { return _pitch; } 
            set
            {
                _pitch = value;
                NotifyPropertyChanged("Pitch");
            }
        }

        private double air;
        public double airSpeed
        {
             get { return air; }
             set 
            {
                air = value;
                NotifyPropertyChanged("AirSpeed");
            }
        }
        private double _roll;
        public double roll
        {
            get { return _roll; }
            set
            {
                _roll = value;
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
                    Console.WriteLine("start loop");
                    airSpeed = 20;
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
