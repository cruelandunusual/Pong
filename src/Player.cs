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
    class Player
    {

        int score;
        Paddle paddle;
        string name;


        public Player(Paddle paddle, string name)
        {
            score = 0;
            this.paddle = paddle;
            this.name = name;
        }


        public int Score
        {
            get { return score; }
            set
            {
                if (value >= PongGame.GameWorld.MAX_SCORE)
                {
                    PongGame.GameWorld.Str_Winner = Name;
                    PongGame.GameWorld.GAME_OVER = true;
                }
                else
                {
                    score = value;
                }
            }
        }


        public string Name
        {
            get { return name; }
        }


        public Paddle Paddle
        {
            get { return paddle; }
        }
    }

}