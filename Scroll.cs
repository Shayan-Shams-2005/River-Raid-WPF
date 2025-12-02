using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace River_Raid_WPF
{
    public class Scroll
    {
        public double scrollSpeed = 5;
        public double SpaceLevelCount = 0;
        private double top = 0;
        bool spaceIntroPlayed = false;
        public bool InvokedShowUFO1 = false;
        public bool InvokedShowUFO2= false;


        public MainWindow mainWindow;

        public Scroll(MainWindow mainwindow) => mainWindow = mainwindow;

        public void ScrollElement(FrameworkElement element)
        {
            RunChoosing();

            top = Canvas.GetTop(element);
            double scrollContent1Top = Canvas.GetTop(mainWindow.scrollContent1);
            double scrollContent2Top = Canvas.GetTop(mainWindow.scrollContent2);
            double scrollContent3Top = Canvas.GetTop(mainWindow.scrollContent3);
            int level = int.Parse(mainWindow.LevelCount.Text);

            if (scrollContent1Top <= 8800)
                Canvas.SetTop(element, top + scrollSpeed);

            if (level <= 3)
            {
                if (scrollContent1Top >= 4125)
                {
                    Canvas.SetTop(mainWindow.scrollContent1, scrollContent2Top - 330);
                    ShowGate(mainWindow.Gate1);
                    UpdateLevel();
                }

                if (scrollContent2Top >= 325)
                {
                    Canvas.SetTop(mainWindow.scrollContent2, scrollContent1Top - 8325);
                    ShowGate(mainWindow.Gate2);
                    UpdateLevel();
                    mainWindow.movingObstacles.ObstaclesMovingChange();
                    DarrkGreenObstacles();
                }
            }
            else if ((level <= 4 && scrollContent1Top >= 4125) || (level <= 5 && scrollContent1Top >= 8550))
            {
                UpdateLevel();
            }
            else if ((level == 5 || level == 6) && scrollContent3Top <= 20)
            {
                Canvas.SetTop(mainWindow.scrollContent3, Canvas.GetTop(mainWindow.scrollContent2) - 275);
                if (Canvas.GetTop(mainWindow.scrollContent3) >= 0) { Canvas.SetTop(mainWindow.scrollContent3, 0); }
            }

            if (Canvas.GetTop(mainWindow.scrollContent3) == 0)
                ScrollSpaceContent();
        }

        public void ControlScrollSpeed()
        {
            scrollSpeed = Math.Max(2, Math.Min(scrollSpeed, 10));
            Canvas.SetTop(mainWindow.SpeedRect, scrollSpeed switch
            {
                2 => 61,
                4 => 47,
                6 => 33,
                8 => 19,
                10 => 5,
                _ => Canvas.GetTop(mainWindow.SpeedRect)
            });
        }

        private void ShowGate(Shape gate)
        {
            gate.Fill = Brushes.Orange;
            gate.Visibility = Visibility.Visible;
        }

        private void UpdateLevel()
        {
            int level = int.Parse(mainWindow.LevelCount.Text) + 1;
            mainWindow.LevelCount.Text = level.ToString();
            levelCounterColor();

        }


        public async Task ScrollSpaceContent()
        {
            double speed =2;
            MoveSpace(mainWindow.Space1, mainWindow.Space2, speed);
            MoveSpace(mainWindow.Space2, mainWindow.Space1, speed);

            if (SpaceLevelCount <= 2 && !spaceIntroPlayed && !mainWindow.creatObstacles.Creatures1Creating && mainWindow.LevelCount.Text=="6")
            {
                mainWindow.animation.ShayanSpaceIntroAnimation();
                spaceIntroPlayed = true;
            }

            if (SpaceLevelCount <= 2 && !spaceIntroPlayed && !mainWindow.creatObstacles.Creatures1Creating && mainWindow.LevelCount.Text == "7")
            {
                mainWindow.animation.ShayanSpaceoAnimation2();
                spaceIntroPlayed = true;
            }
            if (!spaceIntroPlayed && !mainWindow.creatObstacles.Creatures1Creating && mainWindow.MysteryBox.Visibility==Visibility.Visible && mainWindow.LevelCount.Text == "8")
            {
                mainWindow.animation.TypeTextAsync("DESTROY! DESTROY!",40);
                mainWindow.animation.AnimateShayanOnce();
                spaceIntroPlayed = true;
            }

            if (SpaceLevelCount == 2 && mainWindow.MysteryBox.Fill != Brushes.Red && !mainWindow.animation.AnimationIsPlaying)
            {
                mainWindow.MysteryBox.Visibility = Visibility.Visible;
            }

            if (mainWindow.LevelCount.Text == "9" && mainWindow.MysteryBox.Visibility==Visibility.Visible && mainWindow.MysteryBox.Fill==Brushes.MediumPurple && !spaceIntroPlayed && !mainWindow.creatObstacles.Creatures1Creating)
            {
                mainWindow.Sound.currentPlayer.Volume = 0.2;
                spaceIntroPlayed=true;
            }


            if (mainWindow.MysteryBox.Fill == Brushes.Red &&
                mainWindow.MysteryBox.Visibility == Visibility.Collapsed)
            {
                if (int.Parse(mainWindow.LevelCount.Text) <=9 && int.Parse(mainWindow.LevelCount.Text)>=7 && !AllEnemeysAreClear() && mainWindow.creatObstacles.Creatures1Created)
                {

                    if (mainWindow.UFO1.Visibility != Visibility.Visible && !InvokedShowUFO1)
                    {
                        ShowUFOAsync(mainWindow.UFO1);
                        InvokedShowUFO1= true;

                    }
                    if (mainWindow.UFO2.Visibility != Visibility.Visible && !InvokedShowUFO2)
                    {
                        ShowUFOAsync(mainWindow.UFO2);
                        InvokedShowUFO2 = true;
                    }

                }


                if (Canvas.GetTop(mainWindow.Fuel1)<=200)
                {
                    Canvas.SetTop(mainWindow.Fuel1,Canvas.GetTop(mainWindow.Fuel1) + 1);
                }
                if (Canvas.GetTop(mainWindow.Fuel2) <= 200)
                {
                    Canvas.SetTop(mainWindow.Fuel2, Canvas.GetTop(mainWindow.Fuel2) + 1);
                }
                if (mainWindow.Fuel1.Visibility==Visibility.Collapsed)
                {
                    Canvas.SetTop(mainWindow.Fuel1, -300);
                    mainWindow.Fuel1.Fill = mainWindow.creatObstacles.SpaceFuelImage;
                    mainWindow.Fuel1.Visibility=Visibility.Visible;
                    mainWindow.Fuel1.Name = "Fuel1";
                }
                if (mainWindow.Fuel2.Visibility == Visibility.Collapsed)
                {
                    Canvas.SetTop(mainWindow.Fuel2, -300);
                    mainWindow.Fuel2.Fill = mainWindow.creatObstacles.SpaceFuelImage;
                    mainWindow.Fuel2.Visibility = Visibility.Visible;
                    mainWindow.Fuel2.Name = "Fuel2";
                }


                if (mainWindow.LevelCount.Text == "6" && !mainWindow.creatObstacles.Creatures1Creating)
                {
                    mainWindow.creatObstacles.CreatSpaceCreature1(
                    mainWindow.creatObstacles.Run1Creatures1Left,
                    mainWindow.creatObstacles.Run1Creatures1Top,
                    mainWindow.scrollContent3
                    );
                }
                if (mainWindow.LevelCount.Text == "7" && !mainWindow.creatObstacles.Creatures1Creating && !mainWindow.creatObstacles.Creatures2Creating)
                {
                    mainWindow.creatObstacles.CreatSpaceCreature1(
                    mainWindow.creatObstacles.Run2Creatures1Left,
                    mainWindow.creatObstacles.Run2Creatures1Top,
                    mainWindow.scrollContent3
                     );

                   mainWindow.creatObstacles.CreatSpaceCreature2(
                   mainWindow.creatObstacles.Run2Creatures2Left,
                   mainWindow.creatObstacles.Run2Creatures2Top,
                   mainWindow.scrollContent3
                    );

                }
                if (mainWindow.LevelCount.Text == "8" && !mainWindow.creatObstacles.Creatures1Creating && !mainWindow.creatObstacles.Creatures2Creating)
                {
                    mainWindow.creatObstacles.CreatSpaceCreature1(
                        mainWindow.creatObstacles.Run3Creatures1Left,
                        mainWindow.creatObstacles.Run3Creatures1Top,
                        mainWindow.scrollContent3
                         );

                    mainWindow.creatObstacles.CreatSpaceCreature2(
                    mainWindow.creatObstacles.Run3Creatures2Left,
                    mainWindow.creatObstacles.Run3Creatures2Top,
                    mainWindow.scrollContent3
                     );
                }
                if (mainWindow.LevelCount.Text == "9" && !mainWindow.creatObstacles.Creatures1Creating && !mainWindow.creatObstacles.Creatures2Creating)
                {
                    mainWindow.creatObstacles.CreatSpaceCreature1(
                        mainWindow.creatObstacles.Run4Creatures1Left,
                        mainWindow.creatObstacles.Run4Creatures1Top,
                        mainWindow.scrollContent3
                         );

                    mainWindow.creatObstacles.CreatSpaceCreature2(
                    mainWindow.creatObstacles.Run4Creatures2Left,
                    mainWindow.creatObstacles.Run4Creatures2Top,
                    mainWindow.scrollContent3
                     );

                }
            }

            if (AllEnemeysAreClear() && SpaceLevelCount >= 2)
            {
                SpaceLevelCount = 0;
                mainWindow.MysteryBox.Fill = Brushes.MediumPurple;
                HideUFO();
                spaceIntroPlayed = false;
                mainWindow.creatObstacles.Creatures1Created = false;
                mainWindow.creatObstacles.Creatures1Creating = false;
                mainWindow.creatObstacles.Creatures2Created = false;
                mainWindow.creatObstacles.Creatures2Creating = false;
                if (!mainWindow.restart.ClearedObsatacles)
                {
                    UpdateLevel();
                }
            }

        }
        
        public async Task HideUFO()
        {
            await Task.Delay(1000);
            mainWindow.UFO1.Visibility = Visibility.Collapsed;
            mainWindow.UFO2.Visibility = Visibility.Collapsed;
        }

        public async Task ShowUFOAsync(Rectangle ufo)
        {
            ImageBrush originalImage = null;

            if (ufo.Fill == mainWindow.creatObstacles.DestroyedUFOImage)
                originalImage = mainWindow.creatObstacles.UFOImage;
            else if (ufo.Fill == mainWindow.creatObstacles.DestroyedRightUFOImage)
                originalImage = mainWindow.creatObstacles.RightUFOImage;

            if (originalImage == null) return;

            // نیازی به Collapsed قبل از Delay نیست
            await Task.Delay(2000);

            ufo.Fill = originalImage;
            ufo.Visibility = Visibility.Visible;
        }



        private void MoveSpace(FrameworkElement space, FrameworkElement otherSpace, double speed)
        {
            double top = Canvas.GetTop(space) + speed;
            if (top >= 325)
            {
                top = Canvas.GetTop(otherSpace) - 325;
                if (mainWindow.MysteryBox.Visibility == Visibility.Collapsed)
                    SpaceLevelCount++;
            }
            Canvas.SetTop(space, top);
        }

        public void RunChoosing()
        {
            double scrollContent2Top = Canvas.GetTop(mainWindow.scrollContent2);
            if (scrollContent2Top >= 270)
            {
                mainWindow.restart.ClearObstacles(mainWindow.scrollContent1);
                mainWindow.restart.ClearObstacles(mainWindow.scrollContent2);

                switch (mainWindow.LevelCount.Text)
                {
                    case "1": mainWindow.creatObstacles.Run2(); break;
                    case "3": mainWindow.creatObstacles.Run3(); break;
                }
            }
        }

        public void levelCounterColor()
        {
            int level = int.Parse(mainWindow.LevelCount.Text);
            mainWindow.LevelCount.Foreground = level switch
            {
                <= 2 => Brushes.Red,
                <= 4 => Brushes.OrangeRed,
                <= 6 => Brushes.Orange,
                <= 8 => Brushes.LimeGreen,
                9 => Brushes.DarkGreen,
                10 => Brushes.Yellow,
                _ => mainWindow.LevelCount.Foreground
            };
        }

        public void DarrkGreenObstacles()
        {
            ResizeObstacle(mainWindow.DarkGreenObstacle1, mainWindow.DarkGreenObstacle1.Height == 400 ? 200 : 100, mainWindow.DarkGreenObstacle1.Height == 400 ? 200 : 100);
            ResizeObstacle(mainWindow.DarkGreenObstacle2, 100, 100);
        }

        private void ResizeObstacle(FrameworkElement obstacle, double widthIncrease, double heightIncrease)
        {
            double top = Canvas.GetTop(obstacle);
            if (top < 0) return;
            Canvas.SetTop(obstacle, top - heightIncrease / 2);
            Canvas.SetLeft(obstacle, Canvas.GetLeft(obstacle) - widthIncrease / 2);
            obstacle.Width += widthIncrease;
            obstacle.Height += heightIncrease;
        }
       public bool AllEnemeysAreClear()
        {
            if (mainWindow.movingObstacles.spaceCreatures.Count ==2 && ((mainWindow.LevelCount.Text == "6" && mainWindow.creatObstacles.Creatures1Created) ||
                (mainWindow.LevelCount.Text == "7" || mainWindow.LevelCount.Text=="8" || mainWindow.LevelCount.Text=="9") && mainWindow.creatObstacles.Creatures1Created && mainWindow.creatObstacles.Creatures2Created))
            {
                return true;
            }
            return false;
        }


    }
}
