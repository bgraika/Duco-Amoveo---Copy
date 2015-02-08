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
    class Bullet
    {
        public Rectangle hitBox;
        public int size, reflections, speed;
        public Texture2D bulletTexture;
        public Vector2 position, origin;
        public bool visible;
        public double angleX, angleY;

        public Bullet(Vector2 Position, int Size, ContentManager Content, double X, double Y, int Speed)
        {
            position = Position;
            visible = true;
            size = Size;
            origin = new Vector2(1728 / 2, 972 / 2);
            this.LoadContent(Content);
            angleX = X;
            angleY = Y;
            reflections = 0;
            hitBox = new Rectangle();
            speed = Speed;
            if (size == 1)
            {
                hitBox.Height = 5;
                hitBox.Width = 5;
            }
            else
            {
                hitBox.Height = 15;
                hitBox.Width = 15;
            }

            hitBox.X = (int)Position.X;
            hitBox.Y = (int)Position.Y;
        }

        public void Initialize()
        {
            
        }

        public void LoadContent(ContentManager Content)
        {
            if (size == 1)
            {
                bulletTexture = Content.Load<Texture2D>("Small_Bullet");
            }
            else
            {
                bulletTexture = Content.Load<Texture2D>("Big_Bullet");
            }
        }

        public void UnloadContent()
        {
          
        }

        public void Update(GameTime gameTime)
        {
            
            if(reflections >= 3 && size == 1)
            {
                visible = false;
            }
            if (reflections >= 5 && size == 0)
            {
                visible = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bulletTexture, position, Color.White);
        }

    }
}
