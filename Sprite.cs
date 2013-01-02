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
    class Sprite
    {
        #region Declaration
        Texture2D texture;
        public Rectangle rectangle { get; set; }
        #endregion

        public Sprite(Vector2 position)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, 40, 40);
        }

        public Sprite(int x, int y)
        {
            rectangle = new Rectangle(x, y, 40, 40);
        }

        public void LoadContent(ContentManager content, string assetName)
        {
            texture = content.Load<Texture2D>(assetName);
        }

        public void Update(Vector2 translation)
        {
            Rectangle rect = rectangle;
            rectangle = new Rectangle(rect.X += (int)translation.X, rect.Y += (int)translation.Y, 40, 40);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, rectangle, Color.White);
        }
    }
}
