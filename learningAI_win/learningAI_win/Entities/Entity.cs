using learningAI_win.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace learningAI_win.Entities
{
    public class Entity
    {
        public Vector2 Position;
        public Texture2D Sprite;
        public float Rotation = 0f;
        public bool Destroyed;
        public Screen parentScreen;

        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Sprite != null)
            {
                spriteBatch.Draw(Sprite, Position, null, Color.White, (float)(2.0 * Math.PI - Rotation), new Vector2(Sprite.Width / 2, Sprite.Width / 2), 1, SpriteEffects.None, 0);
            }
        }

        /// <summary>
        /// Handles collision checking, triggers the collisions, cannot override
        /// </summary>
        /// <param name="other"></param>
        public void CheckCollision(Entity other)
        {
            if ((Position - other.Position).Length() < Sprite.Width/2 + other.Sprite.Width/2)
            {
                Collision(other);
            }
        }

        public virtual void Collision(Entity other) { }
    }
}
