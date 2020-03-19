using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightGearApp
{
    interface IModel : INotifyPropertyChanged
    {
        void connect(string ip, int port);
        void disconnect();
        void start();  
        
        // Declaring all the Properties
        // The forth Joystick Properties:
        double elevator { set; get; }
        double aileron { set; get; }
        double throttle { set; get; }
        double rudder { set; get; }

        // coordinates Properties
        double lat { set; get; }
        double lon { set; get; }

        // 4X2 matrix properties
        double heading_Degree { set; get; }
        double altitude { set; get; }
        double ground_Speed { set; get; }
        double altimeter { set; get; }
        double vertical_Speed { set; get; }
        double pitch { set; get; }
        double airSpeed { set; get; }
        double roll { set; get; }

        void setRudder(double rudderVal);
        void setThrottle(double thrVal);
        void setElevator(double eleVal);
        void setAileron(double aileronVal);

        // we need to add functions to the interface
    }
}
