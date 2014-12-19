using Microsoft.Xna.Framework;
using SquadAI.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace learningAI_win.Entities
{
    class Bullet : Entity
    {
        private Entity source;
        public Vector2 Velocity;
        private int ticks = 0;

        public Bullet (Entity source, Vector2 position, Vector2 velocity)
        {
            this.source = source;

            Sprite = ContentLoader.Textures["bullet_enemy"];
            Position = position;
            Velocity = velocity;
            Rotation = (float)(Math.Atan2(-velocity.Y, velocity.X));
        }

        public override void Update(GameTime gameTime)
        {
            Position += Velocity;
            ticks++;

            if(ticks > 60)
            {
                Destroyed = true;
            }
        }

        public override void Collision(Entity other)
        {
            if (other != source && !(other is Bullet) && !(other is Slash))
            {
                if (other is Soldier)
                {
                    Soldier soldier = (Soldier)other;
                    soldier.RecentDamage += 5;
                    soldier.HP -= 5;

                    if (soldier.HP <= 0)
                    {
                        soldier.Destroyed = true;
                    }
                    Destroyed = true;
                }


                if (other is Enemy)
                {
                    Enemy enemy = (Enemy)other;
                    enemy.HP -= 5;
                    enemy.Notify();

                    if (enemy.HP <= 0)
                    {
                        enemy.Destroyed = true;
                    }
                    Destroyed = true;
                }
            }
        }
    }
}
