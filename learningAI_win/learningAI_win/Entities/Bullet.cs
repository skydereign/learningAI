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

        public Bullet (Entity source, Vector2 position, Vector2 velocity)
        {
            this.source = source;

            Sprite = ContentLoader.Textures["slash"];
            Position = position;
            Velocity = velocity;
            Rotation = (float)(Math.Atan2(-velocity.Y, velocity.X));
        }

        public override void Update(GameTime gameTime)
        {
            Position += Velocity;
        }

        public override void Collision(Entity other)
        {
            if (other != source && !(other is Bullet))
            {
                Destroyed = true;
                //other.Destroyed = true;
            }
        }
    }
}
