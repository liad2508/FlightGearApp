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

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl
    {
        public Joystick()
        {
            InitializeComponent();
        }
        private void centerKnob_Completed(object sender, EventArgs e) { }
        private Point firstPoint = new Point();
        
      

        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.firstPoint = e.GetPosition(this);
                Knob.CaptureMouse();                  
            }           
        }

        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {            
            knobPosition.X = 0;
            knobPosition.Y = 0;
            Knob.ReleaseMouseCapture();

        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
           if (e.LeftButton == MouseButtonState.Pressed)
            {
                double x = e.GetPosition(this).X - this.firstPoint.X;
                double y = e.GetPosition(this).Y - this.firstPoint.Y;
                if (Math.Sqrt(x * x + y * y) < Base.Width / 6)
                {
                    knobPosition.X = x;
                    knobPosition.Y = y;
                }  
            }
        }        
    }
}
