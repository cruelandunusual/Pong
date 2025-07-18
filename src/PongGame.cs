using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Pong.src
{
    class PongGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        InputHelper inputHelper;
        static GameWorld gameWorld;
        static Point windowBounds;
        static Random random;


        static void Main()
        {
            PongGame game = new PongGame();
            game.Run();
        }


        public PongGame()
        {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
        }


        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();
            windowBounds = new Point(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            random = new Random();
            base.Initialize();
        }


        protected override void LoadContent()
        {
            gameWorld = new GameWorld(Content);
            gameWorld.LoadContent(Content);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            inputHelper = new InputHelper();
        }


        public static GameWorld GameWorld
        {
            get { return gameWorld; }
        }


        public static Point WindowBounds
        {
            get { return windowBounds; }
        }


        public static Random Random
        {
            get { return random; }
        }


        protected override void Update(GameTime gameTime)
        {
            inputHelper.Update();
            gameWorld.HandleInput(inputHelper);
            gameWorld.Update(gameTime, inputHelper);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            gameWorld.Draw(gameTime, spriteBatch);
        }
    }
}