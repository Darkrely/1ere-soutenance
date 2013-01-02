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
    class Case : Sprite
    {
        #region Declaration
        public enum Typetext{
            Herbe,
            Sable,
            Solbois,
            Murbois,
            Eau,
            Solpierre,
            Murpierre,
            SableEau,
            EauSable,
            Noir,
        };
        public bool traversable { get; set; }
        public Typetext text;
        #endregion

        public Case(Vector2 position, char type)
            : base(position)
        {
            traversable = true;
            Attribuertype(type);   
        }

        public Case(int x, int y, char type)
            : base(x, y)
        {
            traversable = true;
            Attribuertype(type);
        }

        public void Attribuertype(char type)
        {
            switch (type)
            {
                case '~':
                    text = Typetext.Eau;
                    traversable = false;
                    break;
                case '#':
                    text = Typetext.Herbe;
                    traversable = true;
                    break;
                case '.':
                    text = Typetext.Sable;
                    traversable = true;
                    break;
                case '_':
                    text = Typetext.Solbois;
                    traversable = true;
                    break;
                case '-':
                    text = Typetext.Murbois;
                    traversable = false;
                    break;
                case 'p':
                    text = Typetext.Solpierre;
                    traversable = true;
                    break;
                case '=':
                    text = Typetext.Murpierre;
                    traversable = false;
                    break;
                case '\\':
                    text = Typetext.EauSable;
                    traversable = true;
                    break;
                case '/':
                    text = Typetext.SableEau;
                    traversable = true;
                    break;
                default:
                    text = Typetext.Noir;
                    traversable = false;
                    break;
            }
        }
    }
}
