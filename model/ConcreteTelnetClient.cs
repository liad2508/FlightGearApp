﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp2;

namespace FlightGearApp.model
{
    class ConcreteTelnetClient : ITelnetClient
    {
        private TcpClient _client;
        private NetworkStream _ns;

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

        


        // This method connects the simulator to the program
        public void connect(string ip, int port)
        {
            try
            {
                _client = new TcpClient("localhost", port);
                _ns = _client.GetStream();
                isc = true;

            }
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
                    for (int intCounter = App.Current.Windows.Count - 1; intCounter > 0; intCounter--)
                        App.Current.Windows[intCounter].Close();
                    
                }

            }


        }


        public void disconnect()
        {
            _ns.Close();
            _client.Close();
            for (int intCounter = App.Current.Windows.Count - 1; intCounter > 0; intCounter--)
                App.Current.Windows[intCounter].Close();
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
