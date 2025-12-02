using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace River_Raid_WPF
{
    public class CreatObstacles
    {
        public MainWindow mainWindow;
        public CreatObstacles(MainWindow mainwindow)
        {
            this.mainWindow = mainwindow;

            RightShipImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Pictures/RightShip.png"));
            LeftShipImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Pictures/LeftShip.png"));
            // Right Helicopter Frames
            RightHeliFrames[0] = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Pictures/RightHelicopter 1.png")));
            RightHeliFrames[1] = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Pictures/RightHelicopter.png")));
            RightHeliFrames[2] = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Pictures/RightHelicopter 3.png")));

            // Left Helicopter Frames
            LeftHeliFrames[0] = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Pictures/LeftHelicopter 1.png")));
            LeftHeliFrames[1] = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Pictures/LeftHelicopter.png")));
            LeftHeliFrames[2] = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Pictures/LeftHelicopter 3.png")));

            FuelImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Pictures/Fuel.png"));
            UsedFuelImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Pictures/Used Fuel.png"));
            SpaceFuelImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Pictures/Space Fuel.png"));
            usedSpaceFuelImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Pictures/Used Space Fuel.png"));
            UfoBullet.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Pictures/UFO Bullet.png"));

            RightDestroyedHelicopter.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Pictures/RightDestroyed Helicopter.png"));
            LeftDestroyedHelicopter.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Pictures/LeftDestroyed Helicopter.png"));
            RightDestroyedShip.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Pictures/RightDestroyed Ship.png"));
            LeftDestroyedShip.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Pictures/LeftDestroyed Ship.png"));
            DestroyedFuel.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Pictures/Destroyed Fuel.png"));
            DestroyedusedFuel.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Pictures/Destroyed Used Fuel.png"));
            DestroyedSpaceFuelImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Pictures/Destroyed Space Fuel.png"));
            DestroyedusedSpaceFuelImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Pictures/Destroyed Used Space Fuel.png"));
            DestroyedUfoBullet.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Pictures/Destroyed UFO Bullet.png"));


            // Space Creatures 1 Frames
            spaceCreatures1[0] = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Pictures/Space Enemy 1.png")));
            spaceCreatures1[1] = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Pictures/Space Enemy 1-2.png")));
            DestroyedSpaceCreature1Image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Pictures/Destroyed Space Enemy 1.png"));

            spaceCreatures2[0] = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Pictures/Space Enemy 2.png")));
            spaceCreatures2[1] = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Pictures/Space Enemy 2-2.png")));
            DestroyedSpaceCreature2Image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Pictures/Destroyed Space Enemy 2.png"));

            UFOImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Pictures/UFO.png"));
            RightUFOImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Pictures/Right UFO.png"));
            DestroyedUFOImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Pictures/Destroyed UFO.png"));
            DestroyedRightUFOImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Pictures/Destroyed Right UFO.png"));
            spaceCreaturesBullet.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Pictures/Space Creatures Bullet.png"));

        }

        Random random = new Random();

        public bool Creatures1Created = false;
        public bool Creatures1Creating = false;

        public bool Creatures2Created = false;
        public bool Creatures2Creating = false;

        //Obstacles Images
        public ImageBrush RightShipImage = new ImageBrush();
        public ImageBrush LeftShipImage = new ImageBrush();
        public ImageBrush[] RightHeliFrames = new ImageBrush[3];
        public ImageBrush[] LeftHeliFrames = new ImageBrush[3];
        public ImageBrush FuelImage = new ImageBrush();
        public ImageBrush UsedFuelImage=new ImageBrush();
        public ImageBrush SpaceFuelImage=new ImageBrush();
        public ImageBrush usedSpaceFuelImage = new ImageBrush();
        public ImageBrush[] spaceCreatures1 = new ImageBrush[2];
        public ImageBrush[] spaceCreatures2 = new ImageBrush[2];
        public ImageBrush SpaceCreature2Image = new ImageBrush();
        public ImageBrush UFOImage=new ImageBrush();
        public ImageBrush RightUFOImage = new ImageBrush();

        public ImageBrush RightDestroyedHelicopter = new ImageBrush();
        public ImageBrush LeftDestroyedHelicopter = new ImageBrush();
        public ImageBrush RightDestroyedShip = new ImageBrush();
        public ImageBrush LeftDestroyedShip = new ImageBrush();
        public ImageBrush DestroyedFuel = new ImageBrush();
        public ImageBrush DestroyedusedFuel = new ImageBrush();
        public ImageBrush DestroyedSpaceFuelImage = new ImageBrush();
        public ImageBrush DestroyedusedSpaceFuelImage = new ImageBrush();
        public ImageBrush DestroyedSpaceCreature1Image = new ImageBrush();
        public ImageBrush DestroyedSpaceCreature2Image = new ImageBrush();
        public ImageBrush DestroyedUFOImage = new ImageBrush();
        public ImageBrush DestroyedRightUFOImage = new ImageBrush();


        public ImageBrush UfoBullet = new ImageBrush();
        public ImageBrush spaceCreaturesBullet=new ImageBrush();
        public ImageBrush DestroyedUfoBullet = new ImageBrush();


        //Space Creatures1 Locations
        public int[] Run1Creatures1Left = { 250, 300, 350, 400, 450, 500, 250, 300, 350, 400, 450, 500, 250, 300, 350, 400, 450, 500, 250, 300, 350, 400, 450, 500 };
        public int[] Run1Creatures1Top = { 10, 10, 10, 10, 10, 10, 40, 40, 40, 40, 40, 40, 70, 70, 70, 70, 70, 70, 100, 100, 100, 100, 100, 100 };

        public int[] Run2Creatures1Left = { 250, 300, 350, 400, 450, 500, 250, 300, 350, 400, 450, 500, 250, 300, 350, 400, 450, 500, 250, 300, 350, 400, 450, 500 };
        public int[] Run2Creatures1Top = { 40, 40, 40, 40, 40, 40, 70, 70, 70, 70, 70, 70, 100, 100, 100, 100, 100, 100, 130, 130, 130, 130, 130, 130 };

        public int[] Run4Creatures1Left = {143,103,63,63,63,103,143,143,143,103,63,338,308,368,298,338,378,298,378,298,378,548,518,578,508,548,588,508,588,508,588};
        public int[] Run4Creatures1Top = {10,10,10,40,70,70,70,100,130,130,130,10,40,40,70,70,70,100,100,130,130,10,40,40,70,70,70,100,100,130,130};

        public int[] Run3Creatures1Left = {200,250,300,350,400,450,500,550,200,250,300,350,400,450,500,550,200,250,300,350,400,450,500,550,200,250,300,350,400,450,500,550};
        public int[] Run3Creatures1Top = {70,70,70,70,70,70,70,70,100,100,100,100,100,100,100,100,130,130,130,130,130,130,130,130,160,160,160,160,160,160,160,160};

        //Space Creatures 2 Locations
        public int[] Run2Creatures2Left = { 200, 250, 300, 350, 400, 450, 500 , 550, 200, 550, 200 ,550, 200, 550, 200, 550, 200, 250, 300, 350, 400, 450, 500, 550};
        public int[] Run2Creatures2Top = { 10, 10, 10, 10, 10, 10, 10, 10, 40, 40, 70, 70 ,100, 100, 130, 130, 160, 160, 160, 160, 160, 160, 160, 160};

        public int[] Run4Creatures2Left = {183,183,183,183,183,208,233,258,258,258,258,258,408,478,418,468,443,443,443,628,628,628,628,628,648,668,688,708,708,708,708,708};
        public int[] Run4Creatures2Top = {10,40,70,100,130,70,70,10,40,70,100,130,10,10,40,40,70,100,130,130,100,70,40,10,40,70,100,130,100,70,40,10};

        public int[] Run3Creatures2Left = { 200, 250, 300, 350, 400, 450, 500, 550, 200, 250, 300, 350, 400, 450, 500, 550};
        public int[] Run3Creatures2Top = { 10, 10, 10, 10, 10, 10, 10, 10, 40, 40, 40, 40, 40, 40, 40, 40};


        //UFO's Location
        // public int[] Run2UFOsLeft = { 50, 650 };
        //public int[] Run2UFOsTop = { 20, 20 };

        //Helicopter's Locations
        public int[] Run1Sc1HelicoptersLeft = { 300, 270, 400, 500, 550, 550, 500, 250, 250 };
        public int[] Run1Sc1HelicoptersTop = { -180, -900, -1150, -1700, -2100, -2400, -2800, -3100, -3300 };

        public int[] Run1Sc2HelicoptersLeft = { 540, 200, 100, 100, 50, 80, 650, 200 };
        public int[] Run1Sc2HelicoptersTop = { 3780, 3400, 3050, 2850, 2200, 1250, 1000, 600 };

        public int[] Run2Sc1HelicoptersLeft = { 550, 550, 450, 350, 300, 350, 550, 270 };
        public int[] Run2Sc1HelicoptersTop = { -300, -400, -600, -800, -1000, -1150, -1650, -2100 };

        public int[] Run2Sc2HelicoptersLeft = { 550, 50, 250 };
        public int[] Run2Sc2HelicoptersTop = { 3300, 2200, 500 };

        public int[] Run3Sc1HelicoptersLeft = { 360, 550, 500, 200, 360, 460, 550, 500, 560, 460, 420, 380, 500, 380, 520 };
        public int[] Run3Sc1HelicoptersTop = { -250, -350, -450, -550, -700, -800, -1050, -1150, -1600, -2000, -2200, -2300, -2450, -2600, -2800 };

        public int[] Run3Sc2HelicoptersLeft = { 180, 180, 180, 380, 40, 40, 682, 200, 27, 708, 708, 228 };
        public int[] Run3Sc2HelicoptersTop = { 4200, 4100, 3600, 3500, 3000, 2700, 2400, 2050, 750, 850, 750, 550 };

        //Ship's Location
        public int[] Run1Sc1ShipsLeft = { 360, 520, 320, 550, 365 };
        public int[] Run1Sc1ShipsTop = { 0, -1550, -1850, -2600, -3500 };

        public int[] Run1Sc2ShipsLeft = { 440, 570, 200, 500, 220, 500, 100 };
        public int[] Run1Sc2ShipsTop = { 4200, 3910, 2600, 2550, 2000, 1850, 500 };

        public int[] Run2Sc1ShipsLeft = { 350, 550, 300, 470, 350, 200, 200, 365 };
        public int[] Run2Sc1ShipsTop = { -50, -1300, -1450, -1800, -2400, -2800, -3000, -3400 };

        public int[] Run2Sc2ShipsLeft = { 500, 100, 650, 150, 685, 365, 365 };
        public int[] Run2Sc2ShipsTop = { 3550, 3150, 3000, 2800, 1100, 700, 200 };

        public int[] Run3Sc1ShipsLeft = { 300, 400, 540, 540, 365 };
        public int[] Run3Sc1ShipsTop = { -100, -15, -1750, -2950, -3350 };

        public int[] Run3Sc2ShipsLeft = { 565, 90, 640, 680, 600, 670, 100, 27, 27, 27, 27, 27, 365 };
        public int[] Run3Sc2ShipsTop = { 3750, 3200, 3200, 2500, 1670, 1370, 1500, 1250, 1150, 1050, 950, 850, 150 };

        //Fuels Location 
        public int[] Run1Sc1FuelsLeft = { 460, 400, 520, 420, 320, 520, 390, 390, 500 };
        public int[] Run1Sc1FuelsTop = { -80, -330, -500, -800, -1030, -1330, -2000, -3000, -3200 };

        public int[] Run1Sc2FuelsLeft = { 540, 600, 250, 200, 680, 80, 700, 600 };
        public int[] Run1Sc2FuelsTop = { 3650, 3100, 2700, 2100, 1380, 1100, 680, 350 };

        public int[] Run2Sc1FuelsLeft = { 450, 250, 350, 450, 300, 350, 450 };
        public int[] Run2Sc1FuelsTop = { -900, -2000, -2200, -2300, -3150, -3250, -3300 };

        public int[] Run2Sc2FuelsLeft = { 550, 620, 150, 600, 200, 530, 100, 50, 685 };
        public int[] Run2Sc2FuelsTop = { 4150, 3890, 2650, 2400, 2050, 1800, 1650, 1250, 950 };

        public int[] Run3Sc1FuelsLeft = { 280, 380, 520, 220 };
        public int[] Run3Sc1FuelsTop = { -625, -950, -1400, -3100 };

        public int[] Run3Sc2FuelsLeft = { 640, 640, 90, 40, 690, 180, 705, 705, 705, 705 };
        public int[] Run3Sc2FuelsTop = { 3890, 3300, 3300, 2800, 2170, 1950, 1250, 1150, 1050, 950 };


        public void Run1()
        {
            CreatHelicopter(Run1Sc1HelicoptersLeft, Run1Sc1HelicoptersTop, mainWindow.scrollContent1);
            CreatHelicopter(Run1Sc2HelicoptersLeft, Run1Sc2HelicoptersTop, mainWindow.scrollContent2);

            CreatShip(Run1Sc1ShipsLeft, Run1Sc1ShipsTop, mainWindow.scrollContent1);
            CreatShip(Run1Sc2ShipsLeft, Run1Sc2ShipsTop, mainWindow.scrollContent2);

            CreatFuel(Run1Sc1FuelsLeft, Run1Sc1FuelsTop, mainWindow.scrollContent1);
            CreatFuel(Run1Sc2FuelsLeft, Run1Sc2FuelsTop, mainWindow.scrollContent2);
        }
        public void Run2()
        {
            CreatHelicopter(Run2Sc1HelicoptersLeft, Run2Sc1HelicoptersTop, mainWindow.scrollContent1);
            CreatHelicopter(Run2Sc2HelicoptersLeft, Run2Sc2HelicoptersTop, mainWindow.scrollContent2);

            CreatShip(Run2Sc1ShipsLeft, Run2Sc1ShipsTop, mainWindow.scrollContent1);
            CreatShip(Run2Sc2ShipsLeft, Run2Sc2ShipsTop, mainWindow.scrollContent2);

            CreatFuel(Run2Sc1FuelsLeft, Run2Sc1FuelsTop, mainWindow.scrollContent1);
            CreatFuel(Run2Sc2FuelsLeft, Run2Sc2FuelsTop, mainWindow.scrollContent2);
        }
        public void Run3()
        {
            CreatHelicopter(Run3Sc1HelicoptersLeft, Run3Sc1HelicoptersTop, mainWindow.scrollContent1);
            CreatHelicopter(Run3Sc2HelicoptersLeft, Run3Sc2HelicoptersTop, mainWindow.scrollContent2);

            CreatShip(Run3Sc1ShipsLeft, Run3Sc1ShipsTop, mainWindow.scrollContent1);
            CreatShip(Run3Sc2ShipsLeft, Run3Sc2ShipsTop, mainWindow.scrollContent2);

            CreatFuel(Run3Sc1FuelsLeft, Run3Sc1FuelsTop, mainWindow.scrollContent1);
            CreatFuel(Run3Sc2FuelsLeft, Run3Sc2FuelsTop, mainWindow.scrollContent2);
        }

        public void CreatUFO(int[] Leftarray, int[] Toparray, Canvas canvas)
        {

            for (int i = 0; i < Leftarray.Length; i++)
            {
                Rectangle Fuel = new Rectangle
                {
                    Width = 100,
                    Height = 100,
                    Fill = UFOImage,
                    Name = "UFO"
                };

                Canvas.SetLeft(Fuel, Leftarray[i]);
                Canvas.SetTop(Fuel, Toparray[i]);

                canvas.Children.Add(Fuel);

            }
        }

        public void CreatFuel(int[] Leftarray, int[] Toparray, Canvas canvas)
        {

            for (int i = 0; i < Leftarray.Length; i++)
            {
                Rectangle Fuel = new Rectangle
                {
                    Width = 70,
                    Height = 70,
                    Fill = FuelImage,
                    Name = "Fuel"
                };

                Canvas.SetLeft(Fuel, Leftarray[i]);
                Canvas.SetTop(Fuel, Toparray[i]);

                canvas.Children.Add(Fuel);

            }
        }

        public void CreatShip(int[] Leftarray, int[] Toparray, Canvas canvas)
        {

            for (int i = 0; i < Leftarray.Length; i++)
            {
                int RightorLeft = random.Next(0, 2);

                Rectangle Ship = new Rectangle
                {
                    Width = 70,
                    Height = 70,
                    Name = "Ship"
                };

                if (RightorLeft == 0)
                {
                    Ship.Fill = LeftShipImage;
                }
                else if (RightorLeft == 1)
                {
                    Ship.Fill = RightShipImage;
                }
                Canvas.SetLeft(Ship, Leftarray[i]);
                Canvas.SetTop(Ship, Toparray[i]);

                canvas.Children.Add(Ship);

            }
        }
        public void CreatHelicopter(int[] Leftarray, int[] Toparray, Canvas canvas)
        {

            for (int i = 0; i < Leftarray.Length; i++)
            {
                Rectangle Helicopter = new Rectangle
                {
                    Width = 66,
                    Height = 54,
                    Name = "Helicopter"
                };

                int RightorLeft = random.Next(0, 2);


                if (RightorLeft == 0)
                {
                    Helicopter.Fill = LeftHeliFrames[0];
                }
                else if (RightorLeft == 1)
                {
                    Helicopter.Fill = RightHeliFrames[0];
                }

                Canvas.SetLeft(Helicopter, Leftarray[i]);
                Canvas.SetTop(Helicopter, Toparray[i]);

                canvas.Children.Add(Helicopter);

            }
        }

        public void CreatSpaceCreature1(int[] lefts, int[] tops, Canvas canvas)
        {


            int i = 0;
            Creatures1Creating = true;

            var t = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
            t.Tick += (s, e) =>
            {
                if (i >= lefts.Length)
                {
                    t.Stop();
                    Creatures1Created = true;
                    if (int.Parse(mainWindow.LevelCount.Text) >= 7 && int.Parse(mainWindow.LevelCount.Text) <= 9)
                    {
                        mainWindow.UFO1.Fill = UFOImage;
                        mainWindow.UFO2.Fill = UFOImage;
                        mainWindow.UFO1.Visibility = Visibility.Visible;
                        mainWindow.UFO2.Visibility = Visibility.Visible;
                    }

                    // تنها زمانی حرکت را شروع کن که هر دو گروه ساخته شده باشند
                    mainWindow.restart.ClearedObsatacles = false;
                    CheckAndStartMovement();
                    return;
                }

                var r = new Rectangle { Width = 30, Height = 30, Fill = spaceCreatures1[0], Name = "SpaceCreature1" };
                Canvas.SetLeft(r, lefts[i]);
                Canvas.SetTop(r, tops[i]);
                canvas.Children.Add(r);
                mainWindow.movingObstacles.spaceCreatures.Add(r);

                i++;
            };

            t.Start();
        }

        public void CreatSpaceCreature2(int[] lefts, int[] tops, Canvas canvas)
        {

            int i = 0;
            Creatures2Creating = true;

            var t = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
            t.Tick += (s, e) =>
            {
                if (i >= lefts.Length)
                {
                    t.Stop();
                    Creatures2Created = true;
                    // تنها زمانی حرکت را شروع کن که هر دو گروه ساخته شده باشند
                    CheckAndStartMovement();
                    mainWindow.restart.ClearedObsatacles = false;
                    return;
                }

                var r = new Rectangle { Width = 30, Height = 30, Fill = spaceCreatures2[0], Name = "SpaceCreature2" };
                Canvas.SetLeft(r, lefts[i]);
                Canvas.SetTop(r, tops[i]);
                canvas.Children.Add(r);
                mainWindow.movingObstacles.spaceCreatures.Add(r);

                i++;
            };

            t.Start();
        }

        private void CheckAndStartMovement()
        {
            // اگر هر دو ساخته شده باشند → شروع حرکت
            if ((Creatures1Created && Creatures2Created && mainWindow.LevelCount.Text!="6") || mainWindow.LevelCount.Text=="6")
            {
                mainWindow.movingObstacles.GroupCreatures();
                mainWindow.movingObstacles.StartMoveLoop();
            }
        }



    }
}
