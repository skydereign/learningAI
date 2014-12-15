using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SquadAI.Source
{
    static class ContentLoader
    {
        public static Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();
        public static SpriteFont FontSmall;

        public static void LoadContent(ContentManager content)
        {
            Textures.Add("circle", content.Load<Texture2D>("Textures/circle"));
            FontSmall = content.Load<SpriteFont>("Fonts/smallFont");
        }
    }
}
