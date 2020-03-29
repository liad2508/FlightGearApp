using FlightGearApp;
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
            int n;
            if (!userPort.Equals(""))
            {
                bool isNumeric = int.TryParse(userPort, out n);
                if (isNumeric)
                {
                    port = Int32.Parse(userPort);
                }
                else
                {

                    port = 01;
                }
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


                        if (e.PropertyName.Equals("VM_Dis"))
                        {                           
                            this.Close();                            
                            this.Owner.Show();
                        }


                        if (e.PropertyName.Equals("VM_AirSpeed"))
                        {    
                            if (vm.VM_AirSpeed.Equals("ERR\n") || vm.VM_AirSpeed.Equals("err\n"))
                            {
                                text_airspeed.Foreground = Brushes.Red;
                                text_airspeed.Text = "ERR";
                            }
                            else
                            {
                                air_tag.Value = Double.Parse(vm.VM_AirSpeed);
                            }             

                        }
                        if (e.PropertyName.Equals("VM_VerticalSpeed"))
                        {
                            if (vm.VM_VerticalSpeed.Equals("ERR\n") || vm.VM_VerticalSpeed.Equals("err\n"))
                            {
                                text_verticalspeed.Foreground = Brushes.Red;
                                text_verticalspeed.Text = "ERR";

                       
                            } else
                            {
                                ver_speed.Value = Double.Parse(vm.VM_VerticalSpeed);
                            }
                            //)); 
                        }

                        if (e.PropertyName.Equals("VM_HeadingDegree"))
                        {
                            if (vm.VM_HeadingDegree.Equals("ERR\n") || vm.VM_HeadingDegree.Equals("err\n"))
                            {
                                text_headdeg.Foreground = Brushes.Red;
                                text_headdeg.Text = "ERR";
                            }
                            else
                            {
                                head_deg.Value = Double.Parse(vm.VM_HeadingDegree);
                            }
                           
                        }

                        if (e.PropertyName.Equals("VM_Roll"))
                        {
                            if (vm.VM_Roll.Equals("ERR\n") || vm.VM_Roll.Equals("err\n"))
                            {
                                text_roll.Foreground = Brushes.Red;
                                text_roll.Text = "ERR";
                            }
                            else
                            {
                                
                                roll_tag.Value = Double.Parse(vm.VM_Roll);
                            }
                           
                        }

                        if (e.PropertyName.Equals("VM_Altimeter"))
                        {
                            if (vm.VM_Altimeter.Equals("ERR\n") || vm.VM_Altimeter.Equals("err\n"))
                            {
                                text_altimeter.Foreground = Brushes.Red;
                                text_altimeter.Text = "ERR";
                            }
                            else
                            {
                                altimeter_tag.Value = Double.Parse(vm.VM_Altimeter);
                            }
                
                        }

                        if (e.PropertyName.Equals("VM_Pitch"))
                        {
                            if (vm.VM_Pitch.Equals("ERR\n") || vm.VM_Pitch.Equals("err\n"))
                            {
                                text_pitch.Foreground = Brushes.Red;
                                text_pitch.Text = "ERR";
                            } 
                            else {

                                pitch_tag.Value = Double.Parse(vm.VM_Pitch);

                            }
                            
                        }

                        if (e.PropertyName.Equals("VM_Altitude"))
                        {
                            if (vm.VM_Altitude.Equals("ERR\n") || vm.VM_Altitude.Equals("err\n"))
                            {
                                text_altitude.Foreground = Brushes.Red;
                                text_altitude.Text = "ERR";
                                
                            }
                            else
                            {
             
                                altitude_tag.Value = Double.Parse(vm.VM_Altitude);
                            }
                            
                        }

                        if (e.PropertyName.Equals("VM_GroundSpeed"))
                        {
                            if (vm.VM_GroundSpeed.Equals("ERR\n") || vm.VM_GroundSpeed.Equals("err\n"))
                            {
                                text_ground.Foreground = Brushes.Red;
                                text_ground.Text = "ERR";
                            }
                            else
                            {
                                ground_tag.Value = Double.Parse(vm.VM_GroundSpeed);
                            }
                           
                        }

                        if (e.PropertyName.Equals("VM_Lat") || e.PropertyName.Equals("VM_Lon"))
                        {
                         
                            if (vm.VM_Lat > -90 && vm.VM_Lat < 90 && vm.VM_Lon > -180 && vm.VM_Lon < 180)
                            {
                                lat_tag.Foreground = Brushes.Green;
                                lat_tag.Text = "Latitude: " + vm.VM_Lat
                                + ", Longtitude: " + vm.VM_Lon;
                                MapLayer.SetPosition(plane, new Location(vm.VM_Lat, vm.VM_Lon));
                            }
                            else
                            {
                                lat_tag.Foreground = Brushes.Red;
                                lat_tag.Text = "Invalid airplane coordinates";
                            }                            

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.vm.VM_Disconnect();
            this.Owner.Show();
        }
    }
}
