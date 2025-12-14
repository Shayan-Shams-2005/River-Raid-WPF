using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Win32.SafeHandles;
namespace River_Raid_WPF
{
    public class Animation
    {
        public Storyboard fuelStoryboard;
        public Storyboard KingsStoryBoard;
        //Credit Variables
        public Storyboard creditStoryboard;

        public double startLeft;

        public bool AnimationIsPlaying = false;

        public int milliseconds = 0;
        public int seconds = 0;
        public int minutes = 0;
        int heliFrame = 0;

        //Shot Variables
        public List<Rectangle> bulletsList = new List<Rectangle>();
        public List<Rectangle> MissileList = new List<Rectangle>();
        public DispatcherTimer shootTimer = new DispatcherTimer();
        public int bulletSpeed = 25;
        public double MissileSpeed = 5;

        //
        List<Rectangle> bullets = new();
        DispatcherTimer spaceCreaturesshootTimer, bulletTimer;


        Random random =new Random();

        public MainWindow mainWindow;

        public Animation(MainWindow mainwindow)
        {
            this.mainWindow = mainwindow;
        }

        public double FuelClearationTime;


        //Credit animation
        public void CreditAnimation()
        {
            // انیمیشن اول
            DoubleAnimation animMain = new DoubleAnimation
            {
                From = 10,
                To = -20,
                Duration = TimeSpan.FromSeconds(3),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever,
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
            };

            // انیمیشن دوم
            DoubleAnimation animName = new DoubleAnimation
            {
                From = 40,
                To = 10,
                Duration = TimeSpan.FromSeconds(3),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever,
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
            };

            Storyboard creditStoryboard = new Storyboard();

            Storyboard.SetTarget(animMain, mainWindow.tbCreditMain);
            Storyboard.SetTargetProperty(animMain, new PropertyPath("(Canvas.Top)"));

            Storyboard.SetTarget(animName, mainWindow.tbCreditName);
            Storyboard.SetTargetProperty(animName, new PropertyPath("(Canvas.Top)"));

            creditStoryboard.Children.Add(animMain);
            creditStoryboard.Children.Add(animName);

            creditStoryboard.Begin();
        }

        //انمیشن شایان
        public async Task AnimateShayanOnce()
        {
            if (!AnimationIsPlaying)
            {
                // نمایش فریم دوم
                mainWindow.Shayan.Source = new BitmapImage(new Uri("pack://application:,,,/Pictures/Shayan Shams1.png"));
                mainWindow.DialogBorder.Visibility = Visibility.Visible;

                // زمان نمایش دیالوگ
                await Task.Delay(2000);
                mainWindow.DialogBorder.Visibility = Visibility.Hidden;

                // بازگرداندن فریم به حالت اولیه
                mainWindow.Shayan.Source = new BitmapImage(new Uri("pack://application:,,,/Pictures/Shayan Shams.png"));
            }
        }
        public async Task ShayanSpaceIntroAnimation()
        {
            AnimationIsPlaying = true;
            mainWindow.restart.inputEnabled = false;
            // نمایش فریم دوم
            await Task.Delay(1500);
            mainWindow.Shayan.Source = new BitmapImage(new Uri("pack://application:,,,/Pictures/Shayan Shams1.png"));
            TypeTextAsync("So...",40);
            mainWindow.DialogBorder.Visibility = Visibility.Visible;
            // زمان نمایش دیالوگ
            await Task.Delay(2000);
            mainWindow.DialogBorder.Visibility = Visibility.Hidden;

            mainWindow.Shayan.Source = new BitmapImage(new Uri("pack://application:,,,/Pictures/Shayan Shams1.png"));
            TypeTextAsync("You Made It",40);
            mainWindow.DialogBorder.Visibility = Visibility.Visible;
            // زمان نمایش دیالوگ
            await Task.Delay(1500);
            TypeTextAsync("We're in Space!",40);

            await Task.Delay(2000);
            mainWindow.DialogBorder.Visibility = Visibility.Hidden;

            await Task.Delay(2000);

            mainWindow.Shayan.Source = new BitmapImage(new Uri("pack://application:,,,/Pictures/Shayan Shams1.png"));
            TypeTextAsync("There Are Some...",40);
            mainWindow.DialogBorder.Visibility = Visibility.Visible;
            await Task.Delay(2000);

            TypeTextAsync("SpaceShips Here", 40);
            mainWindow.DialogBorder.Visibility = Visibility.Visible;
            await Task.Delay(2000);

            TypeTextAsync("Shoot Them!",40);
            await Task.Delay(2000);
            mainWindow.DialogBorder.Visibility = Visibility.Hidden;
            await Task.Delay(2000);

            mainWindow.Shayan.Source = new BitmapImage(new Uri("pack://application:,,,/Pictures/Shayan Shams1.png"));
            TypeTextAsync("Destroy Them All!",40);
            mainWindow.DialogBorder.Visibility = Visibility.Visible;
            await Task.Delay(1500);
            mainWindow.restart.inputEnabled = true;
            // بازگرداندن فریم به حالت اولیه
            mainWindow.Shayan.Source = new BitmapImage(new Uri("pack://application:,,,/Pictures/Shayan Shams.png"));
            mainWindow.DialogBorder.Visibility = Visibility.Hidden;
            AnimationIsPlaying = false;
            mainWindow.scroll.SpaceLevelCount = 1;
        }

