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
    class Player
    {
        public int health, smallBulletSize, bigBulletSize;
        public List<Bullet> bulletList;
        public Vector2 position, origin;
        public Rectangle hitBox;
        public Texture2D playerTexture;
        public Texture2D smallBulletTexture, bigBulletTexture;
        public float rotationAngle, rotationAdjust;
        

        public Player(Vector2 Position, Texture2D Texture)
        {
            position = Position;
            playerTexture = Texture;

        }

        public void Initialize()
        {
            health = 100;
            smallBulletSize = 5;
            bigBulletSize = 20;
            origin = new Vector2 (1728 / 2, 972 / 2);
            hitBox = new Rectangle();
            smallBulletTexture = null;
            bigBulletTexture = null;
            rotationAngle = 0;
        }

        public void LoadContent(ContentManager Content)
        {
            playerTexture = Content.Load<Texture2D>("Player_2_Practice");

        }

        public void UnloadContent()
        {
          
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            // Rotation stuff
            float movement = 0f;

            if (keyState.IsKeyDown(Keys.NumPad1))
                movement += 0.1f;
            
            if (keyState.IsKeyDown(Keys.NumPad2))
                movement -= 0.1f;
           
            rotationAngle += movement;
            float circle = MathHelper.Pi * 2;
            rotationAngle = rotationAngle % circle;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTexture, position, null, Color.White, rotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
        }
 
    }
}
