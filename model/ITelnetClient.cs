using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightGearApp
{
    // The following interface is used for the connection & the network between the 
    // Model and the server.
    interface ITelnetClient
    {
        // A property.
        Boolean isConnect { set; get; }
        // Functions declarations.
        void connect(string ip, int port);
        void write(string command);
        string read();
        void disconnect();
    }
}
