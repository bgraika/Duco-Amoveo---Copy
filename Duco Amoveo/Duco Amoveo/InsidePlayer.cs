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
    class InsidePlayer
    {
        public int health, speed;
        public Vector2 position, previousPosition, origin;
        public Rectangle hitBox;
        public Texture2D playerTexture;
        public bool isVisible = true;


        public InsidePlayer()
        {
            position = new Vector2(1728 / 2 - 12, 972 / 2 - 12);
            health = 100;
            origin = new Vector2 (1728 / 2, 972 / 2);
            hitBox = new Rectangle();
            hitBox.Height = 25;
            hitBox.Width = 25;
            hitBox.X = (int)position.X;
            hitBox.Y = (int)position.Y;
            speed = 6;
            previousPosition = position;
        }

        public void LoadContent(ContentManager Content)
        {
            playerTexture = Content.Load<Texture2D>("Player_1");
        }

        public void UnloadContent()
        {

        }

        public void Update(GameTime gameTime)
        {
            
                KeyboardState keyState = Keyboard.GetState();

                //player movement
                if (keyState.IsKeyDown(Keys.NumPad8))
                    position.Y -= speed;

                if (keyState.IsKeyDown(Keys.NumPad4))
                    position.X -= speed;

                if (keyState.IsKeyDown(Keys.NumPad5))
                    position.Y += speed;

                if (keyState.IsKeyDown(Keys.NumPad6))
                    position.X += speed;

                // Distance check

                if (DistanceFromCenter(position) >= 370)
                {
                    position = previousPosition;
                }

                hitBox.X = (int)position.X;
                hitBox.Y = (int)position.Y;

                previousPosition = position;
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
           spriteBatch.Draw(playerTexture, position, Color.White);
        }

        public double DistanceFromCenter(Vector2 position)
        {
            double distance = 0.0;

            position.X += playerTexture.Width / 2;
            position.Y += playerTexture.Height / 2;

            distance = Math.Sqrt(Math.Pow(position.X - origin.X, 2) + Math.Pow(position.Y - origin.Y, 2));

            return distance;
        }

    }
}
