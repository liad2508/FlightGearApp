using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using WpfApp2;
using System.Windows.Threading;
using System.Windows.Media;

namespace FlightGearApp
{
    // A concrete class which implements the IModel interface.
    class FlightModel : IModel
    {
        // Fields:
        private volatile Boolean stop;
        private Boolean con = true;
        public Mutex MuTexLock = new Mutex();
        ITelnetClient telnetClient;
        public event PropertyChangedEventHandler PropertyChanged;

        // Constructor.
        public FlightModel(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
            this.stop = false; 
        }

        // implements all the Properties from the IModel interface:
        // Notice: in the setter of each property we notify about the
        // the property which was modified.
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

        // The X coordinate's property.
        private double x;
        public double lat
        { 
            get { return x; }
            set {
                x = value;
                NotifyPropertyChanged("Lat");
            }
        }

        // The Y coordinate property.
        private double y;
        public double lon
        { 
            get { return y; }
            set {
                y = value;
                NotifyPropertyChanged("Lon");
            }
        }

        private string headingDegree;
        public string heading_Degree
        {
            get { return headingDegree; }
            set
            {
                headingDegree = value;
                NotifyPropertyChanged("HeadingDegree");
            }
        }

        private string _altitude;
        public string altitude 
        {
            get { return _altitude; }
            set
            {
                _altitude = value;
                NotifyPropertyChanged("Altitude");
            }
        }

        private string groundSpeed;
        public string ground_Speed
        {
            get { return groundSpeed; }
            set
            {
                groundSpeed = value;
                NotifyPropertyChanged("GroundSpeed");
            }
        }

        private string _altimeter;
        public string altimeter
        {
            get { return _altimeter; }
            set 
            {
                _altimeter = value;
                NotifyPropertyChanged("Altimeter");
            }
        }

        private string verticalSpeed;
        public string vertical_Speed 
        { 
            get { return verticalSpeed; } 
            set
            {
                verticalSpeed = value;
                NotifyPropertyChanged("VerticalSpeed");
            }
        }

        private string _pitch;
        public string pitch
        {
            get { return _pitch; } 
            set
            {
                _pitch = value;
                NotifyPropertyChanged("Pitch");
            }
        }

        private string air;
        public string airSpeed
        {
             get { return air; }
             set 
            {
                air = value;               
                NotifyPropertyChanged("AirSpeed");
            }
        }
        private string _roll;
        public string roll
        {
            get { return _roll; }
            set
            {
                _roll = value;
                NotifyPropertyChanged("Roll");
            } 
        }

        private string _dis;
        public string dis
        {
            get { return _dis; }
            set
            {
                _dis = value;
                NotifyPropertyChanged("Dis");
            }
        }

        private string _time;
        public string timeOut
        {
            get { return _time; }
            set
            {
                _time = value;
                NotifyPropertyChanged("TimeOut");
            }
        }

        // By the following function we pass the notification about each property to the VM.
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        // Create a connecting between the Model and the server.
        public void connect(string ip, int port)
        {
            this.telnetClient.connect(ip, port);
            if (this.telnetClient.isConnect == true)
            {
                startFromServer();   
            }
        }

        // Disconnect the connecting between the Model and the server.
        public void disconnect()
        {
            this.stop = true;
            this.telnetClient.disconnect();
        }

