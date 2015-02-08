using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Duco_Amoveo
{
    class Collision
    {
        SoundEffectInstance hurtInstance;

        public Collision()
        {

        }//eo collision

        public void check(List<Bullet> bulletList, InsidePlayer P1, List<Vector2> pointList, SoundManager SM)
        {

            foreach (Bullet b in bulletList)
            {
                if (b.hitBox.Intersects(P1.hitBox))
                {
                    //hurtInstance = SM.playerHurt.CreateInstance();
                    //hurtInstance.IsLooped = false;
                    //hurtInstance.Play();
                    if (b.visible)
                        SM.playHurt();

                    if (b.size == 1)
                    {
                        P1.health -= 10;
                    }
                    if (b.size == 0)
                    {
                        P1.health -= 25;
                    }
                    b.visible = false;
                }
                if (DistanceFromCenter(b) >= 445)
                {
                    b.visible = false;
                }
            } 
        }//eo check

        public double DistanceFromCenter( Bullet b)
        {
            double distance = 0.0;
            Vector2 stageOrigin = new Vector2(864, 486);
            distance = Math.Sqrt(Math.Pow(b.position.X - stageOrigin.X, 2) + Math.Pow(b.position.Y - stageOrigin.Y, 2));

            return distance;
        }
    }

}
