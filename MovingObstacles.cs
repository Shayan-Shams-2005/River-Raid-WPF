using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace River_Raid_WPF
{
    public class MovingObstacles
    {

        Random random =new Random();

        public MainWindow mainWindow;

        public MovingObstacles(MainWindow mainwindow)
        {
            this.mainWindow = mainwindow;
        }

        public List<Rectangle> spaceCreatures = new();

        public  List<List<Rectangle>> creatureGroups = new();

        public string[] dirs = { "right", "right", "down", "down", "left", "left", "up", "up" };

        public double moveStep = 10;

        public bool moving = false;

        public int dirIndex = 0;
        public int movingCount = 100;
        public int Obstaclespeed = 1;

        public void ObstaclesMovement(Canvas canvas)
        {
            foreach (UIElement element in canvas.Children)
            {
                if (element is Rectangle rect)
                {
                    GeneralTransform transform = rect.TransformToAncestor(mainWindow.gameCanvas);
                    Point ObstaclePos = transform.Transform(new Point(0, 0));
                    Rect objRect = new Rect(ObstaclePos.X, ObstaclePos.Y, rect.Width, rect.Height);

                    Rect CanbeSeenRect = new Rect(
                        Canvas.GetLeft(mainWindow.CanbeSeen),
                        Canvas.GetTop(mainWindow.CanbeSeen),
                        mainWindow.CanbeSeen.Width,
                        mainWindow.CanbeSeen.Height
                    );

                    int Move = random.Next(0, movingCount);

                    if (objRect.IntersectsWith(CanbeSeenRect) &&
                        (mainWindow.creatObstacles.RightHeliFrames.Contains(rect.Fill) || rect.Fill == mainWindow.creatObstacles.RightShipImage ||
                         mainWindow.creatObstacles.LeftHeliFrames.Contains(rect.Fill) || rect.Fill == mainWindow.creatObstacles.LeftShipImage))
                    {
                        if (Move == 0)
                        {
                            // ✅ به جای ساخت تایمر جدید، فقط در لیست جهانی اضافه می‌کنیم
                            if (!movingObstacles.Any(x => x.rect == rect))
                                movingObstacles.Add((rect, canvas));
                        }
                    }
                }
            }

            // ✅ اگه تایمر جهانی هنوز ساخته نشده، همین‌جا بسازش
            if (globalMoveTimer == null)
            {
                globalMoveTimer = new DispatcherTimer();
                globalMoveTimer.Interval = TimeSpan.FromMilliseconds(16);
                globalMoveTimer.Tick += GlobalMoveTimer_Tick;
                globalMoveTimer.Start();
            }
        }


        // ⚙️ متد حرکت (جایگزین MoveObstacleUntilGreen)
        public void GlobalMoveTimer_Tick(object sender, EventArgs e)
        {
            List<(Rectangle rect, Canvas canvas)> toRemove = new();

            foreach (var (obstacle, canvas) in movingObstacles)
            {
                if (!canvas.Children.Contains(obstacle))
                {
                    toRemove.Add((obstacle, canvas));
                    continue;
                }

                GeneralTransform transform1 = obstacle.TransformToAncestor(mainWindow.gameCanvas);
                Point ObstaclePos = transform1.Transform(new Point(0, 0));
                Rect ObstacleRect = new Rect(ObstaclePos.X - 2, ObstaclePos.Y, obstacle.Width + 4, obstacle.Height);

                GeneralTransform transform2 = mainWindow.CantSeen.TransformToAncestor(mainWindow.gameCanvas);
                Point CantPos = transform2.Transform(new Point(0, 0));
                Rect CantBeSeenRect = new Rect(CantPos.X, CantPos.Y, mainWindow.CantSeen.Width, mainWindow.CantSeen.Height);

                double left = Canvas.GetLeft(obstacle);

                // ✅ حرکت مانع
                if (mainWindow.creatObstacles.RightHeliFrames.Contains(obstacle.Fill) || obstacle.Fill == mainWindow.creatObstacles.RightShipImage)
                    Canvas.SetLeft(obstacle, left + Obstaclespeed);
                else if (mainWindow.creatObstacles.LeftHeliFrames.Contains(obstacle.Fill) || obstacle.Fill ==mainWindow.creatObstacles.LeftShipImage)
                    Canvas.SetLeft(obstacle, left - Obstaclespeed);

                // 🚫 خارج از محدوده؟ حذف از لیست
                if (ObstacleRect.IntersectsWith(CantBeSeenRect))
                {
                    toRemove.Add((obstacle, canvas));
                    continue;
                }

                // ✅ بررسی برخورد با دیوار سبز
                foreach (UIElement element in canvas.Children)
                {
                    if (element is Rectangle rect &&
                        rect.Fill is SolidColorBrush brush &&
                        (brush.Color == Colors.LimeGreen || brush.Color == Colors.DarkGreen))
                    {
                        GeneralTransform transform3 = rect.TransformToAncestor(mainWindow.gameCanvas);
                        Point GreenPos = transform3.Transform(new Point(0, 0));
                        Rect GreenRect = new Rect(GreenPos.X, GreenPos.Y, rect.Width, rect.Height);

                        if (ObstacleRect.IntersectsWith(GreenRect))
                        {
                            if (mainWindow.creatObstacles.LeftHeliFrames.Contains(obstacle.Fill))
                            {
                                obstacle.Fill = mainWindow.creatObstacles.RightHeliFrames[0];
                                Canvas.SetLeft(obstacle, Canvas.GetLeft(obstacle) + 10);
                            }
                            else if (mainWindow.creatObstacles.RightHeliFrames.Contains(obstacle.Fill))
                            {
                                obstacle.Fill = mainWindow.creatObstacles.LeftHeliFrames[0];
                                Canvas.SetLeft(obstacle, Canvas.GetLeft(obstacle) - 10);
                            }
                            else if (obstacle.Fill == mainWindow.creatObstacles.LeftShipImage)
                            {
                                obstacle.Fill = mainWindow.creatObstacles.RightShipImage;
                                Canvas.SetLeft(obstacle, Canvas.GetLeft(obstacle) + 10);
                            }
                            else if (obstacle.Fill == mainWindow.creatObstacles.RightShipImage)
                            {
                                obstacle.Fill = mainWindow.creatObstacles.LeftShipImage;
                                Canvas.SetLeft(obstacle, Canvas.GetLeft(obstacle) - 10);
                            }
                        }
                    }
                }
            }

            // حذف مواردی که باید از حرکت خارج شن
            foreach (var obs in toRemove)
                movingObstacles.Remove(obs);
        }


        public HashSet<Rectangle> UnmovingObstacles = new(); // موانعی که در حال حرکت‌اند

        DispatcherTimer globalMoveTimer;
        List<(Rectangle rect, Canvas canvas)> movingObstacles = new();
        public void GroupCreatures()
        {
            creatureGroups.Clear();
            for (int i = 0; i < spaceCreatures.Count; i += 6)
                creatureGroups.Add(spaceCreatures.Skip(i).Take(6).ToList());
        }


        public async void StartMoveLoop()
        {
            if (moving) return;
            moving = true;

            mainWindow.animation.StartShooting();
            mainWindow.animation.StartBulletMovement();

            while (true)
            {
                string dir = dirs[dirIndex];
                await Task.WhenAll(creatureGroups.Select((g, i) => MoveGroup(g, dir, i * 100)));
                dirIndex = (dirIndex + 1) % dirs.Length;
            }
        }

        public async Task MoveGroup(List<Rectangle> g, string dir, int delay)
        {
            await Task.Delay(delay);
            foreach (var c in g)
            {
                double l = Canvas.GetLeft(c), t = Canvas.GetTop(c);
                if (dir == "right") Canvas.SetLeft(c, l + moveStep);
                else if (dir == "left") Canvas.SetLeft(c, l - moveStep);
                else if (dir == "down") Canvas.SetTop(c, t + moveStep);
                else if (dir == "up") Canvas.SetTop(c, t - moveStep);
                if(c.Fill== mainWindow.creatObstacles.spaceCreatures1[1]) c.Fill = mainWindow.creatObstacles.spaceCreatures1[0];
                else if (c.Fill == mainWindow.creatObstacles.spaceCreatures1[0]) c.Fill = mainWindow.creatObstacles.spaceCreatures1[1];
                if (c.Fill == mainWindow.creatObstacles.spaceCreatures2[1]) c.Fill = mainWindow.creatObstacles.spaceCreatures2[0];
                else if (c.Fill == mainWindow.creatObstacles.spaceCreatures2[0]) c.Fill = mainWindow.creatObstacles.spaceCreatures2[1];

            }
            await Task.Delay(300);
        }

        public void ObstaclesMovingChange()
        {
            if (mainWindow.LevelCount.Text == "0")
            {
                movingCount = 100;
                Obstaclespeed = 1;
            }
            if (mainWindow.LevelCount.Text == "1")
            {
                movingCount = 50;
                Obstaclespeed = 2;
            }
            if (mainWindow.LevelCount.Text == "2")
            {
                movingCount = 25;
                Obstaclespeed = 3;
            }
            if (mainWindow.LevelCount.Text == "3")
            {
                movingCount = 10;
                Obstaclespeed = 3;
            }
            if (mainWindow.LevelCount.Text == "4")
            {
                movingCount = 5;
                Obstaclespeed = 4;
            }
            if (mainWindow.LevelCount.Text == "4")
            {
                movingCount = 1;
                Obstaclespeed = 5;
            }
        }

        public void MoveUFO(UIElement ufo, double from, double to, double durationSeconds)
        {
            DoubleAnimation anim = new DoubleAnimation()
            {
                From = from,
                To = to,
                Duration = TimeSpan.FromSeconds(durationSeconds),
                AutoReverse = true,   // بعد از رسیدن، برگردد
                RepeatBehavior = RepeatBehavior.Forever // بی‌نهایت تکرار شود
            };
            ufo.BeginAnimation(Canvas.LeftProperty, anim);
        }

    }
}
