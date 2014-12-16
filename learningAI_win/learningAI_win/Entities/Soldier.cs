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
        public Enemy Target;

        public const int MELEE_DISTANCE = 40;

        private enum states { ATTACK, RUN, RETRIEVE }
        private states state;
        private bool melee; // holds wheter the soldier is using melee or not


        public float HP;
        public float RecentDamage; // holds how much damage since last state transition


        private float angSpeed = 0.1f;
        private int meleeTimer = 0;
        

        public Soldier(Screen parentScreen, SoldierPhenotype phenotype, Vector2 position)
        {
            this.parentScreen = parentScreen;
            Phenotype = phenotype;
            Position = position;
            Sprite = ContentLoader.Textures["circle"];

            HP = 100;
            RecentDamage = 0;
            melee = true;
            state = states.ATTACK;
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


            attack();
            if (melee)
            {
                switch (state)
                {
                    case states.ATTACK:
                        //attack();
                        break;

                    case states.RUN:
                        break;

                    case states.RETRIEVE:
                        break;
                }
            }
            // if melee
            // ATTACK - if no damage threshold
            //    if not seen
            //       move to behind enemy
            //       move to enemy
            //       attack
            //    else if threshold not met
            //       move to enemy
            //       attack
            // exit -> RUN - damage threshold met
            // exit -> RETRIEVE - item threshold met

            // RUN - if damage threshold met
            //    else if in range
            //       run out of range
            //    else
            //       move to hiding spot
            // exit -> ATTACK - no longer seen
            // exit -> RETRIEVE - item threshold met

            // RETRIEVE - if desirable item exists
            //    if object is close not in sight and desirable
            //      move to object
            // exit -> ATTACK - item obtained and not in sight
            // exit -> RUN - item obtained and in sight or damage threshold met
            
        }


        private void attack ()
        {
            // ATTACK - if no damage threshold
            //    if not seen
            //       move to behind enemy
            //       move to enemy
            //       attack
            //    else if threshold not met
            //       move to enemy
            //       attack
            // exit -> RUN - damage threshold met
            // exit -> RETRIEVE - item threshold met

            float distance = (Position - Target.Position).Length();
            if(Target.Alerted())
            {
                // rush attack
                if(distance < MELEE_DISTANCE)
                {
                    meleeAttack();
                }
                else
                {
                    moveTowardsTarget();
                }
            }
            else // not seen
            {
                float angTowardsTarget = (float)(Math.Atan2(Position.Y - Target.Position.Y, Target.Position.X - Position.X));


                if(distance < MELEE_DISTANCE)
                {
                    meleeAttack();
                }
                else if(Math.Abs(Utility.AngleBetween(angTowardsTarget, Target.Rotation)) < Math.PI/8) // move toward target
                {
                    // TODO: update to actually check the angle
                    moveTowardsTarget();
                }
                else
                {
                    moveAroundTarget();
                }
            }

            /*
            // exit condition to RUN
            if (recentDamage > Phenotype.Traits[SoldierPhenotype.trait.DAMAGE_THRESHOLD])
            {
                // transition to run
                recentDamage = 0;
                state = states.RUN;
                return;
            }

            // exit condition to RETRIEVE (melee)
            if (meleeItemPriority() > Phenotype.Traits[SoldierPhenotype.trait.MELEE_THRESHOLD])
            {
                //
            }

            // exit condition to RETRIEVE (ranged)
            if (rangedItemPriority() > Phenotype.Traits[SoldierPhenotype.trait.RANGED_THRESHOLD])
            {
                //
            }*/
        }

        private void run ()
        {
            //
            // RUN - if damage threshold met
            //    else if in range
            //       run out of range
            //    else
            //       move to hiding spot
            // exit -> ATTACK - no longer seen
            // exit -> RETRIEVE - item threshold met
        }

        private void retrieve ()
        {
            //
            // RETRIEVE - if desirable item exists
            //    if object is close not in sight and desirable
            //      move to object
            // exit -> ATTACK - item obtained and not in sight
            // exit -> RUN - item obtained and in sight or damage threshold met
        }

        private float meleeItemPriority ()
        {
            return 0f;
        }

        private float rangedItemPriority()
        {
            return 0f;
        }

        private void moveAroundTarget()
        {
            // get target angle
            // move to that point
            
            float angTowardsTarget = (float)(Math.Atan2(Position.Y - Target.Position.Y, Target.Position.X - Position.X));
            float targetAngle = (float)(Target.Rotation + Math.PI);

            // find the direction the soldier needs to turn
            int dir = Math.Sign(Utility.AngleBetween(angTowardsTarget, targetAngle));

            // increase the angle in the proper direction
            float moveToPositionAngle = (float)(angTowardsTarget + Math.PI + dir*angSpeed);

            // find the point
            Vector2 moveToPosition = new Vector2((float)(Target.Position.X + Math.Cos(moveToPositionAngle)*100), 
                (float)(Target.Position.Y - Math.Sin(moveToPositionAngle)*100));

            // find angle to that point
            float moveDirection = (float)Math.Atan2(Position.Y - moveToPosition.Y, moveToPosition.X - Position.X);

            // move to that point
            Position.X += (float)Math.Cos(moveDirection) * 2;
            Position.Y -= (float)Math.Sin(moveDirection) * 2;
        }

        private void moveTowardsTarget()
        {
            // rotate if not facing, then move
            float angTowardsTarget = (float)(Math.Atan2(Position.Y - Target.Position.Y, Target.Position.X - Position.X));

            if (Math.Abs(Utility.AngleBetween(Rotation, angTowardsTarget)) < Math.PI / 4)
            {
                Position.X += (float)Math.Cos(Rotation) * 2;
                Position.Y -= (float)Math.Sin(Rotation) * 2;
            }
                // rotate
                rotateTowards();
            
        }

        private void rotateTowards()
        {
            float ang = (float)(Math.Atan2(Position.Y - Target.Position.Y, Target.Position.X - Position.X));
            if (Rotation - ang > angSpeed)
            {
                Rotation -= angSpeed;
            }
            else if (Rotation - ang < -angSpeed)
            {
                Rotation += angSpeed;
            }
        }

        private void meleeAttack()
        {
            // create attack
            if (meleeTimer == 0)
            {
                parentScreen.AddEntity(new Slash(this, Rotation, Position));
            }

            meleeTimer = (meleeTimer + 1) % 30;
        }
    }
}
