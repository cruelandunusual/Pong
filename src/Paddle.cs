using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pong.src
{
    class Paddle
    {
        Texture2D paddle;
        Vector2 paddleLocation;
        Vector2 paddleOrigin;
        Rectangle paddleSize;

        public Paddle(ContentManager Content, Vector2 loc)
        {
            paddle = Content.Load<Texture2D>("paddle");
            paddleLocation = loc;
            paddleOrigin = new Vector2(paddle.Width, paddle.Height) / 2;
            paddleSize = new Rectangle(0, 0, paddle.Width, paddle.Height);
        }


        public void HandleInput(InputHelper inputHelper)
        {
            //future implementation
        }


        public Vector2 PaddleLocation
        {
            get { return paddleLocation; }
        }


        public Vector2 LeftEdge
        {
            get { return new Vector2(PaddleLocation.X - PaddleSize.Width / 2, PaddleLocation.Y); }
        }


        public Vector2 RightEdge
        {
            get { return new Vector2(PaddleLocation.X + PaddleSize.Width / 2, PaddleLocation.Y); }
        }


        public Vector2 Top
        {
            get { return new Vector2(PaddleLocation.X, PaddleLocation.Y - PaddleSize.Height / 2); }
        }


        public Vector2 Bottom
        {
            get { return new Vector2(PaddleLocation.X, PaddleLocation.Y + PaddleSize.Height / 2); }
        }


        public Rectangle PaddleSize
        {
            get { return paddleSize; }
        }


        public void Update(GameTime gameTime, InputHelper inputHelper)
        {
            paddleLocation.Y = inputHelper.MousePosition.Y;
        }


        public void UpdateAI(GameTime gameTime, Ball ball)
        {
            float AItargetY = ball.RightEdge.Y - paddleSize.Height / 2;
            if (paddleLocation.Y < AItargetY - 35.0f)
            {
                paddleLocation.Y += 14.25f;
            }
            else if (paddleLocation.Y > AItargetY + 35.0)
            {
                paddleLocation.Y -= 14.25f;
            }
        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(paddle, paddleLocation, paddleSize, Color.White, 0.0f, paddleOrigin, 1.0f, SpriteEffects.None, 1.0f);
        }
    }

}