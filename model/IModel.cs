using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightGearApp
{
    interface IModel
    {
        void connect(string ip, int port);
        void disconnect();
        void start();  
        
        double elevator { set; get; }
        double aileron { set; get; }
        // check if need more properties

        // we need to add functions to the interface
    }
}
