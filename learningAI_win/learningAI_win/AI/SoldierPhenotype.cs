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

        public SoldierPhenotype()
        {
            //
        }

        public SoldierPhenotype(float speed, float accuracy, float damage_threshold)
        {
            Traits[trait.SPEED] = speed;
            Traits[trait.ACCURACY] = accuracy;
            Traits[trait.DAMAGE_THRESHOLD] = damage_threshold;
        }

        public void Clear()
        {
            Traits[trait.SPEED] = 0;
            Traits[trait.ACCURACY] = 0;
            Traits[trait.DAMAGE_THRESHOLD] = 0;
        }

        public Dictionary<trait, float> Traits = new Dictionary<trait, float>()
        {
            {trait.SPEED, 1f}, {trait.ACCURACY, 30f}, {trait.DAMAGE_THRESHOLD, 10f}, {trait.RANGED_THRESHOLD, 3f}, {trait.MELEE_THRESHOLD, 4f}
        };

        // dictionary storing the changes during mutation
        public Dictionary<trait, float> Changes = new Dictionary<trait, float>()
        {
            {trait.SPEED, 0}, {trait.ACCURACY, 0}, {trait.DAMAGE_THRESHOLD, 0}, {trait.RANGED_THRESHOLD, 0}, {trait.MELEE_THRESHOLD, 0}
        };

        public SoldierPhenotype Mutate(Random randomGen)
        {
            // TODO: store changes caused by mutation, after match, extract positive 
            SoldierPhenotype mutated = new SoldierPhenotype();

            if (!Global.UseHeuristic)
            {
                // copy over all of the various trait values
                foreach (KeyValuePair<trait, float> pair in Traits)
                {
                    // pair.Value, pair.Key
                    if (randomGen.Next() > 0.25f)
                    {
                        float delta = 0f;

                        switch (pair.Key)
                        {
                            case trait.SPEED:
                                delta = (float)(-0.2 + 0.4 * randomGen.NextDouble());
                                mutated.Traits[pair.Key] = pair.Value + delta;
                                mutated.Changes[pair.Key] = delta;
                                break;

                            case trait.DAMAGE_THRESHOLD:
                                delta = (float)(-3f + 6f * randomGen.NextDouble());
                                mutated.Traits[pair.Key] = pair.Value + delta;
                                break;
                        }
                    }
                }
            }
            else // use heuristic
            {
                // up values according to the heuristic
                foreach (KeyValuePair<trait, float> pair in Traits)
                {
                    mutated.Traits[pair.Key] = pair.Value + Global.Heuristic.Traits[pair.Key];
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