        /* The following function will be used when the user would like to connect
         * to the Flight Gear Simulator.
        public void startFromSimulator()
        {
            new Thread(delegate ()
            {
                try
                {
                    while (!stop)
                    {
                        if (!isLocked(balanceLock))
                        {

                            lock (balanceLock)
                            {



                                // Gets: 
                                string s;
                                int len;

                                
                                string k = 0;
                                for (int i = 0; i < 10; i++)
                                {

                                    this.telnetClient.write("set /position/latitude-deg " + k.ToString() + "\r\n");
                                    this.telnetClient.write("get /position/latitude-deg\r\n");
                                    s = (getBetween(telnetClient.read(), "=", "(string)")).Trim();
                                    len = s.Length;
                                    lat = string.Parse(s.Substring(1, len - 2));

                                    this.telnetClient.write("set /position/longitude-deg " + k.ToString() + "\r\n");
                                    this.telnetClient.write("get /position/longitude-deg\r\n");
                                    s = (getBetween(telnetClient.read(), "=", "(string)")).Trim();
                                    len = s.Length;
                                    lon = string.Parse(s.Substring(1, len - 2));
                                    k += 0.2;
                                }



                                this.telnetClient.write("set /instrumentation/airspeed-indicator/indicated-speed-kt 10\r\n");
                                //this.telnetClient.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\r\n");
                                s = (getBetween(telnetClient.read(), "=", "(string)")).Trim();
                                len = s.Length;
                                this.airSpeed = string.Parse(s.Substring(1, len - 2));
                                //airSpeed = new airSpeed;


                                this.telnetClient.write("get /controls/flight/aileron 20\r\n");
                                this.telnetClient.write("get /controls/flight/aileron\r\n");
                                s = (getBetween(telnetClient.read(), "=", "(string)")).Trim();
                                len = s.Length;
                                aileron = string.Parse(s.Substring(1, len - 2));

                                // try
                                this.telnetClient.write("set /instrumentation/airspeed-indicator/indicated-speed-kt 15\r\n");
                                //this.telnetClient.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\r\n");
                                s = (getBetween(telnetClient.read(), "=", "(string)")).Trim();
                                len = s.Length;
                                this.airSpeed = string.Parse(s.Substring(1, len - 2));
                                //



                                this.telnetClient.write("get /controls/flight/elevator\r\n");
                                s = (getBetween(telnetClient.read(), "=", "(string)")).Trim();
                                len = s.Length;
                                elevator = string.Parse(s.Substring(1, len - 2));



                                this.telnetClient.write("get /controls/flight/rudder\r\n");
                                s = (getBetween(telnetClient.read(), "=", "(string)")).Trim();
                                len = s.Length;
                                rudder = string.Parse(s.Substring(1, len - 2));


                                this.telnetClient.write("get /controls/engines/current-engine/throttle\r\n");
                                s = (getBetween(telnetClient.read(), "=", "(string)")).Trim();
                                len = s.Length;
                                throttle = string.Parse(s.Substring(1, len - 2));

                                this.telnetClient.write("set /position/latitude-deg 0\r\n");
                                this.telnetClient.write("get /position/latitude-deg\r\n");
                                s = (getBetween(telnetClient.read(), "=", "(string)")).Trim();
                                len = s.Length;
                                lat = string.Parse(s.Substring(1, len - 2));

                                this.telnetClient.write("set /position/longitude-deg 0\r\n");
                                this.telnetClient.write("get /position/longitude-deg\r\n");
                                s = (getBetween(telnetClient.read(), "=", "(string)")).Trim();
                                len = s.Length;
                                lon = string.Parse(s.Substring(1, len - 2));


                                this.telnetClient.write("get /instrumentation/heading-indicator/indicated-heading-deg\r\n");
                                s = (getBetween(telnetClient.read(), "=", "(string)")).Trim();
                                len = s.Length;
                                heading_Degree = string.Parse(s.Substring(1, len - 2));


                                this.telnetClient.write("get /instrumentation/gps/indicated-altitude-ft\r\n");
                                s = (getBetween(telnetClient.read(), "=", "(string)")).Trim();
                                len = s.Length;
                                altitude = string.Parse(s.Substring(1, len - 2));


                                this.telnetClient.write("get /instrumentation/gps/indicated-ground-speed-kt\r\n");
                                s = (getBetween(telnetClient.read(), "=", "(string)")).Trim();
                                len = s.Length;
                                ground_Speed = string.Parse(s.Substring(1, len - 2));


                                this.telnetClient.write("get /instrumentation/altimeter/indicated-altitude-ft\r\n");
                                s = (getBetween(telnetClient.read(), "=", "(string)")).Trim();
                                len = s.Length;
                                altimeter = string.Parse(s.Substring(1, len - 2));


                                this.telnetClient.write("get /instrumentation/gps/indicated-vertical-speed\r\n");
                                s = (getBetween(telnetClient.read(), "=", "(string)")).Trim();
                                len = s.Length;
                                vertical_Speed = string.Parse(s.Substring(1, len - 2));


                                this.telnetClient.write("get /instrumentation/attitude-indicator/internal-pitch-deg\r\n");
                                s = (getBetween(telnetClient.read(), "=", "(string)")).Trim();
                                len = s.Length;
                                pitch = string.Parse(s.Substring(1, len - 2));

                                this.telnetClient.write("get /instrumentation/attitude-indicator/internal-roll-deg\r\n");
                                s = (getBetween(telnetClient.read(), "=", "(string)")).Trim();
                                len = s.Length;
                                roll = string.Parse(s.Substring(1, len - 2));


                                this.telnetClient.write("set /position/latitude-deg 300\r\n");
                                this.telnetClient.write("get /position/latitude-deg\r\n");
                                s = (getBetween(telnetClient.read(), "=", "(string)")).Trim();
                                len = s.Length;
                                lat = string.Parse(s.Substring(1, len - 2));

                                this.telnetClient.write("set /position/longitude-deg 200\r\n");
                                this.telnetClient.write("get /position/longitude-deg\r\n");
                                s = (getBetween(telnetClient.read(), "=", "(string)")).Trim();
                                len = s.Length;
                                lon = string.Parse(s.Substring(1, len - 2));

                            }

                        }
                        Thread.Sleep(250);
                    }
                } catch (Exception e)
                {
                    Console.WriteLine("eroroooorrrr");
                    string messageBoxText = "Do you want to save changes?";
                    string caption = "Word Processor";
                    MessageBoxButton button = MessageBoxButton.YesNoCancel;
                    MessageBoxImage icon = MessageBoxImage.Warning;
                    // Display message box
                    MessageBox.Show(messageBoxText, caption, button, icon);
                }
            }).Start();
        }*/


