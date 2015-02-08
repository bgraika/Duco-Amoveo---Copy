using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Duco_Amoveo
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        InsidePlayer InsideP = new InsidePlayer();
        OutsidePlayer OutsideP = new OutsidePlayer();
        Background back, mid;
        Vector2 backSpawn, midSpawn, backOrigin, midOrigin, stageOrigin;
        Texture2D backTexture, midTexture;
        public float rotationAngle;
        public double rotationAdjust;
        SoundManager SM = new SoundManager();
        Collision collCheck = new Collision();
        public bool isGameOver = false, isStopped = false;
        public SpriteFont font, font2;

        Texture2D Health_Back, Health_Front;

        public List<Vector2> hexPoints = new List<Vector2>();
        public List<Vector2> heptPoints = new List<Vector2>();
        public List<Vector2> octPoints = new List<Vector2>();
        public List<Vector2> circlePoints = new List<Vector2>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1728;
            graphics.PreferredBackBufferHeight = 972;
            this.Window.Title = "Hackathon Numba 1 Game";
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            backSpawn = new Vector2(600, 600);
            midSpawn = new Vector2(1500, 700);
            backTexture = null;
            midTexture = null;
            rotationAngle = 0;
            base.Initialize();
            SM.LoopInGame();
            stageOrigin = new Vector2(1728 / 2, 972 / 2);

            // Get Points stuff
            spriteBatch.Begin();
            circlePoints = Primitives2D.DrawCircle(spriteBatch, stageOrigin, 450, 50, Color.White, 8);
            hexPoints = Primitives2D.DrawCircle(spriteBatch, stageOrigin, 450, 6, Color.White, 8);
            heptPoints = Primitives2D.DrawCircle(spriteBatch, stageOrigin, 450, 7, Color.White, 8);
            octPoints = Primitives2D.DrawCircle(spriteBatch, stageOrigin, 450, 8, Color.White, 8);
            spriteBatch.End();
            Health_Back = new Texture2D(GraphicsDevice, 100, 20);
            Health_Front = new Texture2D(GraphicsDevice, 100, 20);
           
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            backTexture = Content.Load<Texture2D>("Background_Back");
            midTexture = Content.Load<Texture2D>("Background_Mid");
            InsideP.LoadContent(Content);
            OutsideP.LoadContent(Content);
            SM.LoadContent(Content);

            backOrigin.X = backTexture.Width / 2;
            backOrigin.Y = backTexture.Height / 2;
            midOrigin.X = midTexture.Width / 2;
            midOrigin.Y = midTexture.Height / 2;
            back = new Background(backTexture, backSpawn, backOrigin);
            mid = new Background(midTexture, midSpawn, midOrigin);

            font = Content.Load<SpriteFont>("GameFont");
            font2 = Content.Load<SpriteFont>("HealthFont");
            
        }

        protected override void UnloadContent()
        {
          
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            InsideP.Update(gameTime);

            if(!isGameOver)
                OutsideP.Update(gameTime, Content);

            back.Update(gameTime, 0.005f);
            mid.Update(gameTime, 0.01f);

            if (OutsideP.circleStage.isVisible)
            {
                collCheck.check(OutsideP.bulletList, InsideP, circlePoints, SM);
            }
            else if (OutsideP.hexStage.isVisible)
            {
                collCheck.check(OutsideP.bulletList, InsideP, hexPoints, SM);
            }
            else if (OutsideP.heptStage.isVisible)
            {
                collCheck.check(OutsideP.bulletList, InsideP, heptPoints, SM);
            }
            else if (OutsideP.octStage.isVisible)
            {
                collCheck.check(OutsideP.bulletList, InsideP, octPoints, SM);
            }

            
            if(InsideP.health <= 0)
            {
                InsideP.isVisible = false;
                isGameOver = true;
            }
            if (isGameOver && !isStopped)
            {
                SM.currentInstance.Stop();
                SM.ChangeLoopToDeath();
                isStopped = true;
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();

            back.Draw(spriteBatch);
            mid.Draw(spriteBatch);
            if (!isGameOver)
                InsideP.Draw(spriteBatch);
            else spriteBatch.DrawString(font, "Game Over", new Vector2(1728 / 2 - font.MeasureString("Game Over").X / 2, 972 / 2 - font.MeasureString("Game Over").Y / 2), Color.Crimson);
            OutsideP.Draw(spriteBatch);

            spriteBatch.DrawString(font2, "Health:", new Vector2(1400, 100), Color.Black);
            spriteBatch.DrawRectangle(new Vector2(1495, 100), new Vector2(110, 30), Color.Black);
            if(!isGameOver)
                spriteBatch.DrawRectangle(new Vector2(1500, 105), new Vector2(InsideP.health, 20), Color.Red);
               
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
