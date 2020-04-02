using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightGearApp
{
    // The following interface represents all the properties of the Model object of our application.
    // The IModel interface implements another interface called INotifyPropertyChanged, by
    // implementing it the Model will be an Observable of properties which have been changed.
    // A notify about the modified properties will be passed to the VM by using ,from the Model,
    // INotifyPropertyChanged interface as a part of the Observer Design Pattern.
    interface IModel : INotifyPropertyChanged
    {
        // Functions declarations:
        void connect(string ip, int port);
        void disconnect();
        void startFromServer();

        // A function for connecting to the flight gear simulator.
        //void startFromSimulator();
        
        // Declaring all the Properties.
        // The forth Joystick Properties:
        double elevator { set; get; }
        double rudder { set; get; }

        // coordinates Properties:
        double lat { set; get; }
        double lon { set; get; }

        // 4X2 matrix properties:
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


        // The following functions will be in used by commands from
        // the View through the VM to the Model. 
        void changeThrottle(double throttle);
        void changeAileron(double aileron);
        void changeRudder(double rudder);
        void changeElevator(double elevator);
    }
}
