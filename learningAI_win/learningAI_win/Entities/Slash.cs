using Microsoft.Xna.Framework;
using SquadAI.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace learningAI_win.Entities
{
    class Slash : Entity
    {
        private Entity source;

        public Slash(Entity source, float rotation, Vector2 position)
        {
            this.source = source;

            Sprite = ContentLoader.Textures["slash"];
            Position = position;
            Rotation = rotation;
        }

        public override void Collision(Entity other)
        {
            if (other != source && !(other is Bullet) && !(other is Slash))
            {
                if (other is Soldier)
                {
                    Soldier soldier = (Soldier)other;
                    soldier.RecentDamage += 10;
                    soldier.HP -= 10;

                    if(soldier.HP <= 0)
                    {
                        soldier.Destroyed = true;
                    }
                    Destroyed = true;
                }


                if (other is Enemy)
                {
                    Enemy enemy = (Enemy)other;
                    enemy.HP -= 10;
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
