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
        public static bool Melee = true;
        public static bool Visual = false;
        public static bool UseHeuristic = false;
        public static SoldierPhenotype Heuristic = new SoldierPhenotype(1.02f, 0f, 0f);
    }
}
