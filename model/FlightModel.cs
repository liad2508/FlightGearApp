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
            this.airSpeed = 65;
            this.lat = 0;
            this.lon = 0;        
                        
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
                NotifyPropertyChanged("Lat");
            }
        }

        // The Y coordinate
        private double y;
        public double lon
        { 
            get { return y; }
            set {
                y = value;
                NotifyPropertyChanged("Lon");
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
                    string s;
                    int len;
                    this.telnetClient.write("set /instrumentation/airspeed-indicator/indicated-speed-kt 10\r\n");
                    this.telnetClient.write("get airspeed-indicator_indicated-speed-kt\r\n");           
                    s = (getBetween(telnetClient.read(), "=", "(double)")).Trim();
                    len = s.Length;                    
                    airSpeed = Double.Parse(s.Substring(1,len-2));
                    

                    this.telnetClient.write("get /controls/flight/aileron\r\n");
                    s = (getBetween(telnetClient.read(), "=", "(double)")).Trim();
                    len = s.Length;
                    aileron = Double.Parse(s.Substring(1, len - 2));


                    this.telnetClient.write("get /controls/flight/elevator\r\n");                   
                    s = (getBetween(telnetClient.read(), "=", "(double)")).Trim();
                    len = s.Length;
                    elevator = Double.Parse(s.Substring(1, len - 2));



                    this.telnetClient.write("get /controls/flight/rudder\r\n");
                    s = (getBetween(telnetClient.read(), "=", "(double)")).Trim();
                    len = s.Length;
                    rudder = Double.Parse(s.Substring(1, len - 2));


                    this.telnetClient.write("get /controls/engines/current-engine/throttle\r\n");
                    s = (getBetween(telnetClient.read(), "=", "(double)")).Trim();
                    len = s.Length;
                    throttle = Double.Parse(s.Substring(1, len - 2));

                    this.telnetClient.write("get /position/latitude-deg\r\n");
                    s = (getBetween(telnetClient.read(), "=", "(double)")).Trim();
                    len = s.Length;
                    lat = Double.Parse(s.Substring(1, len - 2));

                    this.telnetClient.write("get /position/longitude-deg\r\n");
                    s = (getBetween(telnetClient.read(), "=", "(double)")).Trim();
                    len = s.Length;
                    lon = Double.Parse(s.Substring(1, len - 2));


                    this.telnetClient.write("get /instrumentation/heading-indicator/indicated-heading-deg\r\n");
                    s = (getBetween(telnetClient.read(), "=", "(double)")).Trim();
                    len = s.Length;
                    heading_Degree = Double.Parse(s.Substring(1, len - 2));


                    this.telnetClient.write("get /instrumentation/gps/indicated-altitude-ft\r\n");
                    s = (getBetween(telnetClient.read(), "=", "(double)")).Trim();
                    len = s.Length;
                    altitude = Double.Parse(s.Substring(1, len - 2));


                    this.telnetClient.write("get /instrumentation/gps/indicated-ground-speed-kt\r\n");
                    s = (getBetween(telnetClient.read(), "=", "(double)")).Trim();
                    len = s.Length;
                    ground_Speed = Double.Parse(s.Substring(1, len - 2));


                    this.telnetClient.write("get /instrumentation/altimeter/indicated-altitude-ft\r\n");
                    s = (getBetween(telnetClient.read(), "=", "(double)")).Trim();
                    len = s.Length;
                    altimeter = Double.Parse(s.Substring(1, len - 2));


                    this.telnetClient.write("get /instrumentation/gps/indicated-vertical-speed\r\n");
                    s = (getBetween(telnetClient.read(), "=", "(double)")).Trim();
                    len = s.Length;
                    vertical_Speed = Double.Parse(s.Substring(1, len - 2));


                    this.telnetClient.write("get /instrumentation/attitude-indicator/internal-pitch-deg\r\n");
                    s = (getBetween(telnetClient.read(), "=", "(double)")).Trim();
                    len = s.Length;
                    pitch = Double.Parse(s.Substring(1, len - 2));
                                       
                    this.telnetClient.write("get /instrumentation/attitude-indicator/internal-roll-deg\r\n");
                    s = (getBetween(telnetClient.read(), "=", "(double)")).Trim();
                    len = s.Length;
                    roll = Double.Parse(s.Substring(1, len - 2));
                 
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
            
            this.telnetClient.write("set /controls/engines/current-engine/throttle " + throttle.ToString() + "\r\n");

        }

        public void changeAileron(double aileron)
        {
            
            this.telnetClient.write("set /controls/flight/aileron " + aileron.ToString() + "r\n");
        }

        private static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }
    }
}
