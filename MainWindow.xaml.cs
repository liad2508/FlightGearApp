using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Navigation;


namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///

    public partial class MainWindow : Window
    {
       
        private void connect_button_Click(object sender, RoutedEventArgs e)
        {
            if (true) {
                this.Hide();
                second sec = new second();
                sec.Owner = this;
                try
                {
                    sec.Show();
                } catch (Exception)
                {
                    this.Show();
                }           

            }
             
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {            
            Process.Start("chrome.exe", "https://github.com/liad2508/FlightGearApp");
            e.Handled = true;
        }

        private void Hyperlink_RequestNavigate1(object sender, RequestNavigateEventArgs e)
        {
            Process.Start("chrome.exe", "https://www.flightgear.org/");
            e.Handled = true;
        }
    }
}
