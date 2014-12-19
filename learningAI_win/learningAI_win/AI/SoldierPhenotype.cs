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

        public float Fitness = 0f;

        public Dictionary<trait, float> Traits = new Dictionary<trait, float>()
        {
            {trait.SPEED, 2f}, {trait.ACCURACY, 30f}, {trait.DAMAGE_THRESHOLD, 10f}, {trait.RANGED_THRESHOLD, 3f}, {trait.MELEE_THRESHOLD, 4f}
        };

        public SoldierPhenotype Mutate(Random randomGen)
        {
            // TODO: store changes caused by mutation, after match, extract positive 
            SoldierPhenotype mutated = new SoldierPhenotype();

            // copy over all of the various trait values
            foreach (KeyValuePair<trait, float> pair in Traits)
            {
                // pair.Value, pair.Key
                if (randomGen.Next() > 0.25f)
                {
                    switch(pair.Key)
                    {
                        case trait.SPEED:
                            mutated.Traits[pair.Key] = pair.Value + (float)(-0.2 + 0.4 * randomGen.NextDouble());
                            break;

                        case trait.DAMAGE_THRESHOLD: 
                            mutated.Traits[pair.Key] = pair.Value + (float)(-3f + 6f * randomGen.NextDouble());
                            
                            break;
                    }
                }
            }

            if(mutated.Traits[trait.SPEED] <= 0)
            {
                mutated.Traits[trait.SPEED] = 0.2f;
            }

            if (mutated.Traits[trait.DAMAGE_THRESHOLD] < 0)
            {
                mutated.Traits[trait.DAMAGE_THRESHOLD] = 0;
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

        public override string ToString ()
        {
            return "[S = " + Traits[trait.SPEED] + ", A = " + Traits[trait.ACCURACY] + ", D = " + Traits[trait.DAMAGE_THRESHOLD] + "]";
        }
    }
}
