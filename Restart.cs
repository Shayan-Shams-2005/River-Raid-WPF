using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Shapes;
using System.Security.Cryptography.Xml;

namespace River_Raid_WPF
{
    public class Restart
    {

        Random random = new Random();

        public bool inputEnabled = true;
        public bool isGameOver = false;
        public bool GameStarted = false;
        public bool isStarting = true;
        public bool ClearedObsatacles = false;

        public MainWindow mainWindow;
        public Restart(MainWindow mainwindow)
        {
            this.mainWindow = mainwindow;
        }

        public void ResetGameTimer()
        {
            mainWindow.animation.milliseconds = 0;
            mainWindow.animation.seconds = 0;
            mainWindow.animation.minutes = 0;
            mainWindow.MilliSeconds.Text = "00";
            mainWindow.Seconds.Text = "00";
            mainWindow.Minutes.Text = "00";
        }

        public void GameStart()
        {
            if (!GameStarted)
            {
                if (mainWindow.LevelCount.Text == "0")
                {
                    mainWindow.Sound.BackgroundMusic();
                }
                mainWindow.animation.FuelAnimation();
                mainWindow.TimerRecord.Start();
                mainWindow.gameTimer.Start();

                if (mainWindow.tbHealth.Text == "5")
                {
                    int StartDialog = random.Next(0, 14);
                    mainWindow.tbShayanDialog.Text = mainWindow.shayanDialog.Dialog[StartDialog];
                    mainWindow.animation.AnimateShayanOnce();
                }
                else if (mainWindow.tbHealth.Text == "1")
                {
                    mainWindow.animation.TypeTextAsync("Last Chance,Pilot!",40);
                    mainWindow.animation.AnimateShayanOnce();
                }
                GameStarted = true;
            }

        }
        public void ClearObstacles(Canvas canvas)
        {
            for (int i = canvas.Children.Count - 1; i >= 0; i--)
            {
                if (canvas.Children[i] is Rectangle rect)
                {

                    if (rect.Name == "Helicopter" ||
                    rect.Name == "Ship" ||
                    rect.Name.ToString().Contains("Fuel") && rect.Fill !=mainWindow.creatObstacles.SpaceFuelImage && rect.Fill != mainWindow.creatObstacles.DestroyedSpaceFuelImage)
                    {
                        canvas.Children.RemoveAt(i);
                    }

                    if(rect.Name == "SpaceCreature2" || rect.Name == "SpaceCreature1")
                    {
                        canvas.Children.RemoveAt(i);
                        mainWindow.movingObstacles.spaceCreatures.Remove(rect);
                        ClearedObsatacles = true;
                    }
                }
            }
        }
        public async void GameOver()
        {
            if (isGameOver) return; // جلوگیری از چندبار اجرا
            isGameOver = true;

            //Explosion of airplane due to collision with obstacles
            mainWindow.airPlane.Source = mainWindow.plane.DestroyedPlaneImage.ImageSource;


            mainWindow.gameTimer.Stop();
            mainWindow.animation.fuelStoryboard.Stop();

            inputEnabled = false;

            if (mainWindow.tbHealth.Text == "1")
            {
                int TrueGameOverDialog = random.Next(55, 64);
                mainWindow.tbShayanDialog.Text = mainWindow.shayanDialog.Dialog[TrueGameOverDialog];
                mainWindow.animation.AnimateShayanOnce();
            }
            await Task.Delay(2000);

            mainWindow.tbHealth.Text = (int.Parse(mainWindow.tbHealth.Text) - 1).ToString();
            tbHealthColor();


            if (mainWindow.tbHealth.Text == "0")
            {
                mainWindow.Sound.GameOverSound();
                mainWindow.LevelCount.Text = "0";
                mainWindow.tbScore.Text = "0";
                ResetGameTimer();
                StartUp();
                mainWindow.scroll.levelCounterColor();
            }
            else
            {
                ResetGame();
            }
            isGameOver = false; // ریست برای بار بعد
        }

        public void tbHealthColor()
        {
            if (mainWindow.tbHealth.Text == "5") mainWindow.tbHealth.Foreground = Brushes.LimeGreen;
            if (mainWindow.tbHealth.Text == "4") mainWindow.tbHealth.Foreground = Brushes.GreenYellow;
            if (mainWindow.tbHealth.Text == "3") mainWindow.tbHealth.Foreground = Brushes.Orange;
            if (mainWindow.tbHealth.Text == "2") mainWindow.tbHealth.Foreground = Brushes.OrangeRed;
            if (mainWindow.tbHealth.Text == "1") mainWindow.tbHealth.Foreground = Brushes.Red;

        }

