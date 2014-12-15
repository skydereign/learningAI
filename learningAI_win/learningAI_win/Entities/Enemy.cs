using learningAI_win.Screens;
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
        private const int chaseTime = 60;
        private const int bulletSpeed = 10;
        private const int range = 200;
        private const int visionRange = 500;
        private const float visionSpan = (float)(Math.PI/2.0);

        private int sightTimer;
        private int attackTimer;

        private float speed = 1f;
        private float angSpeed = 0.1f;
        private Soldier target;

        public Enemy(Screen parentScreen, Soldier target, Vector2 position)
        {
            this.parentScreen = parentScreen;
            this.target = target;
            Position = position;
            Sprite = ContentLoader.Textures["circle"];
        }

        public override void Update(GameTime gameTime)
        {
            // fire at player if in range (within distance, within angle)
            // move after player
            // if not in los for a certain amount of time, stop chasing
            // wander
            if (inSight())
            {
                sightTimer = chaseTime;
                rotateTowards();

                if(inRange())
                {
                    attack();
                    // attack, and move if not close enough
                }
                else
                {
                    // move towards player
                    moveTowards();
                }
            }
            else if(sightTimer > 0) // chase
            {
                sightTimer--;
            }
            else // wander
            {
                // pick a spot and move towards it for some time
                // turn to move to the next spot
            }
        }

        private bool inSight ()
        {
            float angTowardsTarget = (float)(Math.Atan2(Position.Y - target.Position.Y, target.Position.X - Position.X));
            float distance = (Position - target.Position).Length();
            if (visionRange > distance && Math.Abs(angTowardsTarget - Rotation) < visionSpan/2)
            {
                Console.WriteLine("inSight");
                return true;
            }
            return false;
        }

        private bool inRange()
        {
            return ((Position - target.Position).Length() < range);
        }

        private void rotateTowards ()
        {
            float ang = (float)(Math.Atan2(Position.Y - target.Position.Y, target.Position.X - Position.X));
            if (Rotation - ang > angSpeed)
            {
                Rotation -= angSpeed;
            }
            else if(Rotation - ang < -angSpeed)
            {
                Rotation += angSpeed;
            }
        }

        private void moveTowards ()
        {
            Position.X += (float)Math.Cos(Rotation) * speed;
            Position.Y -= (float)Math.Sin(Rotation) * speed;
        }

        private void attack ()
        {
            attackTimer = (attackTimer + 1) % 10;
            if (attackTimer == 0)
            {
                Vector2 velocity = new Vector2((float)Math.Cos(Rotation) * bulletSpeed,
                    (float)-Math.Sin(Rotation) * bulletSpeed);
                parentScreen.AddEntity(new Bullet(this, Position, velocity));
            }
        }
    }
}
