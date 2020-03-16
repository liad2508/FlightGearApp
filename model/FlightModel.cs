using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public double elevator
        {
            get { return elevator; }
            set
            {
                elevator = value;
                NotifyPropertyChanged("elevator");
            }
        }
            
        public double aileron
        {
            get { return aileron; }
            set
            {
                aileron = value;
                NotifyPropertyChanged("aileron");
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                // using downcasting for the object PropertyChangedEventArgs
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
                    this.telnetClient.write("get /controls/flight/aileron");
                    aileron = Double.Parse(telnetClient.read());
                    this.telnetClient.write("get /controls/flight/elevator");
                    elevator = Double.Parse(telnetClient.read());
                    // TODO - maybe we will add more properties
                    Thread.Sleep(250);
                }

            }).Start();
        }
    }
}
