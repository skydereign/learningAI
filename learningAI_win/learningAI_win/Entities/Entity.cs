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

        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Sprite != null)
            {
                spriteBatch.Draw(Sprite, Position, null, Color.White, 0, new Vector2(Sprite.Width / 2, Sprite.Width / 2), 1, SpriteEffects.None, 0);
            }
        }
    }
}
