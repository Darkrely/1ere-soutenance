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
using Microsoft.Xna.Framework.Storage;
# endregion


namespace DragonTears
{
    class GestionJeu
    {
        public GestionJeu()
        {

        }

        public void NouveauJeu(MapManager map, PersonnageJouable joueur, GameWindow window) 
        {   
            map.ChargementMap("plage.map");
            map.Position(50, 200, joueur, window);
        }

        public void ChargerJeu(MapManager map, string carte, int x, int y, PersonnageJouable joueur, GameWindow window)
        {
            map.ChargementMap(carte);
            map.Position(x, y, joueur, window);
        }

        public void Teleportation(MapManager map, string carte, int x, int y, PersonnageJouable joueur, GameWindow window)
        {
            map.ChargementMap(carte);
            map.Position(x, y, joueur, window);
        }
    }
}
