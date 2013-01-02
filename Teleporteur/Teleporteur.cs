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
using System.IO;
# endregion


namespace DragonTears
{
    class Teleporteur
    {
        public string Destination { get; set; }
        public string Arrive { get; set; }
        int xdestination, ydestination;
        int xarrive, yarrive;
        Rectangle zoneteleporteuse;
        Rectangle Coordonnees;

        public Teleporteur(int xdepart, int ydepart, int xdest, int ydest, string cartedest, string carteactuel, int width,int height)
        {
            xarrive = xdepart;
            yarrive = ydepart;
            xdestination = xdest;
            ydestination = ydest;
            Destination = cartedest;
            Arrive = carteactuel;
            zoneteleporteuse = new Rectangle(xarrive, yarrive, width, height);
            Coordonnees = zoneteleporteuse;
        }

        public void MiseAJourCoordonnees(int x, int y)
        {
            Coordonnees = new Rectangle(zoneteleporteuse.X + x, zoneteleporteuse.Y + y, zoneteleporteuse.Width, zoneteleporteuse.Height);
        }

        public void Teleportation(PersonnageJouable joueur, MapManager map, GameWindow window)
        {
            if (Coordonnees.Intersects(joueur._rectangle))
            {
                map.ChargementMap(Destination);
                map.Position(xdestination, ydestination, joueur, window);
            }
        }
    }
}
