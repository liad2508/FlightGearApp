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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FlightGearApp;
using FlightGearApp.model;
using Microsoft.Maps.MapControl.WPF;


namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FlightViewModel vm;

        public MainWindow()
        {
            InitializeComponent();
            this.vm = new FlightViewModel(new FlightModel(new ConcreteTelnetClient()));            
            DataContext = vm;
            //Set the map mode to Aerial with labels
            myMap.Mode = new AerialMode(true);
            //vm.VM_Rudder = 0.5;
            FlightModel m = new FlightModel(new ConcreteTelnetClient());
            //m.connect("127.0.0.1", 5402);
            Console.WriteLine("hi");
            vm.VM_AirSpeed = 50;
            double b = vm.VM_Aileron;
         
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
