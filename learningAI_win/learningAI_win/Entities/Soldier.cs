using learningAI_win.AI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public Soldier(Vector2 position)
        {
            Position = position;
            Sprite = ContentLoader.Textures["circle"];
        }
    }
}
