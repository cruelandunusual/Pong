
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


class InputHelper
{
    MouseState currentMouseState, previousMouseState;
    KeyboardState currentKeyboardState, previousKeyboardState;

    public void Update()
    {
        previousMouseState = currentMouseState;
        currentMouseState = Mouse.GetState();
        previousKeyboardState = currentKeyboardState;
        currentKeyboardState = Keyboard.GetState();
    }


    public MouseState CurrentMouseState
    {
        get { return currentMouseState; }
    }


    public KeyboardState CurrentKeyboardState
    {
        get { return currentKeyboardState; }
    }


    public Vector2 MousePosition
    {
        get { return new Vector2(currentMouseState.X, currentMouseState.Y); }
    }


    public bool KeyPressed(Keys k)
    {
        return currentKeyboardState.IsKeyDown(k) && previousKeyboardState.IsKeyUp(k);
    }


    public bool MouseLeftButtonPressed()
    {
        return currentMouseState.LeftButton == ButtonState.Pressed
            && previousMouseState.LeftButton == ButtonState.Pressed;
    }
}
