using learningAI_win.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace learningAI_win
{
    class Global
    {
        public static int NumRounds = 1000;
        public static int RoundSize = 5;
        public static bool Melee = false;
        public static bool Visual = true;
        public static bool UseHeuristic = false;
        public static SoldierPhenotype Heuristic = new SoldierPhenotype(1.02f, 0f, 0f);
        public static SoldierPhenotype StartingPhenotype = new SoldierPhenotype(0.13f, 3.9f, 22.6f);
    }
}
