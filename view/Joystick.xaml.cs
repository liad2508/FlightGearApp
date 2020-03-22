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
    /// 
    public delegate void PositionChange(object sender, PositionChangeEventArgs e);

    public class PositionChangeEventArgs : EventArgs
    {
        private string data;
        public PositionChangeEventArgs(string d)
        {
            this.data = d;
        }

        public string positionChanged {  get { return data; } }
    }

    public partial class Joystick : UserControl
    {
        public Joystick()
        {
            InitializeComponent();
        }
        private void centerKnob_Completed(object sender, EventArgs e) { }
        private Point firstPoint = new Point();
        bool mousePressed = false;
        double x, y, x1, y1, positionX, positionY;
        private double rightBorder = 45;
        private double leftBoarder = -45;
        private double upBorder = 45;
        private double downBoarder = -45;
        public event PositionChange positionchange;
        
        public double PositionY
        {
            get
            {
                return this.positionY;
            }
            set
            {
                if(value > upBorder)
                {
                    positionY = upBorder;
                }
                else if (value < downBoarder)
                {
                    positionY = downBoarder;
                }
                else
                {
                    positionY = value;                   
                }
                positionchange(this, new PositionChangeEventArgs("Y"));
            }
        }

        public double PositionX
        {
            get
            {
                return this.positionX;
            }
            set
            {
                if (value > rightBorder)
                {
                    positionX = rightBorder;
                }
                else if (value < leftBoarder)
                {
                    positionX = leftBoarder;
                }
                else
                {
                    positionX = value;                    
                }
                positionchange(this, new PositionChangeEventArgs("X"));
            }
        }

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
            PositionX = 0;
            knobPosition.Y = 0;
            PositionY = 0;
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
                    PositionX = -x;
                    knobPosition.Y = y;
                    PositionY = -y;
                }
            }
        }   
       
        
    }
}
