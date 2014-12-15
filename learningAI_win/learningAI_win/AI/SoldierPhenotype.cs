using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace learningAI_win.AI
{
    public class SoldierPhenotype
    {
        public enum trait
        {
            SPEED,
            ACCURACY,
            RUN_THRESHOLD,
            PICKUP_RANGED,
            PICKUP_MELEE
        }

        Dictionary<trait, float> Traits = new Dictionary<trait, float>()
        {
            {trait.SPEED, 1.0f}
        };

        public SoldierPhenotype Mutate()
        {
            SoldierPhenotype mutated = new SoldierPhenotype();

            return mutated;
        }

        public SoldierPhenotype Crossbreed(SoldierPhenotype other)
        {
            SoldierPhenotype cross = new SoldierPhenotype();
            // cross this with other
            return cross;
        }

        public string ToString ()
        {
            return "[]";
        }
    }
}
