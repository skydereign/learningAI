using learningAI_win.AI;
using learningAI_win.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SquadAI.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace learningAI_win.Entities
{
    public class Soldier : Entity
    {
        public SoldierPhenotype Phenotype;

        public Soldier(Screen parentScreen, SoldierPhenotype phenotype, Vector2 position)
        {
            this.parentScreen = parentScreen;
            Phenotype = phenotype;
            Position = position;
            Sprite = ContentLoader.Textures["circle"];
        }

        public override void Update(GameTime gameTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Position.Y -= 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Position.X -= 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Position.Y += 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Position.X += 1;
            }
        }
    }
}
