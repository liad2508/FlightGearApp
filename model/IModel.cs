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
        void startFromServer();
        //void startFromSimulator();
        
        // Declaring all the Properties
        // The forth Joystick Properties:
        double elevator { set; get; }
        //double aileron { set; get; }
        //double throttle { set; get; }
        double rudder { set; get; }

        // coordinates Properties
        double lat { set; get; }
        double lon { set; get; }

        // 4X2 matrix properties
        string heading_Degree { set; get; }
        string altitude { set; get; }
        string ground_Speed { set; get; }
        string altimeter { set; get; }
        string vertical_Speed { set; get; }
        string pitch { set; get; }
        string airSpeed { set; get; }
        string roll { set; get; }
        string dis { set; get; }
        string timeOut { set; get; }

        void setRudder(double rudderVal);
        //void setThrottle(double thrVal);
        void setElevator(double eleVal);
        //void setAileron(double aileronVal);

        // we need to add functions to the interface
        void changeThrottle(double throttle);
        void changeAileron(double aileron);
        void changeRudder(double rudder);
        void changeElevator(double elevator);
    }
}
