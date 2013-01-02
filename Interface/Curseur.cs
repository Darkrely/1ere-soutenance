#region Using
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
# endregion

namespace DragonTears
{
    class Curseur
    {
        #region Declaration
        MouseState buttonPress;
        Rectangle mousedetection;
        Texture2D texture;
        Vector2 position;
        #endregion

        public Curseur(Texture2D texture)
        {
            this.texture = texture;
            position = Vector2.Zero;
        }

        public void Update()
        {
            buttonPress = Mouse.GetState();
            position.X = buttonPress.X;
            position.Y = buttonPress.Y;
        }

        public bool Press()
        {
            buttonPress = Mouse.GetState();

            if (buttonPress.LeftButton == ButtonState.Pressed)
                return true;
            else
                return false;
        }

        public Rectangle getMouseContainer()
        {
            buttonPress = Mouse.GetState();

            mousedetection = new Rectangle((int)buttonPress.X, (int)buttonPress.Y, (int)1, (int)1);
            return mousedetection;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(position.X > 0 && position.Y > 0)
                spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
