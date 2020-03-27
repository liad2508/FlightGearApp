using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using WpfApp2;

namespace FlightGearApp
{
    class FlightModel : IModel
    {
        private volatile Boolean stop;
        public Mutex MuTexLock = new Mutex();
        ITelnetClient telnetClient;
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly object balanceLock = new object();

        bool isLocked(object o)
        {
            if (!Monitor.TryEnter(o))
                return true;
            Monitor.Exit(o);
            return false;
        }

        public FlightModel(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
            this.stop = false;
            //this.airSpeed = 10;
            //this.airSpeed = 5;
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
                NotifyPropertyChanged("HeadingDegree");
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
                NotifyPropertyChanged("GroundSpeed");
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
                NotifyPropertyChanged("VerticalSpeed");
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
                Console.WriteLine(air);                
                NotifyPropertyChanged("AirSpeed");
                Console.WriteLine("after notify");
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
            startFromServer();            
        }

        public void disconnect()
        {
            this.stop = true;

            this.telnetClient.disconnect();
        }

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

                                /*
                                double k = 0;
                                for (int i = 0; i < 10; i++)
                                {

                                    this.telnetClient.write("set /position/latitude-deg " + k.ToString() + "\r\n");
                                    this.telnetClient.write("get /position/latitude-deg\r\n");
                                    s = (getBetween(telnetClient.read(), "=", "(double)")).Trim();
                                    len = s.Length;
                                    lat = Double.Parse(s.Substring(1, len - 2));

                                    this.telnetClient.write("set /position/longitude-deg " + k.ToString() + "\r\n");
                                    this.telnetClient.write("get /position/longitude-deg\r\n");
                                    s = (getBetween(telnetClient.read(), "=", "(double)")).Trim();
                                    len = s.Length;
                                    lon = Double.Parse(s.Substring(1, len - 2));
                                    k += 0.2;
                                }*/



                                this.telnetClient.write("set /instrumentation/airspeed-indicator/indicated-speed-kt 10\r\n");
                                //this.telnetClient.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\r\n");
                                s = (getBetween(telnetClient.read(), "=", "(double)")).Trim();
                                len = s.Length;
                                this.airSpeed = Double.Parse(s.Substring(1, len - 2));
                                //airSpeed = new airSpeed;


                                this.telnetClient.write("get /controls/flight/aileron 20\r\n");
                                this.telnetClient.write("get /controls/flight/aileron\r\n");
                                s = (getBetween(telnetClient.read(), "=", "(double)")).Trim();
                                len = s.Length;
                                aileron = Double.Parse(s.Substring(1, len - 2));

                                // try
                                this.telnetClient.write("set /instrumentation/airspeed-indicator/indicated-speed-kt 15\r\n");
                                //this.telnetClient.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\r\n");
                                s = (getBetween(telnetClient.read(), "=", "(double)")).Trim();
                                len = s.Length;
                                this.airSpeed = Double.Parse(s.Substring(1, len - 2));
                                //



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

                                this.telnetClient.write("set /position/latitude-deg 0\r\n");
                                this.telnetClient.write("get /position/latitude-deg\r\n");
                                s = (getBetween(telnetClient.read(), "=", "(double)")).Trim();
                                len = s.Length;
                                lat = Double.Parse(s.Substring(1, len - 2));

                                this.telnetClient.write("set /position/longitude-deg 0\r\n");
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


                                this.telnetClient.write("set /position/latitude-deg 300\r\n");
                                this.telnetClient.write("get /position/latitude-deg\r\n");
                                s = (getBetween(telnetClient.read(), "=", "(double)")).Trim();
                                len = s.Length;
                                lat = Double.Parse(s.Substring(1, len - 2));

                                this.telnetClient.write("set /position/longitude-deg 200\r\n");
                                this.telnetClient.write("get /position/longitude-deg\r\n");
                                s = (getBetween(telnetClient.read(), "=", "(double)")).Trim();
                                len = s.Length;
                                lon = Double.Parse(s.Substring(1, len - 2));

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


            //MuTexLock.WaitOne();
            //{

            //  if (!isLocked(balanceLock))
            // {

            //  lock (balanceLock)
            //  {
            MuTexLock.WaitOne();
            this.telnetClient.write("set /controls/engines/current-engine/throttle " + throttle.ToString() + "\r\n");
                    this.telnetClient.read();
               // }
           // }
                //}
            MuTexLock.ReleaseMutex();
            

        }