        // The following 4th Change functions will be called from the View through the VM
        // and finally to the Model in a case the user changed one of the following 4th components.
        // 1st fucntion.
        public void changeThrottle(double throttle)
        {
            if (this.con)
            {
                MuTexLock.WaitOne();
                this.telnetClient.write("set /controls/engines/current-engine/throttle " + throttle.ToString() + "\r\n");
                this.telnetClient.read();
                MuTexLock.ReleaseMutex();
            }
         

        }

        // 2nd function.
        public void changeAileron(double aileron)
        {
            if (this.con)
            {
                MuTexLock.WaitOne();
                this.telnetClient.write("set /controls/flight/aileron " + aileron.ToString() + "\r\n");
                this.telnetClient.read();
                Console.WriteLine("change telnet  aileronnnnnnn " + aileron.ToString());
                MuTexLock.ReleaseMutex();
            }
        }

        // 3rd function.
        public void changeRudder(double rudder)
        {
            if (this.con)
            {
                MuTexLock.WaitOne();
                this.telnetClient.write("set /controls/flight/rudder " + rudder.ToString() + "\r\n");
                this.telnetClient.read();
                MuTexLock.ReleaseMutex();
            }
        }

        // 4th function.
        public void changeElevator(double elevator)
        {
            if (this.con)
            {
                MuTexLock.WaitOne();
                this.telnetClient.write("set /controls/flight/elevator " + elevator.ToString() + "\r\n");
                this.telnetClient.read();
                MuTexLock.ReleaseMutex();
            }
        }

        // The following function will be in used in startFromSimulator in order to parse values.
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