        public async Task ShayanSpaceoAnimation2()
        {
            AnimationIsPlaying = true;
            mainWindow.Shayan.Source = new BitmapImage(new Uri("pack://application:,,,/Pictures/Shayan Shams1.png"));
            TypeTextAsync("That Was Good!", 40);
            mainWindow.DialogBorder.Visibility = Visibility.Visible;
            await Task.Delay(1000);

            TypeTextAsync("Keep Destroying.", 40);
            mainWindow.DialogBorder.Visibility = Visibility.Visible;
            // زمان نمایش دیالوگ
            await Task.Delay(1500);

            mainWindow.DialogBorder.Visibility = Visibility.Hidden;

            AnimationIsPlaying = false;

        }
        public async Task ShayanSpaceAnimation3()
        {
            mainWindow.restart.inputEnabled = false;
            AnimationIsPlaying = true;
            await Task.Delay(2000);
            mainWindow.Shayan.Source = new BitmapImage(new Uri("pack://application:,,,/Pictures/Shayan Shams1.png"));
            TypeTextAsync("Now...", 40);
            mainWindow.DialogBorder.Visibility = Visibility.Visible;
            await Task.Delay(1000);

            TypeTextAsync("Time For Their Planet!.", 40);
            mainWindow.DialogBorder.Visibility =  Visibility.Visible;
            // زمان نمایش دیالوگ
            await Task.Delay(2500);

            mainWindow.Planet.Fill = mainWindow.creatObstacles.Mars;
            mainWindow.Planet.Visibility = Visibility.Visible;

            TypeTextAsync("Shoot!.", 40);

            await Task.Delay(1000);
            mainWindow.DialogBorder.Visibility = Visibility.Visible;

            mainWindow.DialogBorder.Visibility = Visibility.Hidden;

            mainWindow.restart.inputEnabled = true;
            AnimationIsPlaying = false;

        }
        public async Task ShayanSpaceAnimation4()
        {
            mainWindow.restart.inputEnabled = false;
            AnimationIsPlaying = true;
            await Task.Delay(2000);
            mainWindow.Shayan.Source = new BitmapImage(new Uri("pack://application:,,,/Pictures/Shayan Shams1.png"));
            TypeTextAsync("Now", 40);
            mainWindow.DialogBorder.Visibility = Visibility.Visible;
            await Task.Delay(2000);

            mainWindow.Shayan.Source = new BitmapImage(new Uri("pack://application:,,,/Pictures/Shayan Shams.png"));
            await Task.Delay(1000);
            mainWindow.Shayan.Source = new BitmapImage(new Uri("pack://application:,,,/Pictures/Shayan Shams1.png"));
            await Task.Delay(500);
            mainWindow.Shayan.Source = new BitmapImage(new Uri("pack://application:,,,/Pictures/Shayan Shams2.png"));
            await Task.Delay(1000);
            TypeTextAsync("The Last Shot!.", 40);
            await Task.Delay(2000);

            TypeTextAsync("We're Gonna WIN!.", 40);
            await Task.Delay(1000);

            TypeTextAsync("This is Our Chance!.", 40);

            await Task.Delay(1000);

            TypeTextAsync("Do That...", 40);


            KingsArriving();

            await Task.Delay(2000);
            mainWindow.DialogBorder.Visibility = Visibility.Hidden;

            await Task.Delay(500);

            mainWindow.Shayan.Source = new BitmapImage(new Uri("pack://application:,,,/Pictures/Shayan Shams1.png"));

            await Task.Delay(500);

            mainWindow.Shayan.Source = new BitmapImage(new Uri("pack://application:,,,/Pictures/Shayan Shams.png"));          
        }
        public async Task KingsDialog()
        {
            mainWindow.tbKingDialog.Text = "pls Stop!";
            mainWindow.KingsDialogBorder.Visibility = Visibility.Visible;

            await Task.Delay(1000);

            mainWindow.tbKingDialog.Text = "He is Lying to You!";

            await Task.Delay(2000);

            mainWindow.tbKingDialog.Text = "He ...";

            await Task.Delay(2000);

            mainWindow.tbKingDialog.Text = "is...";

            await Task.Delay(2000);

            mainWindow.tbKingDialog.Text = "oh My God...";

            await Task.Delay(2000);

            mainWindow.tbKingDialog.Text = "He is Gonna Kill Me";

            await Task.Delay(2000);

            mainWindow.tbKingDialog.Text = "He Will Destroy You After That!";

            await Task.Delay(2000);


            mainWindow.Shayan.Source = new BitmapImage(new Uri("pack://application:,,,/Pictures/Shayan Shams1.png"));

            await Task.Delay(500);

            mainWindow.Shayan.Source = new BitmapImage(new Uri("pack://application:,,,/Pictures/Shayan Shams3.png"));

            TypeTextAsync("Shut it UP!!", 40);
            mainWindow.DialogBorder.Visibility = Visibility.Visible;
            await Task.Delay(2000);

            mainWindow.DialogBorder.Visibility = Visibility.Hidden;

            mainWindow.restart.inputEnabled = true;
            AnimationIsPlaying = false;
        }
        public async Task KingsArriving()
        {
            mainWindow.KingsSpaceShip.Source = mainWindow.creatObstacles.kingsSpaceShip1;
            mainWindow.KingsSpaceShip.Visibility = Visibility.Visible;

            kingsSpaceShipScale();
            kingsSpaceShipMovement();
        }

