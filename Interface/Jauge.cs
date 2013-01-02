using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;


namespace DragonTears
{
    class Jauge
    {
        public Texture2D jaugeVide;
        public Texture2D JaugeVide
        {
            get { return jaugeVide; }
            set { jaugeVide = value; }
        }
        Texture2D vie, mana;

        Vector2 position = new Vector2(10, -51);
        Rectangle rectangleVie = new Rectangle(13, -51, 196, 50);
        Rectangle rectangleMana = new Rectangle(13, -51, 196, 50);

        public Vector2 getPosition()
        {
            return position;
        }

        #region fonctions gestion
        public void LoadContent(ContentManager content, string text_jauge, string text_vie, string text_mana)
        {
            jaugeVide = content.Load<Texture2D>(text_jauge);
            vie = content.Load<Texture2D>(text_vie);
            mana = content.Load<Texture2D>(text_mana);
        }

        public void Update(bool combat)
        {
            if (combat == true)
            {
                if (position.Y < 10)
                {
                    position.Y++;
                }
            }
            
            if (combat == false && position.Y > -51)
            {
                position.Y--;
            }
        }

        public void UpdateSante(int sante, int santeMax)
        {
            rectangleVie.Width = (int)(((float)sante / (float)santeMax) * ((float)vie.Width));
            rectangleVie.Y = (int)position.Y;
        }

        public void UpdateMana(int mana, int manaMax)
        {
            rectangleMana.Width = (int)(((float)mana / (float)manaMax) * ((float)(this.mana).Width));
            rectangleMana.Y = (int)position.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(jaugeVide, position, Color.White);
            spriteBatch.Draw(vie, rectangleVie, Color.White);
            spriteBatch.Draw(mana, rectangleMana, Color.White);
            spriteBatch.End();
        }

        #endregion fonctions gestion


    }
}