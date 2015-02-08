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

    class Background
    {

        public Vector2 position, origin;
        public Texture2D texture;
        public float rotationAngle;
        

        public Background(Texture2D Texture, Vector2 Position, Vector2 Origin)
        {
            texture = Texture;
            position = Position;
            origin = Origin;
        }

        public void Initialize()
        {
            texture = null;
            rotationAngle = 0;
        }

        public void LoadContent(ContentManager Content)
        {
           

        }

        public void UnloadContent()
        {
          
        }

        public void Update(GameTime gameTime, float rotationAdjust)
        {
            // Rotation stuff
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds / 2;
            rotationAngle += elapsed;
            float circle = MathHelper.Pi * 2;
            rotationAngle = rotationAngle % circle - rotationAdjust;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, rotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
           //spriteBatch.Draw(midTexture, midSpawn, null, Color.White, -rotationAngle, midOrigin, 1.0f, SpriteEffects.None, 0f);
            //spriteBatch.Draw(frontTexture, frontSpawn, null, Color.White, rotationAngle, frontOrigin, 1.0f, SpriteEffects.None, 0f);
        }
 
    }
}