        public async Task KingsSpacceShipOpening()
        {
            await Task.Delay(3000);
            mainWindow.KingsSpaceShip.Source = mainWindow.creatObstacles.kingsSpaceShip2;
            await Task.Delay(500);
            mainWindow.KingsSpaceShip.Source = mainWindow.creatObstacles.kingsSpaceShip3;
            await Task.Delay(500);

            mainWindow.King.Visibility=Visibility.Visible;
            mainWindow.KingsSpaceShip.Source = mainWindow.creatObstacles.kingsSpaceShip4;
            await Task.Delay(1000);

            kingsMovementTop();

            await Task.Delay(4000);

            mainWindow.KingsSpaceShip.Source = mainWindow.creatObstacles.kingsSpaceShip4;
            await Task.Delay(500);

            mainWindow.KingsSpaceShip.Source = mainWindow.creatObstacles.kingsSpaceShip5;
            await Task.Delay(500);
            mainWindow.KingsSpaceShip.Source = mainWindow.creatObstacles.kingsSpaceShip6;  

            await Task.Delay(1000);

            SpaceShipMovement();
        }

        public void SpaceShipMovement()
        {
            var anim = new DoubleAnimation
            {
                From = Canvas.GetLeft(mainWindow.KingsSpaceShip),
                To = -500,
                Duration = TimeSpan.FromSeconds(2)
            };

            anim.Completed += (s, e) =>
            {
                KingsDialog(); // async call
            };
            mainWindow.KingsSpaceShip.BeginAnimation(Canvas.LeftProperty, anim);
        
        }

