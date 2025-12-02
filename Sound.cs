using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace River_Raid_WPF
{
    public class Sound
    {
        public MainWindow mainWindow;
        public Sound(MainWindow mainwindow)
        {
            this.mainWindow = mainwindow;
        }

        //پخش کننده های صداهای بازی
        public MediaPlayer currentPlayer = new MediaPlayer();
        public MediaPlayer nextPlayer = new MediaPlayer();
        MediaPlayer obstacleExplosionPlayer = new MediaPlayer();
        MediaPlayer planeExplosionPlayer = new MediaPlayer();
        MediaPlayer fuelChargePlayer = new MediaPlayer();
        MediaPlayer shootingPlayer = new MediaPlayer();
        MediaPlayer gameOverPlayer = new MediaPlayer();
        MediaPlayer shayanSoundPlayer=new MediaPlayer();

        bool isplaying = false;
        string bgPath;
        string repeatBgPath;

        public async Task BackgroundMusic()
        {

            int level = int.Parse(mainWindow.LevelCount.Text);

            if(mainWindow.restart.isStarting)
            {
                bgPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sound/Background 1-1.mp3");
                repeatBgPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sound/Background 1-1.mp3");

                Play();
            }

            if (level < 6 && mainWindow.restart.GameStarted)
            {
                if (bgPath == System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sound/Background 1-1.mp3"))
                {
                    bgPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sound/Backgroud 1-2.mp3");
                    repeatBgPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sound/Backgroud 1-3.mp3");
                    
                   isplaying = false;
                   Play();
                }
                    
            }
            if (level == 6)
            {
                if (bgPath == System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sound/Backgroud 1-2.mp3"))
                {
                    bgPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sound/silence.wav");
                    repeatBgPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sound/silence.wav");

                    isplaying = false;
                    Play();
                }
                
            }
            if (mainWindow.MysteryBox.Fill == Brushes.Red && !mainWindow.creatObstacles.Creatures1Creating && level==7 && !mainWindow.restart.ClearedObsatacles)
            {
                if (bgPath == System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sound/silence.wav"))
                {
                    bgPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sound/Background 2-1.mp3");
                    repeatBgPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sound/Background 2-2.mp3");

                    isplaying = false;
                    Play();
                }
                
            }
            if(level==10 && !mainWindow.restart.ClearedObsatacles)
            {
                if (bgPath == System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sound/Background 2-1.mp3"))
                {
                    bgPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sound/Background 2-3.mp3");
                    repeatBgPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sound/silence.wav");

                    isplaying = false;
                    await Play();
                }
               
            }

            



        }

        bool isBusy = false;

        public async Task Play()
        {
            if (isBusy) return;
            isBusy = true;

            if (!isplaying)
            {
                nextPlayer.Open(new Uri(bgPath));
                await CrossFade(currentPlayer, nextPlayer, 0.5, 1200);

                var temp = currentPlayer;
                currentPlayer = nextPlayer;
                nextPlayer = temp;

                currentPlayer.MediaEnded -= CurrentPlayer_MediaEnded;
                currentPlayer.MediaEnded += CurrentPlayer_MediaEnded;

                isplaying = true;
            }

            isBusy = false;
        }

        // وقتی آهنگ تموم شد
        public void CurrentPlayer_MediaEnded(object sender, EventArgs e)
        {
            currentPlayer.Open(new Uri(repeatBgPath));
            currentPlayer.Volume = 0.5;
            currentPlayer.Play();
        }
        public async Task CrossFade(MediaPlayer fromPlayer, MediaPlayer toPlayer,
                             double targetVolume = 0.5, int durationMs = 1200)
        {
                int steps = 40; // نرم‌تر بودن
                double fadeOutStep = fromPlayer.Volume / steps;
                double fadeInStep = targetVolume / steps;
                int delay = durationMs / steps;

                toPlayer.Volume = 0;
                toPlayer.Play();

                for (int i = 0; i < steps; i++)
                {
                    fromPlayer.Volume -= fadeOutStep;
                    toPlayer.Volume += fadeInStep;
                    await Task.Delay(delay);
                }

                fromPlayer.Stop();
                fromPlayer.Volume = 0;
                toPlayer.Volume = targetVolume;
        }

        public void ShayanspeakSound()
        {
            string ShayanSoundPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sound/Shayan Sound.wav");
            shayanSoundPlayer.Open(new Uri(ShayanSoundPath));
            shayanSoundPlayer.Volume = 0.6;
            shayanSoundPlayer.Play();

        }


        public void PlaneExplosionSound()
        {
            string planeExplosionPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sound/Air Plane Explosion.mp3");
            planeExplosionPlayer.Open(new Uri(planeExplosionPath));
            planeExplosionPlayer.Volume = 0.6;
            planeExplosionPlayer.Play();
        }

        public void ObstacleExplosionSound(Rectangle rect)
        {
            string path;

            if (int.Parse(mainWindow.LevelCount.Text) < 6)
            {
                if (rect.Fill == Brushes.Orange)
                    path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sound/Huge Explosion.wav");
                else
                    path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sound/Obstacles Explosion.mp3");
            }
            else
            {
                if (rect.Fill == mainWindow.creatObstacles.UFOImage)
                    path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sound/UFO Explosion.wav");
                else if (rect.Fill == mainWindow.creatObstacles.UfoBullet)
                    path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sound/UFO Bullet Explosion.wav");
                else if (rect.Fill == mainWindow.creatObstacles.spaceCreaturesBullet)
                    path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sound/Creatures Bullet Explosion.wav");
                else if (rect.Fill == Brushes.MediumPurple)
                    path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sound/Mystery Box.wav");
                else
                    path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sound/Space Creatures Down.wav");
            }

            PlayOneShot(path, 1.0);
        }

        List<MediaPlayer> soundPlayers = new List<MediaPlayer>();

        private void PlayOneShot(string filePath, double volume = 0.6)
        {
            MediaPlayer player = new MediaPlayer();

            player.Volume = volume;

            player.MediaOpened += (s, e) =>
            {
                player.Play();
            };

            player.MediaEnded += (s, e) =>
            {
                player.Stop();
                player.Close();
                soundPlayers.Remove(player); // از لیست حذف شود
                player = null;
            };

            // در صورت خطا
            player.MediaFailed += (s, e) =>
            {
                soundPlayers.Remove(player);
                player.Close();
                player = null;
            };

            soundPlayers.Add(player); // اضافه کردن به Pool
            player.Open(new Uri(filePath));
        }



        public void FuelChargeSound()
        {
            string fuelChargePath;

            if (int.Parse(mainWindow.LevelCount.Text) < 6)
                fuelChargePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sound/Fuel Charge.mp3");
            else
                fuelChargePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sound/Space Fuel Charge.mp3");

            PlayOneShot(fuelChargePath, 0.6);
        }

        public void GameOverSound()
        {
            string gameOverPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sound/Game Over.mp3");
            gameOverPlayer.Open(new Uri(gameOverPath));
            gameOverPlayer.Volume = 0.6;
            gameOverPlayer.Play();
        }

        public void ShootingSound()
        {
            string ShootingPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sound/Shoting.wav");
            shootingPlayer.Open(new Uri(ShootingPath));
            shootingPlayer.Volume = 0.6;
            shootingPlayer.Play();
        }
    }
}