        // The following function is the main function of our concrete Model class.
        // By using this function the Model reads from the server values per each property.
        // The values of each property will be sampled from the server and will be passed to the VM
        // and then to the View correspondingly.
        [Obsolete]
        public void startFromServer()
        {
            // Using a seperate thread for reading the values from the server.
            new Thread(delegate ()
            {
                // Using try & catch to deal with a case of crashing.
                try
                {
                    // A main loop of reading the values from the server.
                    while (!stop)
                    {
                        MuTexLock.WaitOne();                        
                        string s;

                        this.telnetClient.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\r\n");
                        s = telnetClient.read();
                        if (s.Equals("Time Out 10s"))
                        {
                            this.timeOut = s;                            
                        }
                        else
                        {
                            this.timeOut = "";
                            this.airSpeed = s;
                        }


                        this.telnetClient.write("get /instrumentation/heading-indicator/indicated-heading-deg\r\n");
                        s = telnetClient.read();
                         if (s.Equals("Time Out 10s"))
                        {
                            this.timeOut = s;                            
                        }
                        else
                        {
                            this.timeOut = "";
                            this.heading_Degree = s;
                        }

                        
                        this.telnetClient.write("get /instrumentation/gps/indicated-altitude-ft\r\n");
                        s = telnetClient.read();
                        if (s.Equals("Time Out 10s"))
                        {
                            this.timeOut = s;
                        }
                        else
                        {
                            this.timeOut = "";
                            this.altitude = s;
                        }

                        
                        this.telnetClient.write("get /instrumentation/gps/indicated-ground-speed-kt\r\n");
                        s = telnetClient.read();
                        if (s.Equals("Time Out 10s"))
                        {
                            this.timeOut = s;
                        }
                        else
                        {
                            this.timeOut = "";
                            this.ground_Speed = s;
                        }

                        
                        this.telnetClient.write("get /instrumentation/altimeter/indicated-altitude-ft\r\n");
                        s = telnetClient.read();
                        if (s.Equals("Time Out 10s"))
                        {
                            this.timeOut = s;
                        }
                        else
                        {
                            this.timeOut = "";
                            this.altimeter = s;
                        }

                        
                        this.telnetClient.write("get /instrumentation/gps/indicated-vertical-speed\r\n");
                        s = telnetClient.read();
                        if (s.Equals("Time Out 10s"))
                        {
                            this.timeOut = s;
                        }
                        else
                        {
                            this.timeOut = "";
                            this.vertical_Speed = s;
                        }


                       
                        this.telnetClient.write("get /instrumentation/attitude-indicator/internal-pitch-deg\r\n");
                        s = telnetClient.read();
                        if (s.Equals("Time Out 10s"))
                        {
                            this.timeOut = s;
                        }
                        else
                        {
                            this.timeOut = "";
                            this.pitch = s;
                        }

                        
                        this.telnetClient.write("get /instrumentation/attitude-indicator/internal-roll-deg\r\n");
                        s = telnetClient.read();
                        if (s.Equals("Time Out 10s"))
                        {
                            this.timeOut = s;
                        }
                        else
                        {
                            this.timeOut = "";
                            this.roll = s;
                        }


                        string t;
                        this.telnetClient.write("get /position/latitude-deg\r\n");
                        s = (telnetClient.read()).ToString();
                        this.telnetClient.write("get /position/longitude-deg\r\n");
                        t = (telnetClient.read()).ToString();

                        try
                        {
                            if ((!s.Equals("Time Out 10s")) && (!t.Equals("Time Out 10s")))
                            {
                                lat = Double.Parse(s);
                                lon = Double.Parse(t);
                            }

                        } catch (Exception e)
                        {
                            e.ToString();
                            this.timeOut = s;
                        }
              
                        
                        MuTexLock.ReleaseMutex();
                        Thread.Sleep(250);
                    }
                    // In a case of exception the user will be asked if he would like to connect again.
                } catch(Exception e)
                {
                    con = false;
                    string messageBoxText = "Do you want to connect again?";
                    string caption = "Error Connection";
                    MessageBoxButton button = MessageBoxButton.YesNo;
                    MessageBoxImage icon = MessageBoxImage.Warning;
                    // Display message box
                    MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                   
                    switch (result)
                    {
                        // The user chose Yes.
                        case MessageBoxResult.Yes:
                            dis = "error";
                            break;
                        // The user chose No.
                        case MessageBoxResult.No:                            
                            Environment.Exit(0);
                            break;
                    }
                    }
            }).Start();
        }
    }
}
