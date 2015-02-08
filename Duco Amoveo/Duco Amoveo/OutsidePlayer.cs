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
    class OutsidePlayer
    {
        public int speed, bulletSpeed, bulletDelay, bulletBuffer, i;
        public Vector2 position, previousPosition, origin, stageOrigin;
        public Texture2D playerTexture;
        public List<Bullet> bulletList;
        public string lastShift = "circle";
        public float tempPosX;
        double angleX = 0.0, angleY = 0.0;

        public Stage hexStage = new Stage(6);
        public Stage heptStage = new Stage(7);
        public Stage octStage = new Stage(8);
        public Stage circleStage = new Stage(50);
        public List<Vector2> hexPoints = new List<Vector2>();
        public List<Vector2> heptPoints = new List<Vector2>();
        public List<Vector2> octPoints = new List<Vector2>();
        public List<Vector2> circlePoints = new List<Vector2>();
     

        SoundManager SM;

        public OutsidePlayer()
        {
            position = new Vector2(1728 / 2 + 465, (972 / 2) - 12);
            origin = new Vector2(1728 / 2, (972 / 2));
            stageOrigin = new Vector2(1728 / 2, 972 / 2);
            previousPosition = position;
            speed = 6;
            bulletSpeed = 6;
            bulletDelay = 5;
            bulletList = new List<Bullet>();
            circleStage.isVisible = true;
            bulletBuffer = 15;
            SM = new SoundManager();
            i = 0;
            tempPosX = 0;
        }

        public void LoadContent(ContentManager Content)
        {
            playerTexture = Content.Load<Texture2D>("Player_1");
            SM.LoadContent(Content);
        }

        public void UnloadContent()
        {

        }

        public void Update(GameTime gameTime, ContentManager Content)
        {
            KeyboardState keyState = Keyboard.GetState();

            //player movement
            if (keyState.IsKeyDown(Keys.A))
            {
                position.X += incrementAngleX(i, 1);
                position.Y += incrementAngleY(i, 1);
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                position.X += incrementAngleX(i, 0);
                position.Y += incrementAngleY(i, 0);
            }
            //end player movement
            if (tempPosX != position.X)
            {
                tempPosX = position.X;
                i++;
            }


            // Shape shifting

            #region ShapeShifting

            if (keyState.IsKeyDown(Keys.Up))
            {
                circleStage.isVisible = true;
                hexStage.isVisible = false;
                heptStage.isVisible = false;
                octStage.isVisible = false;
            }

            if (keyState.IsKeyDown(Keys.Left))
            {
                circleStage.isVisible = false;
                hexStage.isVisible = true;
                heptStage.isVisible = false;
                octStage.isVisible = false;
            }
                

            if (keyState.IsKeyDown(Keys.Down))
            {
                circleStage.isVisible = false;
                hexStage.isVisible = false;
                heptStage.isVisible = true;
                octStage.isVisible = false;
            }
                

            if (keyState.IsKeyDown(Keys.Right))
            {
                circleStage.isVisible = false;
                hexStage.isVisible = false;
                heptStage.isVisible = false;
                octStage.isVisible = true;
            }

            #endregion

            

            //shooting
            if (keyState.IsKeyDown(Keys.S))
                ShootBig(Content);
            else if (keyState.IsKeyDown(Keys.W))
                ShootSmall(Content);

            UpdateBullets(gameTime);


            previousPosition = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // spriteBatch.Draw(playerTexture, position, null, Color.White, rotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
            #region Bullet Draw
            foreach (Bullet b in bulletList)
            {
                b.Draw(spriteBatch);
            }
            spriteBatch.Draw(playerTexture, position, Color.White);
            #endregion

            #region Stage Draw
            if (circleStage.isVisible)
            {
                if (lastShift != "circle")
                    SM.playShapeShift();
                Primitives2D.DrawCircle(spriteBatch, stageOrigin, 450, 500, Color.White, 16);
                Primitives2D.DrawCircle(spriteBatch, stageOrigin, 450, 500, Color.Black, 8);
                lastShift = "circle";
            }
            else if (hexStage.isVisible)
            {
                if (lastShift != "hex")
                    SM.playShapeShift();
                Primitives2D.DrawCircle(spriteBatch, stageOrigin, 450, 6, Color.White, 16);
                Primitives2D.DrawCircle(spriteBatch, stageOrigin, 450, 6, Color.Black, 8);
                lastShift = "hex";
            }
            else if (heptStage.isVisible)
            {
                if (lastShift != "hept")
                    SM.playShapeShift();
                Primitives2D.DrawCircle(spriteBatch, stageOrigin, 450, 7, Color.White, 16);
                Primitives2D.DrawCircle(spriteBatch, stageOrigin, 450, 7, Color.Black, 8);
                lastShift = "hept";
            }
            else if (octStage.isVisible)
            {
                if (lastShift != "oct")
                    SM.playShapeShift();
                Primitives2D.DrawCircle(spriteBatch, stageOrigin, 450, 8, Color.White, 16);
                Primitives2D.DrawCircle(spriteBatch, stageOrigin, 450, 8, Color.Black, 8);
                lastShift = "oct";
            }
            #endregion
        }

        #region Shots

        public void ShootBig(ContentManager Content)
        {
            if (bulletBuffer > 0)
                bulletBuffer--;
            else
            {
                Vector2 newPosition;
                Bullet newBullet;
                if (hexStage.isVisible)
                {
                    newPosition = new Vector2((float)(position.X - 105 * Math.Cos(angleX)), (float) (position.Y - 105 * Math.Sin(angleY)));
                    newBullet = new Bullet(newPosition, 0, Content, angleX, angleY, 10);  // 1 for big bullet, 0 for small bullet
                }
                else if (heptStage.isVisible)
                {
                    newPosition = new Vector2((float)(position.X - 85 * Math.Cos(angleX)), (float)(position.Y - 85 * Math.Sin(angleY)));
                    newBullet = new Bullet(newPosition, 0, Content, angleX, angleY, 6);
                }
                else if (octStage.isVisible)
                {
                    newPosition = new Vector2((float)(position.X - 70 * Math.Cos(angleX)), (float)(position.Y - 70 * Math.Sin(angleY)));
                    newBullet = new Bullet(newPosition, 0, Content, angleX, angleY, 1);
                }
                else
                {
                    newPosition = new Vector2((float)(position.X - 45 * Math.Cos(angleX)), (float)(position.Y - 45 * Math.Sin(angleY)));
                    newBullet = new Bullet(newPosition, 0, Content, angleX, angleY, 5);
                }

                bulletList.Add(newBullet);
                bulletBuffer = 15;
                SM.playBigShot();
            }

        }

        public void ShootSmall(ContentManager Content)
        {
            if (bulletBuffer > 0)
                bulletBuffer--;
            else
            {
                Vector2 newPosition;
                Bullet newBullet;
                if (hexStage.isVisible)
                {
                    newPosition = new Vector2((float)(position.X - 105 * Math.Cos(angleX)), (float)(position.Y - 105 * Math.Sin(angleY)));
                    newBullet = new Bullet(newPosition, 1, Content, angleX, angleY, 1);
                }
                else if (heptStage.isVisible)
                {
                    newPosition = new Vector2((float)(position.X - 85 * Math.Cos(angleX)), (float)(position.Y - 85 * Math.Sin(angleY)));
                    newBullet = new Bullet(newPosition, 1, Content, angleX, angleY, 6);
                }
                else if (octStage.isVisible)
                {
                    newPosition = new Vector2((float)(position.X - 70 * Math.Cos(angleX)), (float)(position.Y - 70 * Math.Sin(angleY)));
                    newBullet = new Bullet(newPosition, 1, Content, angleX, angleY, 15);
                }
                else
                {
                    newPosition = new Vector2((float)(position.X - 45 * Math.Cos(angleX)), (float)(position.Y - 45 * Math.Sin(angleY)));
                    newBullet = new Bullet(newPosition, 1, Content, angleX, angleY, 10);
                }

                
                bulletList.Add(newBullet);
                bulletBuffer = 15;
                SM.playSmallShot();
            }
        }
        #endregion

        public void UpdateBullets(GameTime gameTime)
        {
            foreach (Bullet b in bulletList)
            {
                b.Update(gameTime);


                b.position.X += -(float)Math.Cos(b.angleX) * b.speed;
                b.position.Y += -(float)Math.Sin(b.angleY) * b.speed;
                b.hitBox.X = (int)b.position.X;
                b.hitBox.Y = (int)b.position.Y;


                if (b.position.Y <= 0)
                {
                    b.visible = false;
                }
            }

            for(int i = 0; i < bulletList.Count; i++)
            {
                if (!bulletList[i].visible)
                {
                    bulletList.RemoveAt(i);
                    i--;
                }
            }
        }

        public float AngleToCenter(Vector2 position)
        {
            float angle = 0;


            return angle;
        }

        public double DistanceFromCenter(Vector2 position)
        {
            double distance = 0.0;

            position.X += playerTexture.Width / 2;
            position.Y += playerTexture.Height / 2;

            distance = Math.Sqrt(Math.Pow(position.X - stageOrigin.X, 2) + Math.Pow(position.Y - stageOrigin.Y, 2));

            return distance;
        }

        #region Increments

        public float incrementAngleX(int i, int DA)
        {
            double xval = 0;
            if (DA == 0)
            {
                angleX = (angleX + (0.065));// % (MathHelper.Pi * 2);
                xval = (float)475 * Math.Cos(angleX) + 852;
            }
            else
            {
                angleX = (angleX - (0.065));// % (MathHelper.Pi * 2);
                xval = (float)475 * Math.Cos(angleX) + 852;
            }
            return ((float)xval - position.X);
        }
        public float incrementAngleY(int i, int DA)
        {
            double yval = 0;
            if (DA == 0)
            {
                angleY = (angleY + (0.065));// % (MathHelper.Pi * 2);
                yval = (float)475 * Math.Sin(angleY) + 474;
            }
            else
            {
                angleY = (angleY - (0.065));// % (MathHelper.Pi * 2);
                yval = (float)475 * Math.Sin(angleY) + 474;
            }
            return ((float)yval - position.Y);
        }

        #endregion
    }
}
