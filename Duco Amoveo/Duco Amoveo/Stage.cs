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
    class Stage
    {
        public Vector2 origin;
        public float radius;
        public int sides;
        public float thickness;
        public bool isVisible;
        public List<Vector2> pointList = new List<Vector2>();

        public Stage(int Sides)
        {
            sides = Sides;

        }

        public void Initialize()
        {
            origin.X = 1728 / 2;
            origin.Y = 972 / 2;
            radius = 450;
            thickness = 5;
            isVisible = false;
        }

        public void LoadContent(ContentManager Content)
        {

        }

        public void UnloadContent()
        {
          
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            pointList = Primitives2D.DrawCircle(spriteBatch, origin, radius, sides, Color.White, thickness);
        }
 

    }
}
