﻿using FlightGearApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FlightGearApp.model;
using Microsoft.Maps.MapControl.WPF;
using System.Threading;
using System.ComponentModel;
using System.Configuration;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for second.xaml
    /// </summary>
    public partial class second : Window
    {
        private FlightViewModel vm;
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        const Double BOARDER_UP = 45;
        const Double BOARDER_RIGHT = 45;

        [Obsolete]
        public second()
        {
            InitializeComponent();
            this.vm = new FlightViewModel(new FlightModel(new ConcreteTelnetClient()));
            //myMap.Mode = new AerialMode(true);           
           
            string Ip = ConfigurationSettings.AppSettings["Ip"]; 
            int port = Int32.Parse(ConfigurationSettings.AppSettings["Port"]); 

            string userIp = ((MainWindow)Application.Current.MainWindow).tb1.Text;
            string userPort = ((MainWindow)Application.Current.MainWindow).tb2.Text;
            if (!userIp.Equals(""))
            {
                Ip = userIp;
            }
            if (!userPort.Equals(""))
            {
                port = Int32.Parse(userPort);
            }
            
            DataContext = vm;                       
            vm.VM_Connect(Ip, port);
            updateView();
            updateJoystick();
        }

        public void updateJoystick()
        {
            this.joystick.positionchange += delegate (object sender, PositionChangeEventArgs e)
            {
                {
                    
                        if (e.positionChanged.Equals("X"))
                        {
                            double rudder = joystick.PositionX / BOARDER_RIGHT;
                            //Dispatcher.BeginInvoke(
                            //new ThreadStart(() => 
                            vm.VM_Rudder = rudder;
                            //));
                            rudder_tag.Text = rudder.ToString();


                        }
                        if (e.positionChanged.Equals("Y"))
                        {
                            double elevator = joystick.PositionY / BOARDER_UP;
                            // Dispatcher.BeginInvoke(
                            //new ThreadStart(() =>
                            vm.VM_Elevator = elevator;
                            //));
                            elevator_tag.Text = elevator.ToString();
                        }
                   
                }
            };
        }

        public void updateView()
        {
            this.vm.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                {
                    this.Dispatcher.Invoke(() =>
                    {


                        if (e.PropertyName.Equals("VM_AirSpeed"))
                        {
                            //Dispatcher.BeginInvoke(
                            //new ThreadStart(() =>
                            air_tag.Value = vm.VM_AirSpeed;
                            //));

                        }
                        if (e.PropertyName.Equals("VM_VerticalSpeed"))
                        {
                            //Dispatcher.BeginInvoke(
                            //new ThreadStart(() =>
                            ver_speed.Value = vm.VM_VerticalSpeed;
                            //));
                        }

                        if (e.PropertyName.Equals("VM_HeadingDegree"))
                        {
                            // Dispatcher.BeginInvoke(
                            // new ThreadStart(() => 
                            head_deg.Value = vm.VM_HeadingDegree;
                            //));
                        }

                        if (e.PropertyName.Equals("VM_Roll"))
                        {
                            //Dispatcher.BeginInvoke(
                            //new ThreadStart(() =>
                            roll_tag.Value = vm.VM_Roll;
                            //));
                        }

                        if (e.PropertyName.Equals("VM_Altimeter"))
                        {
                            // Dispatcher.BeginInvoke(
                            //new ThreadStart(() =>
                            altimeter_tag.Value = vm.VM_Altimeter;
                            //)) ;
                        }

                        if (e.PropertyName.Equals("VM_Pitch"))
                        {
                            //Dispatcher.BeginInvoke(
                            //new ThreadStart(() => 
                            pitch_tag.Value = vm.VM_Pitch;
                            //));
                        }

                        if (e.PropertyName.Equals("VM_Altitude"))
                        {
                            //Dispatcher.BeginInvoke(
                            //new ThreadStart(() =>
                            altitude_tag.Value = vm.VM_Altitude;
                            //));
                        }

                        if (e.PropertyName.Equals("VM_GroundSpeed"))
                        {
                            //Dispatcher.BeginInvoke(
                            //new ThreadStart(() =>
                            ground_tag.Value = vm.VM_GroundSpeed;
                            //));
                        }

                        if (e.PropertyName.Equals("VM_Lat") || e.PropertyName.Equals("VM_Lon"))
                        {
                            //this.Dispatcher.Invoke(() =>
                            // { 
                            if (vm.VM_Lat > -81 && vm.VM_Lat < 81 && vm.VM_Lon > -180 && vm.VM_Lon < 180)
                            {
                                lat_tag.Foreground = Brushes.Green;
                                lat_tag.Text = "Latitude: " + vm.VM_Lat
                                + ", Longtitude: " + vm.VM_Lon;
                            }
                            else
                            {
                                lat_tag.Foreground = Brushes.Red;
                                lat_tag.Text = "Invalid airplane coordinates";
                            }

                            MapLayer.SetPosition(plane, new Location(vm.VM_Lat, vm.VM_Lon));

                            //});
                        }

                        // end of 1st dispatcher invoke
                    });


                };
            };
        }


        private void Vm_PropertyChanged1(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Joystick_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Activated(object sender, EventArgs e)
        {

        }

        private void Joystick_MouseEnter(object sender, MouseEventArgs e)
        {
            li.FontSize = 10;
        }

        private void Joystick_MouseLeave(object sender, MouseEventArgs e)
        {
            li.FontSize = 30;
        }




    }
}