        public void kingsMovementTop()
        {
            var anim = new DoubleAnimation
            {
                From = Canvas.GetTop(mainWindow.King),
                To = 0,
                Duration = TimeSpan.FromSeconds(3)
            };

            anim.Completed += (s, e) =>
            {
                kingsMovementLeft(); // async call
            };

            mainWindow.King.BeginAnimation(Canvas.TopProperty, anim);
        }
        public void kingsMovementLeft()
        {
            var anim = new DoubleAnimation
            {
                From = Canvas.GetLeft(mainWindow.King),
                To = 50,
                Duration = TimeSpan.FromSeconds(3)
            };
            mainWindow.King.BeginAnimation(Canvas.LeftProperty, anim);
        }

        public void kingsSpaceShipScale()
        {
            var anim = new DoubleAnimation
            {
                From = 0.25,
                To = 20,
                Duration = TimeSpan.FromSeconds(10)
            };

            anim.Completed += (s, e) =>
            {
                _ = KingsSpacceShipOpening(); // async call
            };

            mainWindow.kingScale.BeginAnimation(ScaleTransform.ScaleXProperty, anim);
            mainWindow.kingScale.BeginAnimation(ScaleTransform.ScaleYProperty, anim);

        }


        public void kingsSpaceShipMovement()
        {
            // Animation for Canvas.Left
            var leftAnim = new DoubleAnimation
            {
                To = 100,
                Duration = TimeSpan.FromSeconds(10),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            // Animation for Canvas.Top
            var topAnim = new DoubleAnimation
            {
                To = 10,
                Duration = TimeSpan.FromSeconds(10),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            // Start animations
            mainWindow.KingsSpaceShip.BeginAnimation(Canvas.LeftProperty, leftAnim);
            mainWindow.KingsSpaceShip.BeginAnimation(Canvas.TopProperty, topAnim);
        }




        //انیمیشن مربوط به نمایش سوخت 
        public void FuelAnimation()
        {
            double startLeft = Canvas.GetLeft(mainWindow.FuelDisplay);
            double targetLeft = 10;
            double trueFuelClearationTime = FuelClearationTime;
            trueFuelClearationTime = FuelClearationTime * startLeft / 300;
            // ساخت انیمیشن جدید برای سوخت
            var animation = new DoubleAnimation
            {
                From = startLeft,
                To = targetLeft,
                Duration = TimeSpan.FromSeconds(trueFuelClearationTime),
                FillBehavior = FillBehavior.HoldEnd
            };

            Storyboard.SetTarget(animation, mainWindow.FuelDisplay);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(Canvas.Left)"));

            fuelStoryboard = new Storyboard();
            fuelStoryboard.Children.Add(animation);

            fuelStoryboard.Begin();
        }

        //پر کردن سوخت بعد از هربار باخت
        public void ChargeUpFuel(Rectangle rect)
        {

            fuelStoryboard.Stop();
            startLeft = Canvas.GetLeft(mainWindow.FuelDisplay) + 80;
            double trueFuelClearationTime = FuelClearationTime;
            trueFuelClearationTime = FuelClearationTime * startLeft / 300;

            if (Canvas.GetLeft(mainWindow.FuelDisplay) < 40)
            {
                int ChargeUpFuelDialog = random.Next(30, 38);
                mainWindow.animation.TypeTextAsync(mainWindow.shayanDialog.Dialog[ChargeUpFuelDialog],40);
                AnimateShayanOnce();
            }

            if (startLeft > 320)
            {
                startLeft = 320;

            }
            if (fuelStoryboard.Children[0] is DoubleAnimation anim)
            {
                anim.From = startLeft;
                anim.To = 10;
                anim.Duration = TimeSpan.FromSeconds(trueFuelClearationTime);
                anim.FillBehavior = FillBehavior.HoldEnd;
            }
            // reset and start again
            fuelStoryboard.Begin(mainWindow, true);
        }

        public void MissileLaunch()
        {
            mainWindow.Sound.ShootingSound();

            Rectangle Missile = new Rectangle
            {
                Width = 80,
                Height = 80,
                Fill = mainWindow.creatObstacles.planeMissile1
            };

            double left = Canvas.GetLeft(mainWindow.airPlane) + mainWindow.airPlane.Width / 2 - Missile.Width / 2;
            double top = Canvas.GetTop(mainWindow.airPlane) - Missile.Height;

            Canvas.SetLeft(Missile, left);
            Canvas.SetTop(Missile, top+80);

            mainWindow.unScrollingContent.Children.Add(Missile);
            MissileList.Add(Missile);
        }

        public void MoveMissiles()
        {
            for (int i = MissileList.Count - 1; i >= 0; i--)
            {
                var b = MissileList[i];
                double top = Canvas.GetTop(b);

                if (Canvas.GetTop(b) >= 185)
                {
                    top = Canvas.GetTop(b) - 5; // سرعت گلوله
                    Canvas.SetTop(b, top);
                }
                else if(Canvas.GetTop(b) < 185 && Canvas.GetTop(b) > 175)
                {
                    top = Canvas.GetTop(b) - .15; // سرعت گلوله
                    Canvas.SetTop(b, top);
                }
                else
                {
                    top = Canvas.GetTop(b)-25; // سرعت گلوله
                    Canvas.SetTop(b, top);
                }

                if (top < -50)
                {
                    mainWindow.unScrollingContent.Children.Remove(b);
                    MissileList.RemoveAt(i);
                }
            }
        }


        public void Shot()
        {
            mainWindow.Sound.ShootingSound();

            Rectangle bullet = new Rectangle
            {
                Width = 5,
                Height = 15,
                Fill = mainWindow.creatObstacles.planeBullet
            };

            double left = Canvas.GetLeft(mainWindow.airPlane) + mainWindow.airPlane.Width / 2 - bullet.Width / 2;
            double top = Canvas.GetTop(mainWindow.airPlane) - bullet.Height;

            Canvas.SetLeft(bullet, left);
            Canvas.SetTop(bullet, top);

            mainWindow.unScrollingContent.Children.Add(bullet);
            bulletsList.Add(bullet);
        }

        public void MoveBullets()
        {
            for (int i = bulletsList.Count - 1; i >= 0; i--)
            {
                var b = bulletsList[i];

                double top = Canvas.GetTop(b) - bulletSpeed; // سرعت گلوله
                Canvas.SetTop(b, top);

                if (top < 0)
                {
                    mainWindow.unScrollingContent.Children.Remove(b);
                    bulletsList.RemoveAt(i);
                }
            }
        }

        public void ShootTimer_Tick(object sender, EventArgs e)
        {
            if(mainWindow.Missile) MissileLaunch();
            else if(mainWindow.Bullets) Shot();
        }



        public void GameTimerRecord_Tick(object sender, EventArgs e)
        {

            if (!mainWindow.restart.isStarting && mainWindow.restart.GameStarted)
            {
                milliseconds += 1;

                if (milliseconds >= 100)
                {
                    milliseconds = 0;
                    seconds++;
                }

                if (seconds >= 60)
                {
                    seconds = 0;
                    minutes++;
                }
            }

            // نمایش روی رابط کاربری
            mainWindow.MilliSeconds.Text = milliseconds.ToString("00");
            mainWindow.Seconds.Text = seconds.ToString("00");
            mainWindow.Minutes.Text = minutes.ToString("00");

        }

        public void StartShooting()
        {
            if (spaceCreaturesshootTimer != null) return;

            spaceCreaturesshootTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(200) };
            spaceCreaturesshootTimer.Tick += (s, e) =>
            {
                foreach (var c in mainWindow.movingObstacles.spaceCreatures)
                    if (c.Name == "SpaceCreature2" && random.Next(0, 20) == 0)
                    {
                        Shoot(c);
                    }
                    else if (c.Name.Contains("UFO") && c.Visibility==Visibility.Visible)
                    {
                        Shoot(c);
                    }

            };
            spaceCreaturesshootTimer.Start();
        }

        public void Shoot(Rectangle c)
        {
            Rectangle b;

            // اگر شلیک کننده UFO بود
            if (c.Name.Contains("UFO"))
            {
                b = new Rectangle
                {
                    Width = 30,       // بزرگتر
                    Height = 30,
                    Fill = mainWindow.creatObstacles.UfoBullet 
                };
            }
            else
            {
                // گلوله پیشفرض موجودات فضایی
                b = new Rectangle
                {
                    Width = 12,
                    Height = 20,
                    Fill = mainWindow.creatObstacles.spaceCreaturesBullet
                };
            }

            Canvas.SetLeft(b, Canvas.GetLeft(c) + c.Width / 2 - b.Width / 2);
            Canvas.SetTop(b, Canvas.GetTop(c) + c.Height);

            StartBulletAnimation(b);

            mainWindow.scrollContent3.Children.Add(b);
            bullets.Add(b);
        }

        DispatcherTimer timer = new DispatcherTimer();

        public void StartBulletAnimation(Rectangle b)
        {
            // تایمر مخصوص این گلوله
            DispatcherTimer t = new DispatcherTimer();
            t.Interval = TimeSpan.FromMilliseconds(120);

            t.Tick += (s, e) =>
            {
                // اگر گلوله از Canvas حذف شده بود → تایمر را متوقف کن
                if (!mainWindow.scrollContent3.Children.Contains(b))
                {
                    t.Stop();
                    return;
                }

                // انیمیشن سوئیچ بین دو فریم
                if (b.Fill == mainWindow.creatObstacles.spaceCreaturesBullet)
                    b.Fill = mainWindow.creatObstacles.spaceCreaturesBullet2;
                else if (b.Fill == mainWindow.creatObstacles.spaceCreaturesBullet2)
                    b.Fill = mainWindow.creatObstacles.spaceCreaturesBullet;
            };

            t.Start();
        }

        public void StartBulletMovement()
        {
            if (bulletTimer != null) return;
            
            bulletTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(16) };
            bulletTimer.Tick += (s, e) =>
            {
                for (int i = bullets.Count - 1; i >= 0; i--)
                {
                    var b = bullets[i];
                    Canvas.SetTop(b, Canvas.GetTop(b) + 3);

                    if (Canvas.GetTop(b) > mainWindow.gameCanvas.Height)
                    {
                        bullets.RemoveAt(i);
                        mainWindow.scrollContent3.Children.Remove(b);
                    }
                }
            };
            bulletTimer.Start();
        }
        public void AnimateHelicopters(Canvas canvas)
        {
            foreach (var rect in canvas.Children.OfType<Rectangle>())
            {
                if (rect.Name == "Helicopter") ChangeHelicopterFrame(rect);
            }

            heliFrame++;
            if (heliFrame > 2) heliFrame = 0;   // چون 0 , 1 , 2 داریم
        }

        private void ChangeHelicopterFrame(Rectangle heli)
        {
            if (heli.Name != "Destroyed")
            {
                // تشخیص جهت از روی تصویر فعلی
                bool isLeft = heli.Fill == mainWindow.creatObstacles.LeftHeliFrames[0] ||
                              heli.Fill == mainWindow.creatObstacles.LeftHeliFrames[1] ||
                              heli.Fill == mainWindow.creatObstacles.LeftHeliFrames[2];

                if (isLeft)
                    heli.Fill = mainWindow.creatObstacles.LeftHeliFrames[heliFrame];
                else
                    heli.Fill = mainWindow.creatObstacles.RightHeliFrames[heliFrame];
            }
        }

        public async Task TypeTextAsync(string fullText, int delay = 40)
        {
            mainWindow.tbShayanDialog.Text = "";
            foreach (char c in fullText)
            {
                mainWindow.tbShayanDialog.Text += c;
                if (c != '!' && c != ' ' && c != '.') mainWindow.Sound.ShayanspeakSound(); 
                await Task.Delay(delay);
            }
        }



    }
}
