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

        public Round(SoldierPhenotype phenotype, int roundSize, Callback completionCall)
        {
            this.roundSize = roundSize;
            CompletionCall = completionCall;

            // set up phenotypes
            phenotypes = new List<SoldierPhenotype>();
            phenotypes.Add(phenotype);

            // mutate the phenotype for extra matches
            for (int i = 1; i < roundSize; i++)
            {
                phenotypes.Add(phenotype.Mutate());
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
                Match match = new Match();
                Thread thread = new Thread(new ThreadStart(match.Run));
                matches.Add(match);
                threadMatches.Add(thread);
                thread.Start();
            }


            // join them
            for (int i = 0; i < roundSize; i++)
            {
                threadMatches[i].Join();
                // get the phenotype from the match
            }

            // crossbreed the top two

            CompletionCall(this); // end the Round
        }

        public SoldierPhenotype GetNewSoldier ()
        {
            // update
            return new SoldierPhenotype();
        }
    }
}