        public void changeAileron(double aileron)
        {
            MuTexLock.WaitOne();
            this.telnetClient.write("set /controls/flight/aileron " + aileron.ToString() + "\r\n");
            this.telnetClient.read();
            Console.WriteLine("change telnet  aileronnnnnnn " + aileron.ToString());
            MuTexLock.ReleaseMutex();
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

        public void changeRudder(double rudder)
        {
            MuTexLock.WaitOne();
            this.telnetClient.write("set /controls/flight/rudder " + rudder.ToString() + "\r\n");
            this.telnetClient.read();
            MuTexLock.ReleaseMutex();
        }

        public void changeElevator(double elevator)
        {
            MuTexLock.WaitOne();
            this.telnetClient.write("set /controls/flight/elevator " + elevator.ToString() + "\r\n");
            this.telnetClient.read();
            MuTexLock.ReleaseMutex();
        }

        [Obsolete]
        public void startFromServer()
        {
            new Thread(delegate ()
            {
                try
                {
                    while (!stop)
                    {
                        MuTexLock.WaitOne();
                        Console.WriteLine("serverrr");
                        string s;

                        this.telnetClient.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\r\n");
                        s = telnetClient.read();
                        Console.WriteLine(s);
                        this.airSpeed = Double.Parse(s);

                        this.telnetClient.write("get /controls/flight/aileron\r\n");
                        s = telnetClient.read();
                        aileron = Double.Parse(s);

                        this.telnetClient.write("get /controls/flight/elevator\r\n");
                        s = telnetClient.read();
                        elevator = Double.Parse(s);

                        this.telnetClient.write("get /controls/flight/rudder\r\n");
                        s = telnetClient.read();
                        rudder = Double.Parse(s);

                        this.telnetClient.write("get /controls/engines/current-engine/throttle\r\n");
                        s = telnetClient.read();
                        throttle = Double.Parse(s);

                        this.telnetClient.write("get /position/latitude-deg\r\n");
                        s = telnetClient.read();
                        lat = Double.Parse(s);

                        this.telnetClient.write("get /position/longitude-deg\r\n");
                        s = telnetClient.read();
                        lon = Double.Parse(s);

                        this.telnetClient.write("get /instrumentation/heading-indicator/indicated-heading-deg\r\n");
                        s = telnetClient.read();
                        heading_Degree = Double.Parse(s);


                        this.telnetClient.write("get /instrumentation/gps/indicated-altitude-ft\r\n");
                        s = telnetClient.read();
                        altitude = Double.Parse(s);


                        this.telnetClient.write("get /instrumentation/gps/indicated-ground-speed-kt\r\n");
                        s = telnetClient.read();
                        ground_Speed = Double.Parse(s);


                        this.telnetClient.write("get /instrumentation/altimeter/indicated-altitude-ft\r\n");
                        s = telnetClient.read();
                        altimeter = Double.Parse(s);


                        this.telnetClient.write("get /instrumentation/gps/indicated-vertical-speed\r\n");
                        s = telnetClient.read();
                        vertical_Speed = Double.Parse(s);


                        this.telnetClient.write("get /instrumentation/attitude-indicator/internal-pitch-deg\r\n");
                        s = telnetClient.read();
                        pitch = Double.Parse(s);

                        this.telnetClient.write("get /instrumentation/attitude-indicator/internal-roll-deg\r\n");
                        s = telnetClient.read(); ;
                        roll = Double.Parse(s);


                        this.telnetClient.write("get /position/latitude-deg\r\n");
                        s = telnetClient.read();
                        lat = Double.Parse(s);


                        this.telnetClient.write("get /position/longitude-deg\r\n");
                        s = telnetClient.read();
                        lon = Double.Parse(s);
                        //}

                        //}
                        MuTexLock.ReleaseMutex();
                        Thread.Sleep(250);
                    }
                } catch(Exception e)
                {
                    
                    string messageBoxText = "Do you want to connect again?";
                    string caption = "Error Connection";
                    MessageBoxButton button = MessageBoxButton.YesNo;
                    MessageBoxImage icon = MessageBoxImage.Warning;
                    // Display message box
                    MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                   
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            //disconnect();
                            Application.Current.Dispatcher.Invoke((Action)delegate
                            {
                                MainWindow window = new MainWindow();
                                //App.Current.second.Close();
                                App.Current.MainWindow = window;
                                App.Current.MainWindow.Show();
                            });
                            break;
                        case MessageBoxResult.No:
                            disconnect();
                            Environment.Exit(0);
                            break;
                    }
                    }
            }).Start();
        }
    }
}
