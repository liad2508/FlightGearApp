using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp2;



namespace WpfApp1.view
{
    public partial class Controls : UserControl1
    {

        const Double BOARDER_UP = 45;
        const Double BOARDER_DOWN = -45;
        public Joystick joystick;

        public Controls()
        {
            InitializeComponent();
            this.joystick.positionchange += delegate (object sender, PositionChangeEventArgs e)
            {
                if (e.positionChanged.Equals("X"))
                {
                    double pos = joystick.PositionX;
                    double rudder = joystick.PositionX / BOARDER_UP;
                }
                // UPDATE THE ViewModel
                else if (e.positionChanged.Equals("Y")) 
                {
                    double pos = joystick.PositionY;
                    double elevator = joystick.PositionY / BOARDER_UP;
                    
                }
            };



        }


    }
}
