using learningAI_win.AI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace learningAI_win.Screens
{
    class Manager : Screen
    {
        // runs n number of rounds
        // each time passing in a phenotype (from the last round)
        // adds fitness and seed to the graph data
        // renders the graph
        private int roundCount;
        private int roundSize;
        private int curRound;
        private SoldierPhenotype soldierPhenotype;

        public Manager(int roundCount, int roundSize)
        {
            this.roundCount = roundCount;
            this.roundSize = roundSize;
            curRound = 0;
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
        /// Run the Manager without graphics
        /// </summary>
        public void Run ()
        {
            Console.WriteLine("Manager::Run");
            soldierPhenotype = new SoldierPhenotype();
            random = new Random(1);

            while(curRound < roundCount)
            {
                Round round = new Round(soldierPhenotype, roundSize, EndRound, random.Next());
                round.Run();
                curRound++;
            }
        }

        /// <summary>
        /// Extracts and stores the winning phenotype from the completed round
        /// </summary>
        /// <param name="screen"></param>
        public void EndRound (Screen screen)
        {
            Round lastRound = (Round)screen;
            soldierPhenotype = lastRound.GetNewSoldier();
        }
    }
}
