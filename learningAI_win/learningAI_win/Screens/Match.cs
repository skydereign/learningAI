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

        public Match(SoldierPhenotype phenotype)
        {
            Entities = new List<Entity>();
            newEntities = new List<Entity>();

            this.phenotype = phenotype;

            soldier = new Soldier(this, phenotype, new Vector2(500, 300));
            Entities.Add(soldier);

            enemy = new Enemy(this, soldier, new Vector2(400, 200));
            Entities.Add(enemy);

            soldier.Target = enemy;

            random = new Random(1);
        }
        public override void Update(GameTime gameTime)
        {
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
                Console.WriteLine("detectEnd triggered");
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
                //
            }
        }

        public override void AddEntity(Entity entity)
        {
            newEntities.Add(entity);
        }

        public float EvaluateFitness ()
        {
            // ticks to win, 0.1
            // win, 0.45
            // percent health, 0.2
            // enemy health, 0.25
            return 0f;
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
            return (soldier.Destroyed || enemy.Destroyed);
        }
    }
}
