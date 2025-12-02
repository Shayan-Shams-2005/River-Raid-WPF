using System;
using System.Diagnostics.Eventing.Reader;
using System.DirectoryServices.ActiveDirectory;
using System.Media;
using System.Numerics;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace River_Raid_WPF
{
    public partial class MainWindow : Window
    {
        //نمونه سازی از کلاس ها
        public CreatObstacles creatObstacles;
        public MovingObstacles movingObstacles;
        public Sound Sound;
        public ShayanDialog shayanDialog;
        public Animation animation;
        public Restart restart;
        public Scroll scroll;
        public Plane plane;
        public Collision colission;

        Random random = new Random();

        //زمان بندی ها
        public DispatcherTimer gameTimer = new DispatcherTimer();
        public DispatcherTimer TimerRecord;


        public MainWindow()
        {
            

            InitializeComponent();

            Sound = new Sound(this);                     // اول Sound بساز
            movingObstacles = new MovingObstacles(this); // بعد MovingObstacles بساز
            creatObstacles = new CreatObstacles(this); // بعد CreatObstacles بساز
            shayanDialog = new ShayanDialog(this);
            animation = new Animation(this);
            restart = new Restart(this);
            scroll = new Scroll(this);
            plane = new Plane(this);
            colission = new Collision(this);

            animation.CreditAnimation();

            animation.shootTimer.Interval = TimeSpan.FromMilliseconds(200);
            animation.shootTimer.Tick += animation.ShootTimer_Tick;

            //Plane ImageSource
            plane.PlaneImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Pictures/Air Plane.png"));
            //Destroyed Plane ImageSource
            plane.DestroyedPlaneImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Pictures/BOOM.png"));

            gameTimer.Interval = TimeSpan.FromMilliseconds(16);
            gameTimer.Tick += GameLoop;

            UFO1.Fill = creatObstacles.UFOImage;
            UFO2.Fill = creatObstacles.UFOImage;

            movingObstacles.spaceCreatures.Add(UFO1);
            movingObstacles.spaceCreatures.Add(UFO2);


            Fuel1.Fill=creatObstacles.SpaceFuelImage;
            Fuel2.Fill=creatObstacles.SpaceFuelImage;

        }

        public void GameLoop(object sender, EventArgs e)
        {
            animation.MoveBullets();

            Sound.BackgroundMusic();

            animation.AnimateHelicopters(scrollContent1);
            animation.AnimateHelicopters(scrollContent2);


            if (Canvas.GetLeft(UFO1) <= 5 && UFO1.Fill!=creatObstacles.DestroyedUFOImage && UFO1.Fill!=creatObstacles.DestroyedRightUFOImage) UFO1.Fill = creatObstacles.RightUFOImage;
            else if (Canvas.GetLeft(UFO1) >= 145 && UFO1.Fill != creatObstacles.DestroyedUFOImage && UFO1.Fill != creatObstacles.DestroyedRightUFOImage) UFO1.Fill = creatObstacles.UFOImage;

            if (Canvas.GetLeft(UFO2) <= 555 && UFO2.Fill != creatObstacles.DestroyedUFOImage && UFO2.Fill != creatObstacles.DestroyedRightUFOImage) UFO2.Fill = creatObstacles.RightUFOImage;
            else if (Canvas.GetLeft(UFO2) >= 695 && UFO2.Fill != creatObstacles.DestroyedUFOImage && UFO2.Fill != creatObstacles.DestroyedRightUFOImage) UFO2.Fill = creatObstacles.UFOImage;

            colission.CheckCollision();


            if (restart.isStarting)
            {
                plane.airPlanetop = Canvas.GetTop(scrollContent1);

                if (plane.airPlanetop < 0)
                {
                    Canvas.SetTop(scrollContent1, plane.airPlanetop + scroll.scrollSpeed);
                    restart.inputEnabled = false;
                }
                if (plane.airPlanetop >= 0)
                {
                    Canvas.SetTop(scrollContent1, 0);
                    restart.isStarting = false;
                    restart.inputEnabled = true;
                    gameTimer.Stop();
                }
                return;
            }

            scroll.ScrollElement(scrollContent1);
            scroll.ScrollElement(scrollContent2);

            movingObstacles.ObstaclesMovement(scrollContent1);
            movingObstacles.ObstaclesMovement(scrollContent2);

            if (Canvas.GetLeft(FuelDisplay) >= 100)
            {
                shayanDialog.LowFuelDialogShowed = true;
            }

            if (Canvas.GetLeft(FuelDisplay) <= 60 && shayanDialog.LowFuelDialogShowed)
            {
               // int FuelGameOverDialog = random.Next(22, 30);
               // tbShayanDialog.Text = shayanDialog.Dialog[FuelGameOverDialog];
               // animation.AnimateShayanOnce();
                shayanDialog.LowFuelDialogShowed = false;
            }

            if (Canvas.GetLeft(FuelDisplay) == 10)
            {
                if (tbHealth.Text != "1")
                {
                  //  int FuelGameOverDialog = random.Next(64, 72);
                   // tbShayanDialog.Text = shayanDialog.Dialog[FuelGameOverDialog];
                    //animation.AnimateShayanOnce();

                }
                //GameOver();
            }

            plane.ControlMoving();
        }


        public void Score(Rectangle rect)
        {
            if (creatObstacles.RightHeliFrames.Contains(rect.Fill) || creatObstacles.LeftHeliFrames.Contains(rect.Fill))
            {
                tbScore.Text = (int.Parse(tbScore.Text) + 30).ToString();
            }
            else if (rect.Fill == creatObstacles.RightShipImage || rect.Fill == creatObstacles.LeftShipImage)
            {
                tbScore.Text = (int.Parse(tbScore.Text) + 60).ToString();
            }
            else if (rect.Fill == creatObstacles.FuelImage)
            {
                tbScore.Text = (int.Parse(tbScore.Text) + 80).ToString();
            }
            else if (rect.Fill == Brushes.Orange)
            {
                tbScore.Text = (int.Parse(tbScore.Text) + 500).ToString();
            }
            else if(rect.Fill==Brushes.MediumPurple)
            {
                tbScore.Text = (int.Parse(tbScore.Text) + 1000).ToString();
            }
            else if(creatObstacles.spaceCreatures1.Contains(rect.Fill))
            {
                tbScore.Text = (int.Parse(tbScore.Text) + 60).ToString();
            }
            else if (creatObstacles.spaceCreatures2.Contains(rect.Fill))
            {
                tbScore.Text = (int.Parse(tbScore.Text) + 120).ToString();
            }
            else if (rect.Fill == creatObstacles.SpaceFuelImage)
            {
                tbScore.Text = (int.Parse(tbScore.Text) + 160).ToString();
            }
            else if(rect.Fill==creatObstacles.UFOImage)
            {
                tbScore.Text = (int.Parse(tbScore.Text) + 180).ToString();
            }
            else if(rect.Fill==creatObstacles.UfoBullet)
            {
                tbScore.Text = (int.Parse(tbScore.Text) + 10).ToString();
            }
            else if (rect.Fill == creatObstacles.spaceCreaturesBullet)
            {
                tbScore.Text = (int.Parse(tbScore.Text) + 5).ToString();
            }

        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            //Sometimes the input shouldn’t work.

            if (!restart.inputEnabled) return;

            if (e.Key == Key.Up)
            {
                scroll.scrollSpeed += 2;
            }

            if (e.Key == Key.Down)
            {
                scroll.scrollSpeed -= 2;
            }
            scroll.ControlScrollSpeed();
            animation.FuelClearationTime = 20 - (scroll.scrollSpeed * 1.3);
            plane.ControlFuel();

            animation.FuelAnimation();



            restart.GameStart();


            double airPlaneLeft = Canvas.GetLeft(airPlane);

            if (e.Key == Key.Left)
            {
                plane.moveLeft = true;
            }

            if (e.Key == Key.Right)
            {
                plane.moveRight = true;
            }


            if (e.Key == Key.Space && !plane.isShooting)
            {
                plane.isShooting = true;
                animation.Shot();
                animation.shootTimer.Start();
            }
        }
        
        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
                plane.moveLeft = false;


            if (e.Key == Key.Right)
                plane.moveRight = false;

            if (e.Key == Key.Space)
            {
                plane.isShooting = false;
                animation.shootTimer.Stop();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            restart.StartUp();

            TimerRecord = new DispatcherTimer();
            TimerRecord.Interval = TimeSpan.FromMilliseconds(10); // هر 10 میلی‌ثانیه ≈ 0.01 ثانیه
            TimerRecord.Tick += animation.GameTimerRecord_Tick;


            // UFO1 حرکت بین 0 تا 100
            movingObstacles.MoveUFO(UFO1, 0, 150, 1.5);

            // UFO2 حرکت بین 650 تا 550
            movingObstacles.MoveUFO(UFO2, 700, 550, 1.5);
        }
    }
}
