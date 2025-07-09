using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pong.src
{
    class Ball
    {
        Texture2D ball;
        Vector2 ballLocation;
        Vector2 ballVelocity;
        Vector2 ballOrigin;
        Vector2 ballStartPosition;
        float xSpeedDelta;
        Vector2 MINSPEED;
        Vector2 MAXSPEED;
        Vector2 INITVEL;

        public Ball(ContentManager Content)
        {
            ball = Content.Load<Texture2D>("spr_ball_blue");
            ballStartPosition = new Vector2(PongGame.WindowBounds.X / 2, 20.0f);
            INITVEL = new Vector2(0.5f, 0.3f);
            ballLocation = ballStartPosition;
            ballVelocity = INITVEL;
            ballOrigin = new Vector2(ball.Width, ball.Height) / 2;
            MINSPEED = new Vector2(-10.0f, -2.0f);
            MAXSPEED = new Vector2(10.0f, 2.0f);

            xSpeedDelta = 1.07f;
        }


        public void PaddleBounce(Paddle paddle)
        {
            ballVelocity.X = -ballVelocity.X * xSpeedDelta; // 
                                                            //ballVelocity = Vector2.Clamp(ballVelocity, MINSPEED, MAXSPEED);
                                                            //set y velocity based on distance from paddle centre
            float ySpeedDelta = ballLocation.Y - (paddle.PaddleLocation.Y + paddle.PaddleSize.Height / 2);
            ballVelocity.Y = ballVelocity.Y * (ySpeedDelta * 0.035f); //constant multiplier to lower deltaY to reasonable amount
            ballVelocity = Vector2.Clamp(ballVelocity, MINSPEED, MAXSPEED);
        }


        public void WindowBoundsBounce()
        {
            ballVelocity.Y = -ballVelocity.Y;
        }

        public void Reset(Paddle paddle)
        {
            ballLocation = ballStartPosition;
            ballVelocity = Vector2.Subtract(paddle.PaddleLocation, ballLocation);
            ballVelocity = Vector2.Normalize(ballVelocity);
            ballVelocity = Vector2.Multiply(ballVelocity, INITVEL);
        }


        public int BallHalfWidth
        {
            get { return ball.Width / 2; }
        }


        public Vector2 LeftEdge
        {
            get { return new Vector2(ballLocation.X - BallHalfWidth, ballLocation.Y); }
        }


        public Vector2 RightEdge
        {
            get { return new Vector2(ballLocation.X + BallHalfWidth, ballLocation.Y); }
        }


        public Vector2 TopEdge
        {
            get { return new Vector2(ballLocation.X, ballLocation.Y - BallHalfWidth); }
        }


        public Vector2 BottomEdge
        {
            get { return new Vector2(ballLocation.X, ballLocation.Y + BallHalfWidth); }
        }


        public Vector2 Location
        {
            get { return ballLocation; }
        }


        public Vector2 Velocity
        {
            get { return ballVelocity; }
            set
            {
                ballVelocity = value;
            }
        }


        public void Update(GameTime gameTime)
        {
            ballLocation += ballVelocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ball, ballLocation, null, Color.White, 0.0f, ballOrigin, 1.0f, SpriteEffects.None, 1.0f);
        }
    }
}