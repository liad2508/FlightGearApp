using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FlightGearApp.model
{
    class ConcreteTelnetClient : ITelnetClient
    {
        private TcpClient _client;
        private NetworkStream _ns;


        // This method connects the simulator to the program
        public void connect(string ip, int port)
        {           
            _client = new TcpClient("localhost", port);
            _ns = _client.GetStream();
        }


        public void disconnect()
        {
            _ns.Close();
            _client.Close();
        }

        // This menthod get the data form the simulator
        public string read()
        {
            
            string retval;
            _ns.ReadTimeout = 10000;
            byte[] bytes = new byte[1024];
            int bytesRead = _ns.Read(bytes, 0, bytes.Length);
            retval = Encoding.ASCII.GetString(bytes, 0, bytesRead);
            return retval;
        }

        public void write(string command)
        {
            
            byte[] bytes = new byte[1024];
            bytes = Encoding.ASCII.GetBytes(command);
            _ns.Write(bytes, 0, bytes.Length);
        }
    }
}
