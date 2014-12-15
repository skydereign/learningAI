using learningAI_win.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace learningAI_win.Screens
{
    public abstract class Screen
    {
        public delegate void Callback(Screen screen);
        public Callback CompletionCall;

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        public virtual void AddEntity(Entity entity) { }
    }
}
