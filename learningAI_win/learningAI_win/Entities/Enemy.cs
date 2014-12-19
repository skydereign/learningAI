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
        public int VisionRange = 200;

        private const int chaseTime = 60;
        private const int bulletSpeed = 10;
        private const int range = 200;
        private const int moveRange = range - 30;
        private const float visionSpan = (float)(Math.PI/3.0);
        private enum wanderState { IDLE, WANDER, ROTATE_R, ROTATE_L, SIZE}
        private wanderState state;

        private int sightTimer;
        private int attackTimer;
        private int wanderTimer;

        public float HP;
        private float speed = 0.5f;
        private float angSpeed = 0.05f;
        private Soldier target;

        public Enemy(Screen parentScreen, Soldier target, Vector2 position)
        {
            this.parentScreen = parentScreen;
            this.target = target;
            Position = position;
            Sprite = ContentLoader.Textures["circle"];

            HP = 100;
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

                    // move if not close enough to the target
                    if ((Position - target.Position).Length() > moveRange)
                    {
                        moveTowards();
                    }
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
                rotateTowards();
                if ((Position - target.Position).Length() > moveRange)
                {
                    moveTowards();
                }
            }
            else // wander
            {
                wanderTimer = (wanderTimer + 1) % 60;

                if(wanderTimer == 0)
                {
                    // pick a random wander state
                    state = (wanderState)(parentScreen.random.Next(0, (int)wanderState.SIZE));
                }

                parentScreen.random.Next();
                switch(state)
                {
                    case wanderState.IDLE:
                        // don't do anything
                        break;

                    case wanderState.ROTATE_R:
                        Rotation += angSpeed / 10f;
                        break;

                    case wanderState.ROTATE_L:
                        Rotation -= angSpeed / 10f;
                        break;

                    case wanderState.WANDER:
                        Position.X += (float)Math.Cos(Rotation) * speed/2;
                        Position.Y -= (float)Math.Sin(Rotation) * speed/2;
                        break;
                }
            }
        }

        public bool Alerted ()
        {
            return sightTimer > 0;
        }

        public void Notify ()
        {
            sightTimer = 60;
        }





        private bool inSight ()
        {
            // TODO: implement cover
            float angTowardsTarget = (float)(Math.Atan2(Position.Y - target.Position.Y, target.Position.X - Position.X));
            float distance = (Position - target.Position).Length();
            if (VisionRange > distance && Math.Abs(angTowardsTarget - Rotation) < visionSpan/2)
            {
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
            attackTimer = (attackTimer + 1) % 20;
            if (attackTimer == 0)
            {
                // TODO: aim to where the target is moving
                Vector2 velocity = new Vector2(
                       (float)Math.Cos(Rotation) * bulletSpeed,
                       (float)-Math.Sin(Rotation) * bulletSpeed);

                parentScreen.AddEntity(new Bullet(this, Position, velocity));
            }
        }
    }
}
