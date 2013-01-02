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
    class Arme
    {
        #region Declaration
        string nomarme;
        public enum typearme { Poing,
            Pelle,
            Poignard,
            Epee,
            Hache
        };
        public int degat { get; set; }
        #endregion

        public Arme(typearme arme)
        {
            ChangerArme(arme);
        }

        public void ChangerArme(typearme arme)
        {
            if (arme == typearme.Poing)
            {
                nomarme = "Hâche";
                degat = 18;
            }

            else if (arme == typearme.Pelle)
            {
                nomarme = "Pelle";
                degat = 5;
            }

            else if (arme == typearme.Poignard)
            {
                nomarme = "Poignard";
                degat = 8;
            }

            else if (arme == typearme.Epee)
            {
                nomarme = "Epée";
                degat = 14;
            }

            else
            {
                nomarme = "Poings";
                degat = 2;
            }
        }

        public void InfligerDegat(PersonnageAttaquable personnage)
        {
            personnage.RecevoirDegat(degat);
        }
    }
}
