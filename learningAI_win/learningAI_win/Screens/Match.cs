using learningAI_win.AI;
using learningAI_win.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace learningAI_win.Screens
{
    public class Match : Screen
    {
        public List<Entity> Entities; // list of current Entities
        private List<Entity> newEntities; // list of entities to be created this frame
        private SoldierPhenotype phenotype;
        private bool running;

        // the two fighting entities
        private Soldier soldier;
        private Enemy enemy;

        private int gameTicks = 0;

        public Match(SoldierPhenotype phenotype)
        {
            Entities = new List<Entity>();
            newEntities = new List<Entity>();

            this.phenotype = phenotype;

            soldier = new Soldier(this, phenotype, new Vector2(400, 200));
            Entities.Add(soldier);

            enemy = new Enemy(this, soldier, new Vector2(200, 100));
            Entities.Add(enemy);

            soldier.Target = enemy;

            random = new Random(1);
        }
        public override void Update(GameTime gameTime)
        {
            gameTicks++;
            foreach (Entity e in Entities)
            {
                e.Update(gameTime);
            }

            handleCollisions();

            // transfer new entities to full entity list
            Entities.AddRange(newEntities);
            newEntities.Clear();

            // delete all flagged Entities
            for(int i=0; i<Entities.Count; i++)
            {
                Entity e = Entities[i];
                if(e.Destroyed)
                {
                    Entities.Remove(e);
                    i--;
                }
            }

            if(detectEnd())
            {
                running = false;
                Game1.screens.Remove(this);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach(Entity e in Entities)
            {
                e.Draw(spriteBatch);
            }
        }

        public void Run()
        {
            // run update until the match is completed
            running = true;

            while(running)
            {
                Update(null);
            }
        }

        public override void AddEntity(Entity entity)
        {
            newEntities.Add(entity);
        }

        public float Fitness ()
        {
            return soldier.Phenotype.Fitness;
        }

        public float EvaluateFitness ()
        {
            float fitness = 0;
            // ticks to win, 0.1
            if(enemy.HP <= 0) // soldier won
            {
                // use shorter as better
                float val = Math.Max(Math.Min((3000f - gameTicks) / 3000f, 1), 0);
                fitness +=  val * 0.1f;
            }
            else
            {
                // enemey won, use longer as better
                float val = Math.Max(Math.Min((gameTicks) / 3000f, 1), 0);
                fitness += val * 0.1f;
            }

            if (soldier.Melee)
            {
                fitness += soldier.Phenotype.Traits[SoldierPhenotype.trait.SPEED] / 40f;
            }
            // percent health, 0.2
            fitness += soldier.HP / 100.0f * 0.2f;

            // win, 0.45
            if(enemy.HP <= 0)
            {
                //Console.WriteLine("victory");
                fitness += 0.45f;
            }

            // enemy health, 0.25
            fitness -= ((enemy.HP - 100) / 100f)*0.25f;


            // if ran out of time, reduce fitness
            if(gameTicks > 3000)
            {
                fitness -= 0.2f;
            }
            soldier.Phenotype.Fitness = fitness;
            //Console.WriteLine("[" + soldier.Phenotype.ToString() + "] fitness = " + fitness);
            return fitness;
        }

        public SoldierPhenotype GetPhenotype ()
        {
            return phenotype;
        }

        private void handleCollisions()
        {
            // might want to update to not have a and b collide with each other twice
            foreach(Entity a in Entities)
            {
                foreach(Entity b in Entities)
                {
                    if (a != b)
                    {
                        a.CheckCollision(b);
                    }
                }
            }
        }

        private bool detectEnd()
        {
            if ((soldier.Destroyed || enemy.Destroyed))
            {
                //Console.WriteLine(soldier.Destroyed ? "Loss" : "Victory");
            }
            return (soldier.Destroyed || enemy.Destroyed);
        }
    }
}
