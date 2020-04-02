using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1;
using WpfApp2;

namespace FlightGearApp.model
{
    // The following class is a concrete class that implements the ITelnetClient interface.
    class ConcreteTelnetClient : ITelnetClient
    {
        private TcpClient _client;
        private NetworkStream _ns;

       // A property.
       private Boolean isc = false;
       public Boolean isConnect { 
            get
            {
                return isc;
            }
            set
            {
                isc = value;
            }
        
        }

        // The following will make a connec between the server and the Model of
        // the program/application.
        public void connect(string ip, int port)
        {
            // Using try & catch to deal with a case of error in connection.
            try
            {
                _client = new TcpClient("localhost", port);
                _ns = _client.GetStream();
                isc = true;

            }
            // catch the exception.
            catch (Exception e)
            {
                string messageBoxText = "Please connect again";
                string caption = "Error Connection";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                // Display message box
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                if (result == MessageBoxResult.OK)
                {
                    // The following loops cleans all the opened windows.
                    for (int intCounter = App.Current.Windows.Count - 1; intCounter > 0; intCounter--)
                        App.Current.Windows[intCounter].Close();
                }
            }
        }

        // The following method close the connection between the program and the server.
        public void disconnect()
        {
            _ns.Close();
            _client.Close();
            // The following loops cleans all the opened windows.
            for (int intCounter = App.Current.Windows.Count - 1; intCounter > 0; intCounter--)
                App.Current.Windows[intCounter].Close();
        }

        // The followin method reads data from the server.
        public string read()
        {
            try
            {
                string retval;                
                byte[] bytes = new byte[1024];
                _ns.ReadTimeout = 2000;
                int bytesRead = _ns.Read(bytes, 0, bytes.Length);
                retval = Encoding.ASCII.GetString(bytes, 0, bytesRead);
                return retval;
            } catch (Exception e)
            {
                e.ToString();
                Console.WriteLine("time out read");
                return "Time Out 10s";
            }
        }

        // The followng method write commands to the server in order to recieve/read
        // data from the server.
        public void write(string command)
        {
            try
            {
                byte[] bytes = new byte[1024];
                bytes = Encoding.ASCII.GetBytes(command);
                _ns.WriteTimeout = 2000;
                _ns.Write(bytes, 0, bytes.Length);
            } catch (Exception e)
            {
                e.ToString();
                Console.WriteLine("wait to write to server");
            }
        }
    }
}
