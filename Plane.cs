using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace River_Raid_WPF
{
    public class Plane
    {

        public MainWindow mainWindow;
        public Plane(MainWindow mainwindow)
        {
            this.mainWindow = mainwindow;
        }

        //Boleans
        public bool isShooting = false;
        public bool moveLeft = false;
        public bool moveRight = false;

        public double airPlanetop;
        public double velocity = 0;
        public double acceleration = 0.5;
        public double maxSpeed = 10;

        public ImageBrush PlaneImage = new ImageBrush();
        public ImageBrush DestroyedPlaneImage = new ImageBrush();

        //کنترل حرکت به چپ و راست
        public void ControlMoving()
        {

            if (moveLeft && moveRight || !moveLeft && !moveRight)
            {
                velocity = 0;
            }
            if (moveLeft && !moveRight)
            {
                if (velocity > 0) velocity = 0;
                velocity -= acceleration;
            }

            else if (moveRight && !moveLeft)
            {
                if (velocity < 0) velocity = 0;
                velocity += acceleration;
            }
            // محدود کردن سرعت به بازه‌ی مجاز
            if (velocity > maxSpeed) velocity = maxSpeed;
            if (velocity < -maxSpeed) velocity = -maxSpeed;

            Canvas.SetLeft(mainWindow.airPlane, Canvas.GetLeft(mainWindow.airPlane) + velocity);
        }

        public void ControlFuel()
        {

            if (mainWindow.animation.FuelClearationTime <= 5)
            {
                mainWindow.animation.FuelClearationTime = 5;
            }
            if (mainWindow.animation.FuelClearationTime >= 25)
            {
                mainWindow.animation.FuelClearationTime = 25;
            }
        }
    }
}