        //this is for start up or after 3 times gameover
        public void StartUp()
        {
            ResetGame();
            mainWindow.leftLand.Height = 4525;
            mainWindow.rightLand.Height = 4525;
            Canvas.SetTop(mainWindow.scrollContent1, -400);
            Canvas.SetTop(mainWindow.scrollContent3, -8650);
            mainWindow.tbHealth.Text = "5";
            mainWindow.DarkGreenObstacle1.Height = 400;
            mainWindow.DarkGreenObstacle1.Width = 100;
            Canvas.SetTop(mainWindow.DarkGreenObstacle1, 2400);
            Canvas.SetLeft(mainWindow.DarkGreenObstacle1, 350);
            mainWindow.DarkGreenObstacle2.Height = 400;
            mainWindow.DarkGreenObstacle2.Width = 400;
            Canvas.SetTop(mainWindow.DarkGreenObstacle2, 850);
            Canvas.SetLeft(mainWindow.DarkGreenObstacle2, 200);
            isStarting = true;
            mainWindow.Sound.BackgroundMusic();
            inputEnabled = false;
            mainWindow.gameTimer.Start();
            tbHealthColor();

        }


        private void ResetGame()
        {

            GameStarted = false;

            ClearObstacles(mainWindow.scrollContent1);
            ClearObstacles(mainWindow.scrollContent2);
            ClearObstacles(mainWindow.scrollContent3);


            if (mainWindow.LevelCount.Text == "0" || isStarting)
            {
                mainWindow.creatObstacles.Run1();
                mainWindow.movingObstacles.movingCount = 100;
                mainWindow.movingObstacles.Obstaclespeed = 1;
                Canvas.SetTop(mainWindow.scrollContent1, 0);
                Canvas.SetTop(mainWindow.scrollContent2, -8325);
                mainWindow.Gate1.Visibility = Visibility.Collapsed;

            }
            else if (mainWindow.LevelCount.Text == "1")
            {
                mainWindow.creatObstacles.Run1();
                mainWindow.movingObstacles.movingCount = 50;
                mainWindow.movingObstacles.Obstaclespeed = 2;
                Canvas.SetTop(mainWindow.scrollContent1, -4510);
                Canvas.SetTop(mainWindow.scrollContent2, -4180);

            }
            else if (mainWindow.LevelCount.Text == "2")
            {
                mainWindow.creatObstacles.Run2();
                mainWindow.movingObstacles.movingCount = 25;
                mainWindow.movingObstacles.Obstaclespeed = 3;
                Canvas.SetTop(mainWindow.scrollContent1, 0);
                Canvas.SetTop(mainWindow.scrollContent2, -8325);
                mainWindow.Gate1.Visibility = Visibility.Collapsed;

            }
            else if (mainWindow.LevelCount.Text == "3")
            {
                mainWindow.creatObstacles.Run2();
                mainWindow.movingObstacles.movingCount = 10;
                mainWindow.movingObstacles.Obstaclespeed = 3;
                Canvas.SetTop(mainWindow.scrollContent1, -4510);
                Canvas.SetTop(mainWindow.scrollContent2, -4180);
            }
            else if (mainWindow.LevelCount.Text == "4")
            {
                mainWindow.creatObstacles.Run3();
                mainWindow.movingObstacles.movingCount = 5;
                mainWindow.movingObstacles.Obstaclespeed = 4;
                Canvas.SetTop(mainWindow.scrollContent1, 0);
                Canvas.SetTop(mainWindow.scrollContent2, -8325);
                mainWindow.Gate1.Visibility = Visibility.Collapsed;
            }
            else if (mainWindow.LevelCount.Text == "5")
            {
                mainWindow.creatObstacles.Run3();
                mainWindow.movingObstacles.movingCount = 1;
                mainWindow.movingObstacles.Obstaclespeed = 5;
                Canvas.SetTop(mainWindow.scrollContent1, -4510);
                Canvas.SetTop(mainWindow.scrollContent2, -4180);
            }
            else if (int.Parse(mainWindow.LevelCount.Text) >= 6)
            {
               mainWindow.UFO1.Visibility= Visibility.Collapsed;
               mainWindow.UFO2.Visibility = Visibility.Collapsed;
               mainWindow.Fuel1.Name = "Fuel1";
               mainWindow.Fuel2.Name = "Fuel2";
               Canvas.SetTop(mainWindow.Fuel1, -300);
               Canvas.SetTop(mainWindow.Fuel2, -300);
               mainWindow.scroll.SpaceLevelCount = 2;
            }

            //reset airplane position and image
            Canvas.SetTop(mainWindow.airPlane, 230);
            Canvas.SetLeft(mainWindow.airPlane, 365);
            mainWindow.airPlane.Source = mainWindow.plane.PlaneImage.ImageSource;


            if (mainWindow.animation.fuelStoryboard != null) mainWindow.animation.fuelStoryboard.Stop();

            Canvas.SetLeft(mainWindow.FuelDisplay, 320);
            mainWindow.animation.FuelClearationTime = 15;           // زمان پر شدن سوخت رو ریست کن
            mainWindow.animation.FuelAnimation();
            mainWindow.animation.fuelStoryboard.Stop();
            inputEnabled = true;

            //reset scrollspeed 
            mainWindow.scroll.scrollSpeed = 4;
            Canvas.SetTop(mainWindow.SpeedRect, 47);
        }

    }
}
