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
    class PersonnageAttaquable
    {
        #region Declaration
        public int vie, mana, force;
        protected Arme arme;
        protected Rectangle _collision;
        public Rectangle collision { get { return _collision; } set { _collision = value; } }
        
        #endregion

        public PersonnageAttaquable(int vie, int mana, int force, Arme arme)
        {
            this.vie = vie;
            this.mana = mana;
            this.force = force;
            this.arme = arme;
        }

        public void AttaquePhysique(PersonnageAttaquable personnage)
        {
            arme.InfligerDegat(personnage);
        }

        public void RecevoirDegat(int degat)
        {
            if (vie - (degat * force) <= 0)
                vie = 0;
            else 
                vie -= degat * force;
        }
    }
}
