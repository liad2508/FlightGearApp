using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightGearApp
{
    interface ITelnetClient
    {
        Boolean isConnect { set; get; }
        void connect(string ip, int port);
        void write(string command);
        string read(); // blocking call
        void disconnect();
    }
}
