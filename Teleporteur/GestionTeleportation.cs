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
    class GestionTeleportation
    {
        List<Teleporteur> teleporteurs;
        public GestionTeleportation()
        {
            teleporteurs = new List<Teleporteur>() { };
            ChargementTeleporteurs();
        }

        public void ChargementTeleporteurs()
        {
            try
            {
                StreamReader monStreamReader = new StreamReader("teleporteurs.tel");
                string line = monStreamReader.ReadLine();
                int index = 0;
                string Destination;
                string Arrive;
                int xdestination, ydestination;
                int xarrive, yarrive;
                int width, height;

                teleporteurs.Add(new Teleporteur(1320, 0, 1320, 660, "lac.map", "plage.map", 80, 40));
                teleporteurs.Add(new Teleporteur(1320, 760, 1320, 0, "plage.map", "lac.map", 80, 40));
                Destination = "";
                Arrive = "";
                xdestination = 0;
                ydestination = 0;
                xarrive = 0;
                yarrive = 0;
                width = 0;
                height = 0;

                while (line != null)
                {
                    if (index == 0)
                    {
                        Destination = "";
                        Arrive = "";
                        xdestination = 0;
                        ydestination = 0;
                        xarrive = 0;
                        yarrive = 0;
                        width = 0;
                        height = 0;
                    }
                    else
                    {
                        switch(index)
                        {
                            case 1:
                                Destination = line;
                                break;
                            case 2:
                                Arrive = line;
                                break;
                            case 3:
                                xdestination = int.Parse(line);
                                break;
                            case 4:
                                ydestination = int.Parse(line);
                                break;
                            case 5:
                                xarrive = int.Parse(line);
                                break;
                            case 6:
                                yarrive = int.Parse(line);
                                break;
                            case 7:
                                width = int.Parse(line);
                                break;
                            case 8:
                                height = int.Parse(line);
                                break;
                            default:
                                teleporteurs.Add(new Teleporteur(xarrive, yarrive, xdestination, ydestination, Destination, Arrive, width, height));
                                index = 0;
                                break;
                        }
                        line = monStreamReader.ReadLine();
                    }
                    index++;
                }
                monStreamReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void MiseAJourTeleporteurs(PersonnageJouable joueur, MapManager map, GameWindow window)
        {
            foreach (Teleporteur teleporteur in teleporteurs)
            {
                if (teleporteur.Arrive == map.actuelmap)
                {
                    teleporteur.MiseAJourCoordonnees(map.xmap, map.ymap);
                }
            }

            foreach (Teleporteur teleporteur in teleporteurs)
            {
                if (teleporteur.Arrive == map.actuelmap)
                {
                    teleporteur.Teleportation(joueur, map, window);
                }
            }
        }
    }
}
