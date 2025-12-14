using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace River_Raid_WPF
{
    public class Collision
    {
        public Random random = new Random();

        public MainWindow mainWindow;

        public int PlanetShootCount = 0;
        public int SpaceShipShootCount = 0;

        public bool SpaceShipDestroyed = false;

        public Collision(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        public async Task CheckCollision()
        {
            double airplaneX = Canvas.GetLeft(mainWindow.airPlane) + 12;
            double airplaneY = Canvas.GetTop(mainWindow.airPlane) + 20;
            Rect airplaneRect = new Rect(airplaneX, airplaneY,
                                         mainWindow.airPlane.Width - 20,
                                         mainWindow.airPlane.Height - 50);

            var obstacles = mainWindow.scrollContent1.Children.OfType<Rectangle>()
                              .Concat(mainWindow.scrollContent2.Children.OfType<Rectangle>())
                              .Concat(mainWindow.scrollContent3.Children.OfType<Rectangle>())
                              .Where(r => r.Visibility == Visibility.Visible)
                              .ToList();

            var bullets = mainWindow.unScrollingContent.Children.OfType<Rectangle>()
                              .Where(b => b.Fill == mainWindow.creatObstacles.planeBullet && b.Tag == null)
                              .ToList();

            // 📌 Transform فقط یک بار برای هر مانع
            var obstacleRects = obstacles.Select(rect =>
            {
                var t = rect.TransformToAncestor(mainWindow.gameCanvas);
                var p = t.Transform(new Point(0, 0));
                return (rect, new Rect(p.X, p.Y, rect.Width, rect.Height));
            }).ToList();

            // 📌 Transform فقط یک بار برای هر گلوله
            var bulletRects = bullets.Select(b =>
            {
                var t = b.TransformToAncestor(mainWindow.gameCanvas);
                var p = t.Transform(new Point(0, 0));
                var r = new Rect(p.X, p.Y, b.Width, b.Height);
                r.Inflate(20, 20); // مثل کد اصلیت بزرگ‌تر
                return (b, r);
            }).ToList();

            // -------------------------
            // برخورد با مانع - برخورد با گلوله
            // -------------------------

            foreach (var (rect, rectBounds) in obstacleRects)
            {
                // برخورد هواپیما
                if (IsObstacle(rect) && airplaneRect.IntersectsWith(rectBounds) && !IsDestroyed(rect))
                {
                    HandlePlaneCollision();
                    return;
                }

                // برخورد سوخت
                if (rect.Name.Contains("Fuel")  && !rect.Name.Contains("Used") && !IsDestroyed(rect) && airplaneRect.IntersectsWith(rectBounds))
                {
                    HandleFuelCollision(rect);
                }

                // گلوله‌ها
                foreach (var (bullet, bulletRect) in bulletRects)
                {
                    // فیلتر سریع – محدوده X
                    if (Math.Abs(bulletRect.X - rectBounds.X) > 150)
                        continue;

                    if (bulletRect.IntersectsWith(rectBounds) && IsDestroyable(rect))
                    {
                        await HandleBulletCollision(rect, bullet);
                        return;
                    }
                }
            }
        }


        private bool IsObstacle(Rectangle rect) =>
            rect.Tag != null && !rect.Name.ToString().Contains("Fuel") || rect.Fill==mainWindow.creatObstacles.UfoBullet || rect.Fill == Brushes.LimeGreen || rect.Fill == Brushes.DarkGreen
            || rect.Fill== mainWindow.creatObstacles.spaceCreaturesBullet || rect.Fill == mainWindow.creatObstacles.spaceCreaturesBullet2 || rect.Fill==Brushes.Orange; 

        private bool IsDestroyed(Rectangle rect) =>
            rect.Fill == mainWindow.creatObstacles.RightDestroyedHelicopter ||
            rect.Fill == mainWindow.creatObstacles.LeftDestroyedHelicopter ||
            rect.Fill == mainWindow.creatObstacles.RightDestroyedShip ||
            rect.Fill == mainWindow.creatObstacles.LeftDestroyedShip ||
            rect.Fill == mainWindow.creatObstacles.DestroyedFuel ||
            rect.Fill == mainWindow.creatObstacles.DestroyedSpaceCreature1Image ||
            rect.Fill == mainWindow.creatObstacles.DestroyedSpaceCreature2Image ||
            rect.Fill == mainWindow.creatObstacles.DestroyedUFOImage ||
            rect.Fill == mainWindow.creatObstacles.DestroyedRightUFOImage ||
            rect.Fill == mainWindow.creatObstacles.DestroyedSpaceFuelImage ||
            rect.Fill == mainWindow.creatObstacles.SpaceShip16 ||
            rect.Fill == mainWindow.creatObstacles.SpaceShip26 ||
            rect.Fill == mainWindow.creatObstacles.SpaceShip33 ||
            rect.Fill == mainWindow.creatObstacles.SpaceShip46 ||
            rect.Fill==Brushes.Red ||
            rect.Fill==mainWindow.creatObstacles.DestroyedSpaceCreaturesBullet ||
            rect.Fill == mainWindow.creatObstacles.DestroyedUfoBullet;

        private bool IsDestroyable(Rectangle rect) =>
            mainWindow.creatObstacles.RightHeliFrames.Contains(rect.Fill) ||
            mainWindow.creatObstacles.LeftHeliFrames.Contains(rect.Fill) ||
            rect.Fill == mainWindow.creatObstacles.RightShipImage ||
            rect.Fill == mainWindow.creatObstacles.LeftShipImage ||
            rect.Fill == mainWindow.creatObstacles.FuelImage ||
            rect.Fill == Brushes.Orange ||
            rect.Fill == mainWindow.creatObstacles.SpaceShip11 ||
            rect.Fill == mainWindow.creatObstacles.SpaceShip12 ||
            rect.Fill == mainWindow.creatObstacles.SpaceShip13 ||
            rect.Fill == mainWindow.creatObstacles.SpaceShip14 ||
            rect.Fill == mainWindow.creatObstacles.SpaceShip15 ||
            rect.Fill == mainWindow.creatObstacles.SpaceShip21 ||
            rect.Fill == mainWindow.creatObstacles.SpaceShip22 ||
            rect.Fill == mainWindow.creatObstacles.SpaceShip23 ||
            rect.Fill == mainWindow.creatObstacles.SpaceShip24 ||
            rect.Fill == mainWindow.creatObstacles.SpaceShip25 ||
             rect.Fill == mainWindow.creatObstacles.SpaceShip31 ||
            rect.Fill == mainWindow.creatObstacles.SpaceShip32 ||
             rect.Fill == mainWindow.creatObstacles.SpaceShip41 ||
            rect.Fill == mainWindow.creatObstacles.SpaceShip42 ||
            rect.Fill == mainWindow.creatObstacles.SpaceShip43 ||
            rect.Fill == mainWindow.creatObstacles.SpaceShip44 ||
            rect.Fill == mainWindow.creatObstacles.SpaceShip45 ||
            rect.Fill == mainWindow.creatObstacles.Mars ||
            mainWindow.creatObstacles.spaceCreatures1.Contains(rect.Fill)||
            mainWindow.creatObstacles.spaceCreatures2.Contains(rect.Fill) ||
            rect.Fill == mainWindow.creatObstacles.SpaceCreature2Image ||
            rect.Fill == mainWindow.creatObstacles.UFOImage ||
            rect.Fill == mainWindow.creatObstacles.RightUFOImage ||
            rect.Fill == mainWindow.creatObstacles.SpaceFuelImage|| 
            rect.Fill == mainWindow.creatObstacles.spaceCreaturesBullet||
            rect.Fill == mainWindow.creatObstacles.spaceCreaturesBullet2 ||
            rect.Fill == mainWindow.creatObstacles.UfoBullet || 
            rect.Fill==mainWindow.creatObstacles.UsedFuelImage ||
            rect.Fill==mainWindow.creatObstacles.usedSpaceFuelImage;

        private void HandlePlaneCollision()
        {
            if (int.Parse(mainWindow.LevelCount.Text) >= 6)
            {
                // mainWindow.Sound.PlaneExplosionSound();
                //mainWindow.restart.GameOver();
            }
            if (mainWindow.tbHealth.Text != "1")
            {
                int dialogIndex = random.Next(12, 22);
                mainWindow.tbShayanDialog.Text = mainWindow.shayanDialog.Dialog[dialogIndex];
                mainWindow.animation.AnimateShayanOnce();
            }
        }

        private void HandleFuelCollision(Rectangle rect)
        {
            rect.Name = "UsedFuel";
            if (rect.Fill == mainWindow.creatObstacles.FuelImage) rect.Fill = mainWindow.creatObstacles.UsedFuelImage;
            else if (rect.Fill == mainWindow.creatObstacles.SpaceFuelImage) rect.Fill = mainWindow.creatObstacles.usedSpaceFuelImage;
                mainWindow.animation.ChargeUpFuel(rect);
            mainWindow.Sound.FuelChargeSound();
        }

        private async Task HandleBulletCollision(Rectangle rect, Rectangle bullet)
        {
            bullet.Name = "Destroyed";
            mainWindow.Sound.ObstacleExplosionSound(rect);
            mainWindow.Score(rect);
            mainWindow.unScrollingContent.Children.Remove(bullet);

            if (rect.Name.ToString() == "Helicopter")
            {
                rect.Name = "Destroyed";
                if (mainWindow.creatObstacles.RightHeliFrames.Contains(rect.Fill))
                {
                    rect.Fill = mainWindow.creatObstacles.RightDestroyedHelicopter;
                }
                else if (mainWindow.creatObstacles.LeftHeliFrames.Contains(rect.Fill))
                {
                    rect.Fill = mainWindow.creatObstacles.LeftDestroyedHelicopter;
                }
            }
            else if (rect.Name.ToString() == "Ship")
                rect.Fill = rect.Fill == mainWindow.creatObstacles.RightShipImage ? mainWindow.creatObstacles.RightDestroyedShip : mainWindow.creatObstacles.LeftDestroyedShip;
            else if (rect.Fill == mainWindow.creatObstacles.FuelImage) rect.Fill = mainWindow.creatObstacles.DestroyedFuel;
            else if (rect.Fill == mainWindow.creatObstacles.UsedFuelImage) rect.Fill = mainWindow.creatObstacles.DestroyedusedFuel;
            else if (rect.Fill == mainWindow.creatObstacles.SpaceFuelImage) rect.Fill = mainWindow.creatObstacles.DestroyedSpaceFuelImage;
            else if (rect.Fill == mainWindow.creatObstacles.usedSpaceFuelImage) rect.Fill = mainWindow.creatObstacles.DestroyedusedSpaceFuelImage;
            else if (rect.Name.ToString() == "SpaceCreature1")
            {
                rect.Fill = mainWindow.creatObstacles.DestroyedSpaceCreature1Image;
                mainWindow.movingObstacles.spaceCreatures.Remove(rect);
            }
            else if (rect.Name.ToString() == "SpaceCreature2")
            {
                rect.Fill = mainWindow.creatObstacles.DestroyedSpaceCreature2Image;
                mainWindow.movingObstacles.spaceCreatures.Remove(rect);
            }
            else if (rect.Fill == mainWindow.creatObstacles.UFOImage)
            {
                rect.Fill = mainWindow.creatObstacles.DestroyedUFOImage;
                if (rect == mainWindow.UFO1)
                {
                    mainWindow.scroll.InvokedShowUFO1 = false;
                }
                if (rect == mainWindow.UFO2)
                {
                    mainWindow.scroll.InvokedShowUFO2 = false;
                }
            }
            else if (rect.Fill == mainWindow.creatObstacles.RightUFOImage)
            {
                rect.Fill = mainWindow.creatObstacles.DestroyedRightUFOImage;

                if (rect == mainWindow.UFO1)
                {
                    mainWindow.scroll.InvokedShowUFO1 = false;
                }
                if (rect == mainWindow.UFO2)
                {
                    mainWindow.scroll.InvokedShowUFO2 = false;
                }
            }
            else if (rect.Fill == Brushes.Orange)
                rect.Fill = Brushes.Red;
            else if (rect.Name.Contains("SpaceShip") && rect.Fill != mainWindow.creatObstacles.SpaceShip31 && rect.Fill != mainWindow.creatObstacles.SpaceShip32)
            {
                SpaceShipShootCount++;
                if (SpaceShipShootCount == 5)
                {
                    if (rect.Fill == mainWindow.creatObstacles.SpaceShip11) rect.Fill = mainWindow.creatObstacles.SpaceShip12;
                    else if (rect.Fill == mainWindow.creatObstacles.SpaceShip12) rect.Fill = mainWindow.creatObstacles.SpaceShip13;
                    else if (rect.Fill == mainWindow.creatObstacles.SpaceShip13) rect.Fill = mainWindow.creatObstacles.SpaceShip14;
                    else if (rect.Fill == mainWindow.creatObstacles.SpaceShip14) rect.Fill = mainWindow.creatObstacles.SpaceShip15;
                    else if (rect.Fill == mainWindow.creatObstacles.SpaceShip21) rect.Fill = mainWindow.creatObstacles.SpaceShip22;
                    else if (rect.Fill == mainWindow.creatObstacles.SpaceShip22) rect.Fill = mainWindow.creatObstacles.SpaceShip23;
                    else if (rect.Fill == mainWindow.creatObstacles.SpaceShip23) rect.Fill = mainWindow.creatObstacles.SpaceShip24;
                    else if (rect.Fill == mainWindow.creatObstacles.SpaceShip24) rect.Fill = mainWindow.creatObstacles.SpaceShip25;
                    else if (rect.Fill == mainWindow.creatObstacles.SpaceShip41) rect.Fill = mainWindow.creatObstacles.SpaceShip42;
                    else if (rect.Fill == mainWindow.creatObstacles.SpaceShip42) rect.Fill = mainWindow.creatObstacles.SpaceShip43;
                    else if (rect.Fill == mainWindow.creatObstacles.SpaceShip43) rect.Fill = mainWindow.creatObstacles.SpaceShip44;
                    else if (rect.Fill == mainWindow.creatObstacles.SpaceShip44) rect.Fill = mainWindow.creatObstacles.SpaceShip45;
                    SpaceShipShootCount = 0;
                }
                else if (rect.Fill == mainWindow.creatObstacles.SpaceShip15 && SpaceShipShootCount==1)
                {
                    rect.Fill = mainWindow.creatObstacles.SpaceShip16;
                    SpaceShipDestroyed = true;
                    SpaceShipShootCount = 0;
                }

                else if (rect.Fill == mainWindow.creatObstacles.SpaceShip25 && SpaceShipShootCount == 1)
                {
                    rect.Fill = mainWindow.creatObstacles.SpaceShip26;
                    SpaceShipDestroyed = true;
                    SpaceShipShootCount = 0;
                }

                else if (rect.Fill == mainWindow.creatObstacles.SpaceShip45 && SpaceShipShootCount == 1)
                {
                    rect.Fill = mainWindow.creatObstacles.SpaceShip46;
                    SpaceShipDestroyed = true;
                    SpaceShipShootCount = 0;
                }
            }
            
        
            

            if (rect.Name.Contains("SpaceShip") && (rect.Fill == mainWindow.creatObstacles.SpaceShip31 || rect.Fill == mainWindow.creatObstacles.SpaceShip32))
            {
               SpaceShipShootCount++;
               if (rect.Fill == mainWindow.creatObstacles.SpaceShip31) rect.Fill = mainWindow.creatObstacles.SpaceShip32;
               else if (rect.Fill == mainWindow.creatObstacles.SpaceShip32) rect.Fill = mainWindow.creatObstacles.SpaceShip31;

               if(SpaceShipShootCount==20)
                {
                    rect.Fill = mainWindow.creatObstacles.SpaceShip33;
                    SpaceShipDestroyed = true;
                    SpaceShipShootCount = 0;
                }

            }
            else if (rect.Fill == mainWindow.creatObstacles.Mars && PlanetShootCount <= 4)
            {
                PlanetShootCount++;
                mainWindow.scroll.spaceIntroPlayed = false;
            }
            else if (rect.Fill == mainWindow.creatObstacles.Mars && PlanetShootCount == 5)
            {
                rect.Fill = Brushes.Red;
            }
            else if(rect.Fill==mainWindow.creatObstacles.spaceCreaturesBullet || rect.Fill==mainWindow.creatObstacles.spaceCreaturesBullet2)
            {
                rect.Width = 40;
                rect.Height = 40;
                Canvas.SetLeft(rect, Canvas.GetLeft(rect) - 14);
                Canvas.SetTop(rect, Canvas.GetTop(rect) - 14);
                rect.Fill = mainWindow.creatObstacles.DestroyedSpaceCreaturesBullet;
            }
            else if (rect.Fill == mainWindow.creatObstacles.UfoBullet) rect.Fill = mainWindow.creatObstacles.DestroyedUfoBullet;
                if (random.Next(0, 5) == 0)
            {
                int dialogIndex = rect.Name.ToString() switch
                {
                    "Helicopter" => random.Next(38, 50),
                    "Ship" => random.Next(38, 50),
                    "Fuel" => random.Next(72, 78),
                    _ => -1
                };
                if (dialogIndex != -1)
                {
                    mainWindow.tbShayanDialog.Text = mainWindow.shayanDialog.Dialog[dialogIndex];
                    mainWindow.animation.AnimateShayanOnce();
                }
            }
            

            await Task.Delay((int)(3 / mainWindow.scroll.scrollSpeed * 1000));
            if ((rect.Fill != mainWindow.creatObstacles.Mars && !rect.Name.Contains("SpaceShip")) || (rect.Fill == mainWindow.creatObstacles.Mars && PlanetShootCount == 5) || (rect.Name.Contains("SpaceShip") && SpaceShipDestroyed))
            {
                rect.Visibility = Visibility.Collapsed;
            }                    
               mainWindow.unScrollingContent.Children.Remove(bullet);
        }
    }
}
