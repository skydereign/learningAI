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
            DAMAGE_THRESHOLD, // damage that can be taken before entering RUN state
            RANGED_THRESHOLD, // 
            MELEE_THRESHOLD
        }

        public Dictionary<trait, float> Traits = new Dictionary<trait, float>()
        {
            {trait.SPEED, 2f}, {trait.ACCURACY, 1.0f}, {trait.DAMAGE_THRESHOLD, 5f}, {trait.RANGED_THRESHOLD, 3f}, {trait.MELEE_THRESHOLD, 4f}
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

        public string Stringify ()
        {
            return "[]";
        }
    }
}
