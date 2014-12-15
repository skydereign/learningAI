using Microsoft.Xna.Framework;
using SquadAI.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace learningAI_win.Entities
{
    public class Enemy : Entity
    {
        public Enemy(Vector2 position)
        {
            Position = position;
            Sprite = ContentLoader.Textures["circle"];
        }

        public override void Update(GameTime gameTime)
        {
            Console.WriteLine("30");
        }
    }
}
