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
            {trait.SPEED, 2f}, {trait.ACCURACY, 10f}, {trait.DAMAGE_THRESHOLD, 5f}, {trait.RANGED_THRESHOLD, 3f}, {trait.MELEE_THRESHOLD, 4f}
        };

        public SoldierPhenotype Mutate(Random randomGen)
        {
            // TODO: store changes caused by mutation, after match, extract positive 
            SoldierPhenotype mutated = new SoldierPhenotype();

            // copy over all of the various trait values
            foreach (KeyValuePair<trait, float> pair in Traits)
            {
                // pair.Value, pair.Key
                if (randomGen.Next() > 0.95f)
                {
                    mutated.Traits[pair.Key] = pair.Value + (float)(-0.4+0.8*randomGen.NextDouble());
                }
            }

            // return
            return mutated;
        }

        public SoldierPhenotype Crossbreed(Random randomGen, SoldierPhenotype other)
        {
            SoldierPhenotype child = new SoldierPhenotype();

            // has a random chance of which parent passes on the trait
            
            foreach(KeyValuePair<trait, float> pair in Traits)
            {
                child.Traits[pair.Key] = (randomGen.Next() < 0.5 ? pair.Value : other.Traits[pair.Key]);
            }
            return child;
        }

        public string Stringify ()
        {
            return "[]";
        }
    }
}
