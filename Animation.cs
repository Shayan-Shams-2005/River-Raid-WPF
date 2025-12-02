using System;
using System.Collections.Generic;
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
namespace River_Raid_WPF
{
    public class Animation
    {
        public Storyboard fuelStoryboard;
        
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
        public DispatcherTimer shootTimer = new DispatcherTimer();
        public int bulletSpeed = 25;

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
        public async Task KingAnimation()
        {
            AnimationIsPlaying = true;
            mainWindow.restart.inputEnabled = false;
            mainWindow.Shayan.Source = new BitmapImage(new Uri("pack://application:,,,/Pictures/Shayan Shams1.png"));
            mainWindow.animation.TypeTextAsync("This is The Last!",40);
            mainWindow.DialogBorder.Visibility = Visibility.Visible;

            await Task.Delay(2000);
            mainWindow.DialogBorder.Visibility = Visibility.Hidden;

            mainWindow.animation.TypeTextAsync("Shoot That!",40);
            mainWindow.DialogBorder.Visibility = Visibility.Visible;

            await Task.Delay(2000);
            mainWindow.Shayan.Source = new BitmapImage(new Uri("pack://application:,,,/Pictures/Shayan Shams.png"));
            mainWindow.DialogBorder.Visibility = Visibility.Hidden;

            //King
            mainWindow.King.Visibility = Visibility.Visible;

            mainWindow.animation.TypeTextAsync("Pls  Stop!",40);
            mainWindow.KingsDialogBorder.Visibility = Visibility.Visible;

            await Task.Delay(2000);
            mainWindow.KingsDialogBorder.Visibility = Visibility.Hidden;

            mainWindow.restart.inputEnabled = true;
            AnimationIsPlaying = false;

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


        public void Shot()
        {
            mainWindow.Sound.ShootingSound();

            Rectangle bullet = new Rectangle
            {
                Width = 5,
                Height = 15,
                Fill = Brushes.Gray
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
            Shot();
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
                    Width = 6,
                    Height = 10,
                    Fill = mainWindow.creatObstacles.spaceCreaturesBullet
                };
            }

            Canvas.SetLeft(b, Canvas.GetLeft(c) + c.Width / 2 - b.Width / 2);
            Canvas.SetTop(b, Canvas.GetTop(c) + c.Height);

            mainWindow.scrollContent3.Children.Add(b);
            bullets.Add(b);
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
                    Canvas.SetTop(b, Canvas.GetTop(b) + 6);

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
