using learningAI_win.AI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace learningAI_win.Screens
{
    class Round : Screen
    {
        // takes a phenotype, mutates it
        // runs n number of matches
        // crossbreeds the best two
        // returns it
        private List<SoldierPhenotype> phenotypes;
        private int roundSize;
        private SoldierPhenotype finalPhenotype;
        public SoldierPhenotype Heuristic;

        public Round(SoldierPhenotype phenotype, SoldierPhenotype heuristic, int roundSize, Callback completionCall, int randomIndex)
        {
            Console.WriteLine("\nNEW ROUND");
            this.roundSize = roundSize;
            CompletionCall = completionCall;

            random = new Random(randomIndex);

            // store the heuristic for later use
            Heuristic = heuristic;

            // set up phenotypes
            phenotypes = new List<SoldierPhenotype>();
            phenotypes.Add(phenotype);

            // mutate the phenotype for extra matches
            for (int i = 1; i < roundSize; i++)
            {
                phenotypes.Add(phenotype.Mutate(random));
                //Console.WriteLine("phenotype = " + phenotypes[i].ToString());
            }
        }

        public override void Update(GameTime gameTime)
        {
            //
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //
        }

        /// <summary>
        /// Runs the round spawning all matches.
        /// Crossbreeds the top two phenotypes
        /// </summary>
        public void Run()
        {
            List<Thread> threadMatches = new List<Thread>();
            List<Match> matches = new List<Match>();

            for (int i = 0; i < roundSize; i++)
            {
                Match match = new Match(phenotypes[i]);
                Thread thread = new Thread(new ThreadStart(match.Run));
                matches.Add(match);
                threadMatches.Add(thread);
                thread.Start();
            }


            // join them
            for (int i = 0; i < roundSize; i++)
            {
                threadMatches[i].Join();
                matches[i].EvaluateFitness();
            }

            // store the original fitness before sorting, [0] is the unmutated soldier
            float originalFitness = matches[0].GetPhenotype().Fitness;

            // sort matches in descending order by fitness
            matches = matches.OrderByDescending(match => match.Fitness()).ToList();


            // crossbreed the top two
            finalPhenotype = matches[0].GetPhenotype();
            finalPhenotype = finalPhenotype.Crossbreed(random, matches[1].GetPhenotype());
            Console.WriteLine("fitness 1 = " + matches[0].GetPhenotype().Fitness + ", fitness 2 = " + matches[1].GetPhenotype().Fitness);
            Console.WriteLine("Final Phenotype = " + finalPhenotype.ToString());



            // make sure there is a fitness before calculating improvements
            if (originalFitness != 0)
            {
                // add to heuristic by comparing with previous
                foreach (SoldierPhenotype.trait trait in (SoldierPhenotype.trait[])Enum.GetValues(typeof(SoldierPhenotype.trait)))
                {
                    float deltaFitness1 = matches[0].Fitness() - originalFitness;
                    float deltaFitness2 = matches[1].Fitness() - originalFitness;

                    if (deltaFitness1 > 0f)
                    {
                        //Console.WriteLine("increase in fitness = " + deltaFitness1 * matches[0].GetPhenotype().Changes[trait]);
                        Heuristic.Traits[trait] += deltaFitness1 * matches[0].GetPhenotype().Changes[trait];
                    }
                    if (deltaFitness2 > 0f)
                    {
                        Heuristic.Traits[trait] += deltaFitness2 * matches[1].GetPhenotype().Changes[trait];
                    }
                }
            }


            CompletionCall(this); // end the Round
        }

        public SoldierPhenotype GetNewSoldier ()
        {
            return finalPhenotype;
        }
    }
}
