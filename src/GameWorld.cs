using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Pong.src
{
    class GameWorld
    {

        Ball ball;
        Paddle player1Paddle;
        Paddle player2Paddle;

        Player player1;
        Player player2;

        Texture2D netSegment;
        Vector2 netSegmentOrigin;
        Rectangle netSegmentSize;

        int maxScore;
        bool isGameOver;

        SpriteFont scoreFont;
        string str_Winner;
        string str_pressSpacebar;
        Texture2D gameOver;


        public GameWorld(ContentManager Content)
        {
            Content.RootDirectory = "Content";
        }


        public void LoadContent(ContentManager Content)
        {
            gameOver = Content.Load<Texture2D>("spr_gameover");
            netSegment = Content.Load<Texture2D>("netSegment");
            netSegmentOrigin = new Vector2(netSegment.Width, netSegment.Height) / 2;
            netSegmentSize = new Rectangle(0, 0, netSegment.Width, netSegment.Height);

            player1Paddle = new Paddle(Content, new Vector2(25.0f, PongGame.WindowBounds.Y / 2));
            player2Paddle = new Paddle(Content, new Vector2(PongGame.WindowBounds.X - 25.0f, PongGame.WindowBounds.Y / 2));

            player1 = new Player(player1Paddle, "Player 1");
            player2 = new Player(player2Paddle, "Player 2");

            ball = new Ball(Content);
            maxScore = 2;
            scoreFont = Content.Load<SpriteFont>("GameFont");
            str_pressSpacebar = "Press spacebar to continue...";

            Reset();
        }


        public void Reset()
        {
            player1.Score = 0;
            player2.Score = 0;
            isGameOver = false;
        }

        public int MAX_SCORE
        {
            get { return maxScore; }
        }


        public bool GAME_OVER
        {
            get { return isGameOver; }
            set
            {
                isGameOver = value;
            }
        }


        public void HandleInput(InputHelper inputHelper)
        {
            player1.Paddle.HandleInput(inputHelper);
            player2.Paddle.HandleInput(inputHelper);
        }


        public void DrawNet(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < PongGame.WindowBounds.Y; i += netSegment.Height * 2)
            {
                spriteBatch.Draw(netSegment, new Vector2(PongGame.WindowBounds.X / 2, i), netSegmentSize, Color.White, 0.0f, netSegmentOrigin, 1.0f, SpriteEffects.None, 1.0f);
            }
        }


        public string Str_Winner
        {
            get { return str_Winner; }
            set
            {
                str_Winner = value;
            }
        }


        public Ball Ball
        {
            get { return ball; }
        }


        public bool BallIsAtLeftOfScreen()
        {
            return Ball.LeftEdge.X <= player1.Paddle.RightEdge.X;
        }


        public bool BallIsHittingLeftPaddle()
        {
            return Ball.LeftEdge.Y >= player1.Paddle.Top.Y
                && Ball.LeftEdge.Y <= player1.Paddle.Bottom.Y;
        }


        public bool BallIsAtRightOfScreen()
        {
            return Ball.RightEdge.X >= player2.Paddle.LeftEdge.X;
        }


        public bool BallIsHittingRightPaddle()
        {
            return Ball.RightEdge.Y >= player2.Paddle.Top.Y
                && Ball.LeftEdge.Y <= player2.Paddle.Bottom.Y;
        }


        public bool BallIsHittingScreenBounds()
        {
            return Ball.BottomEdge.Y >= PongGame.WindowBounds.Y ||
                Ball.TopEdge.Y <= 0.0f;
        }


        public bool IsOutsideWorld(Vector2 location)
        {
            return location.X > PongGame.WindowBounds.X || location.X < 0 ||
                location.Y > PongGame.WindowBounds.Y || location.Y < 0;
        }


        public void Update(GameTime gameTime, InputHelper inputHelper)
        {
            if (!isGameOver)
            {
                player1.Paddle.Update(gameTime, inputHelper);
                player2.Paddle.UpdateAI(gameTime, ball);
                ball.Update(gameTime);
                if (BallIsAtLeftOfScreen())
                {
                    if (BallIsHittingLeftPaddle())
                        ball.PaddleBounce(player1.Paddle);
                    else if (IsOutsideWorld(ball.RightEdge))
                    {
                        player2.Score++;
                        ball.Reset(player2.Paddle);
                    }
                }
                else if (BallIsAtRightOfScreen())
                {
                    if (BallIsHittingRightPaddle())
                        ball.PaddleBounce(player2.Paddle);
                    else if (IsOutsideWorld(ball.LeftEdge))
                    {
                        player1.Score++;
                        ball.Reset(player1.Paddle);
                    }
                }
                else if (BallIsHittingScreenBounds())
                    ball.WindowBoundsBounce();
            }
            else if (inputHelper.KeyPressed(Keys.Space))
                Reset();
        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            string player1NameScore = player1.Name + ": " + player1.Score;
            string player2NameScore = player2.Name + ": " + player2.Score;
            spriteBatch.Begin();
            if (!isGameOver)
            {
                DrawNet(spriteBatch);
                player1.Paddle.Draw(gameTime, spriteBatch);
                player2.Paddle.Draw(gameTime, spriteBatch);
                ball.Draw(gameTime, spriteBatch);
                spriteBatch.DrawString(scoreFont, player1NameScore, new Vector2(20, 22), Color.White);
                spriteBatch.DrawString(scoreFont, player2NameScore, new Vector2(PongGame.WindowBounds.X - scoreFont.MeasureString(player2NameScore).Length() - 20, 22), Color.White);
            }
            else
            {
                string winner = Str_Winner + " wins! " + str_pressSpacebar;
                spriteBatch.DrawString(scoreFont, winner, new Vector2(PongGame.WindowBounds.X - scoreFont.MeasureString(winner).Length(), PongGame.WindowBounds.Y) / 2, Color.White);
            }
            spriteBatch.End();
        }
    }
}