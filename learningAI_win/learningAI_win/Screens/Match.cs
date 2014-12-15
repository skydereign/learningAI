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
        public List<Entity> Entities;

        public Match()
        {
            Entities = new List<Entity>();
            Entities.Add(new Enemy(new Vector2(100, 100)));
        }
        public override void Update(GameTime gameTime)
        {
            foreach (Entity e in Entities)
            {
                e.Update(gameTime);
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
            //
        }
    }
}
