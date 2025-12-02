using System;

namespace River_Raid_WPF
{
    public class ShayanDialog
    {
        public Dictionary<int, string> Dialog { get; private set; }
        public MainWindow mainWindow;

        public bool LowFuelDialogShowed = false;

        public ShayanDialog(MainWindow mainwindow)
        {
            this.mainWindow = mainwindow;

            Dialog = new Dictionary<int, string>()
            {
                // شروع بازی
                {0, "Go! Go! Go!"},
                {1, "rule the sky!"},
                {2, "Mission starts now!"},
                {3, "take off!"},
                {4, "Ready… Aim… and fly!"},
                {5, "Showtime!"},
                {6, "engage the enemy!"},
                {7, "Stay sharp!"},
                {8, "Let’s make history!"},
                {9, "here we go!"},
                {10, "give it everything!"},
                {11, "Let’s light it up!"},

                // باخت به دلیل برخورد
                {12, "next time, pilot!."},
                {13, "crashed into the river!"},
                {14, "We’re going down!"},
                {15, "That was a hard crash!"},
                {16, "you’re out!"},
                {17, "Critical damage!"},
                {18, "Your bird’s down!"},
                {19, "not well enough!"},
                {20, "going down in flames!"},
                {21, "you’re gone!"},

                // سوخت کم
                {22, "Fuel levels critical!"},
                {23, "Find a fuel tank!"},
                {24, "Warning: low fuel!"},
                {25, "Refuel Fast!"},
                {26, "Fuel Warning hurry up!"},
                {27, "low fuel!"},
                {28, "Out of juice!"},
                {29, "Fuel is needed!"},

                // سوخت گیری
                {30, "Fuel up!"},
                {31, "Nice refuel, pilot!"},
                {32, "Good job!"},
                {33, "Nice Work!"},
                {34, "Fuel restored!"},
                {35, "Smooth refuel!"},
                {36, "Fuel check complete!"},
                {37, "Nice work, pilot!"},

                // انفجار دشمن
                {38, "Target destroyed!"},
                {39, "Direct hit!"},
                {40, "Enemy down!"},
                {41, "Nice shot!"},
                {42, "Boom!"},
                {43, "nice work!"},
                {44, "clean kill!"},
                {45, "Beautiful hit!"},
                {46, "Enemy destroyed!"},
                {47, "Bullseye!"},
                {48, "Great shot, pilot!"},
                {49, "Nice and clean!"},

                // زدن دروازه
                {50, "Gate destroyed!"},
                {51, "Through the gate!"},
                {52, "Nailed that gate!"},
                {53, "Another gate down!"},
                {54, "Gate cleared!"},

                // باخت کامل
                {55, "Ha..Ha..Ha.."},
                {56, "Happy Ending!"},
                {57, "You're Just A LOSER!"},
                {58, "Did you even try?"},
                {59, "entertaining!"},
                {60, "my grandma plays better!"},
                {61, "failed miserably!"},
                {62, "bring some skill!"},
                {63, "Happy Losing!"},

                // باخت به دلیل کمبود سوخت
                {64, "Out of fuel!"},
                {65, "No fuel left!"},
                {66, "out of juice!"},
                {67, "Fuel tank empty!"},
                {68, "Fuel’s gone!"},
                {69, "No fuel, no flight!"},
                {70, "Fuel depleted!"},
                {71, "Fuel failure!"},

                // زدن سوخت ها
                {72, "that’s extra points!"},
                {73, "Fuel destroyed!"},
                {74, "score increased!"},
                {75, "Fuels,they pay well!"},
                {76, "my favorite combo!"},
                {77, "Boom for points!"}
            };
        }

        
    }
}
