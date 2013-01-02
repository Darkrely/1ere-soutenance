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
    class MapManager
    {
        #region Declaration

        #region Declaration des textures
        Texture2D textureeau;
        Texture2D textureherbe;
        Texture2D texturesolbois;
        Texture2D texturemurbois;
        Texture2D texturesable;
        Texture2D texturesolpierre;
        Texture2D texturemurpierre;
        Texture2D textureeausable;
        Texture2D texturesableeau;
        Texture2D coltext;
        #endregion

        #region Declaration des limites
            bool limitehaute;
            bool limitebasse;
            bool limitegauche;
            bool limitedroite;
        #endregion

        #region Declaration de la liste contenant la carte
        List<List<char>> map;
        List<char> ligne;
        #endregion

        #region Initialisation d'une case
        Case casee;
        #endregion

        #region Declaration des coordonnees
        public int xmap { get; set; }
        public int ymap { get; set; }
        int xmax;
        int ymax;
        #endregion

        #region Blocage
        public List<List<Rectangle>> collision;
        public List<Rectangle> collisionligne;
        bool affichecol;
        bool touchecol;
        #endregion

        #region Gestion de la course
        bool courseactive = false;
        bool courseenactivation = false;
        #endregion

        public string actuelmap { get; set; }

        #endregion

        public MapManager()
        {
            #region Initialisation de la map sans decalage
            xmap = 0;
            ymap = 0;
            xmax = 0;
            ymax = 0;
            limitehaute = true;
            limitegauche = true;
            limitedroite = false;
            limitebasse = false;
            map = new List<List<char>>();
            ligne = new List<char>();
            collision = new List<List<Rectangle>>();
            collisionligne = new List<Rectangle>();
            affichecol = false;
            touchecol = false;
            #endregion
        }

        public MapManager(int x, int y)
        {
            #region Initialisation de la map avec un decalage
            xmap = -x;
            ymap = -y;
            xmax = 0;
            ymax = 0;
            map = new List<List<char>>();
            ligne = new List<char>();
            collision = new List<List<Rectangle>>();
            collisionligne = new List<Rectangle>();
            affichecol = false;
            touchecol = false;
            #endregion
        }

        public void LoadContent(ContentManager content)
        {
            #region Chargement du décor
            textureeau = content.Load<Texture2D>("Décors\\Mursetsols\\eau");
            textureherbe = content.Load<Texture2D>("Décors\\Mursetsols\\herbe");
            texturesolbois = content.Load<Texture2D>("Décors\\Mursetsols\\solbois");
            texturemurbois = content.Load<Texture2D>("Décors\\Mursetsols\\murbois");
            texturesable = content.Load<Texture2D>("Décors\\Mursetsols\\sable");
            texturesolpierre = content.Load<Texture2D>("Décors\\Mursetsols\\solpierre");
            texturemurpierre = content.Load<Texture2D>("Décors\\Mursetsols\\murpierre");
            textureeausable = content.Load<Texture2D>("Décors\\Mursetsols\\eausable");
            texturesableeau = content.Load<Texture2D>("Décors\\Mursetsols\\sableeau");
            coltext = content.Load<Texture2D>("collision");
            #endregion
        }

        public void Position(int x, int y, PersonnageJouable joueur, GameWindow window)
        {
            int hauteur = window.ClientBounds.Height;
            int largeur = window.ClientBounds.Width;
            int xjoueur = 0;
            int yjoueur = 0;
            xmap = 0;
            ymap = 0;
            

            #region Si la largeur carte est plus petite que l'ecran
            if (xmax <= largeur)
            {
                xmap = 0;
                xjoueur = x;
            }

            else
            {
                if (x < largeur / 2)
                {
                    xmap = 0;
                    xjoueur = x;
                }
                else if (x > xmax - largeur / 2)
                {
                    xmap = -(xmax - largeur);
                    xjoueur = xmax - x;
                }
                else
                {
                    xmap = -(x - largeur / 2);
                    xjoueur = largeur / 2 - 20;
                }
            }
            #endregion

            #region Si la hauteur carte est plus petite que l'ecran
            if (ymax <= hauteur)
            {
                ymap = 0;
                yjoueur = y;
            }

            else
            {
                if (y < largeur / 2)
                {
                    ymap = 0;
                    yjoueur = y;
                }
                else if (y > ymax - hauteur / 2)
                {
                    ymap = -(ymax - hauteur);
                    yjoueur = ymax - y;
                }
                else
                {
                    ymap = -(y - largeur / 2);
                    yjoueur = hauteur / 2 - 40;
                }
            }
            #endregion

            joueur.PositionnementJoueur(xjoueur, yjoueur);
        }

        public void Update(KeyboardState clavier, GameWindow window, PersonnageJouable perso)
        {
            #region miseajourdonnees
            ///////////////////Joueur//////////////////////////////////////
            Rectangle positionjoueur = perso._rectangle;
            ///////////////////Taille fenetre//////////////////////////////
            int largeur = window.ClientBounds.Width;
            int hauteur = window.ClientBounds.Height;
            ///////////////////Touche clavier//////////////////////////////
            bool toucheup = clavier.IsKeyDown(Keys.Up) && !(clavier.IsKeyDown(Keys.Down) && clavier.IsKeyDown(Keys.Left) && clavier.IsKeyDown(Keys.Right));
            bool touchedown = clavier.IsKeyDown(Keys.Down) && !(clavier.IsKeyDown(Keys.Up) && clavier.IsKeyDown(Keys.Left) && clavier.IsKeyDown(Keys.Right));
            bool toucheleft = clavier.IsKeyDown(Keys.Left) && !(clavier.IsKeyDown(Keys.Down) && clavier.IsKeyDown(Keys.Up) && clavier.IsKeyDown(Keys.Right));
            bool toucheright = clavier.IsKeyDown(Keys.Right) && !(clavier.IsKeyDown(Keys.Down) && clavier.IsKeyDown(Keys.Left) && clavier.IsKeyDown(Keys.Up));
            ///////////////////////////////////////////////////////////////
            #endregion

            courseactive = clavier.IsKeyDown(Keys.LeftShift);

            #region Valeur de deplacement
                int deplacement;

                if (courseactive)
                {
                    deplacement = 4;
                }

                else
                {
                    deplacement = 2;
                }
            #endregion

            #region Haut
            if (toucheup)
                {
                    if (ymap < 0 && !perso.dplcmtbas && !perso.blochaut)
                    {
                        ymap += deplacement;
                        limitehaute = false;
                    }
                }

                if (ymap >= 0)
                {
                    limitehaute = true;
                }
                #endregion

            #region Bas
                if (-ymap >= -hauteur + ymax || (ymax < hauteur))
                {
                    limitebasse = true;
                }

                if (touchedown)
                {
                    if (-hauteur + ymax > -ymap && !(ymax < hauteur) && !perso.dplcmthaut && !perso.blocbas)
                    {
                        ymap -= deplacement;
                        limitebasse = false;
                    }
                }

                #endregion
               
            #region Gauche
                if (toucheleft)
                {
                    if (xmap < 0 && !perso.dplcmtdroite && !perso.blocgauche)
                    {
                        xmap += deplacement;
                        limitegauche = false;
                    }
                }

                if (xmap >= 0)
                {
                    limitegauche = true;
                }
                #endregion
                
            #region Droite
                if (toucheright)
                {
                    if (xmap > largeur - xmax + 40 && !(xmax < largeur) && !perso.dplcmtgauche && !perso.blocdroit)
                    {
                        xmap -= deplacement;
                        limitedroite = false;
                    }
                }

                if (xmap <= largeur - xmax + 40 || xmax < largeur)
                {
                    limitedroite = true;
                }
                #endregion

            #region affichage collision
            if (clavier.IsKeyDown(Keys.C))
            {
                touchecol = true;
            }

            if(clavier.IsKeyUp(Keys.C) && touchecol)
            {
                touchecol = false;
                affichecol = !affichecol;
            }
            #endregion

            perso.Deplacement(limitehaute, limitebasse, limitegauche, limitedroite, clavier, window);
        }

        public void ChargementMap(string adress)
        {
            actuelmap = adress;

            #region Initialisation des limites de la carte
            xmax = 0;
            ymax = 0;
            #endregion

            #region Initialisation de la liste
            map = new List<List<char>>() { };
            ligne = new List<char>() { };
            #endregion

            #region Lecture des données de carte
            try
            {
                StreamReader monStreamReader = new StreamReader("Map\\" + adress);
                string line = monStreamReader.ReadLine();

                while (line != null)
                {
                    ymax++;

                    if (xmax < line.Length)
                    {
                        xmax = line.Length;
                    }

                    ligne = new List<char>() { };

                    for (int i = 0; i < line.Length + 20; i++)
                    {
                        if (i < line.Length)
                        {
                            ligne.Add(line[i]);
                        }
                    }

                    map.Add(ligne);
                    collision.Add(collisionligne);
                    line = monStreamReader.ReadLine();
                }
                monStreamReader.Close();

                xmax *= 40;
                ymax *= 40;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endregion

            ChargementCollisionCarte(adress);
        }

        public void ChargementCollisionCarte(string adress)
        {
            int y = 0;
            collision = new List<List<Rectangle>>() { };
            collisionligne = new List<Rectangle>() { };

            #region Lecture des données de carte
            try
            {
                StreamReader monStreamReader = new StreamReader("Map\\" + adress);
                string line = monStreamReader.ReadLine();

                while (line != null)
                {
                    collisionligne = new List<Rectangle>() { };

                    for (int i = 0; i < line.Length + 20; i++)
                    {
                        if (i < line.Length)
                        {
                            #region Filtrage de la texture

                            if (line[i] == '~')
                            {
                                collisionligne.Add(new Rectangle(40 * i, 40 * y, 40, 40));
                            }

                            else if (line[i] == '_')
                            {

                            }

                            else if (line[i] == 'p')
                            {

                            }

                            else if (line[i] == '.')
                            {

                            }

                            else if (line[i] == '_')
                            {

                            }

                            else if (line[i] == '#')
                            {

                            }

                            else if (line[i] == '=')
                            {
                                collisionligne.Add(new Rectangle(40 * i, 40 * y, 40, 40));
                            }

                            else if (line[i] == '-')
                            {
                                collisionligne.Add(new Rectangle(40 * i, 40 * y, 40, 40));
                            }

                            else if (line[i] == '/')
                            {
                                collisionligne.Add(new Rectangle(40 * i + 20, 40 * y + 20, 20, 20));
                            }

                            else if (line[i] == '\\')
                            {
                                collisionligne.Add(new Rectangle(40 * i, 40 * y + 20, 20, 20));
                            }

                            else
                            {
                                collisionligne.Add(new Rectangle(40 * i, 40 * y, 40, 40));
                            }
                        }
                        else
                        {
                            collisionligne.Add(new Rectangle(40 * i, 40 * y, 40, 40));
                        }
                            #endregion
                    }

                    collision.Add(collisionligne);
                    line = monStreamReader.ReadLine();
                    y++;
                }
                monStreamReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endregion
        }

        public void AffichageMap(SpriteBatch spriteBatch)
        {
            #region Lecture de la liste representant la carte
            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
                {
                    #region Affichage des textures

                    casee = new Case(j * 40 + xmap, i * 40 + ymap, map[i][j]);

                    #region Filtrage de la texture
                    if (casee.text == Case.Typetext.Eau)
                    {
                        spriteBatch.Draw(textureeau, casee.rectangle, Color.White);
                    }

                    if (casee.text == Case.Typetext.Herbe)
                    {
                        spriteBatch.Draw(textureherbe, casee.rectangle, Color.White);
                    }

                    if (casee.text == Case.Typetext.Murbois)
                    {
                        spriteBatch.Draw(texturemurbois, casee.rectangle, Color.White);
                    }

                    if (casee.text == Case.Typetext.Murpierre)
                    {
                        spriteBatch.Draw(texturemurpierre, casee.rectangle, Color.White);
                    }

                    if (casee.text == Case.Typetext.Sable)
                    {
                        spriteBatch.Draw(texturesable, casee.rectangle, Color.White);
                    }

                    if (casee.text == Case.Typetext.SableEau)
                    {
                        spriteBatch.Draw(texturesableeau, casee.rectangle, Color.White);
                    }

                    if (casee.text == Case.Typetext.EauSable)
                    {
                        spriteBatch.Draw(textureeausable, casee.rectangle, Color.White);
                    }

                    if (casee.text == Case.Typetext.Solbois)
                    {
                        spriteBatch.Draw(texturesolbois, casee.rectangle, Color.White);
                    }

                    if (casee.text == Case.Typetext.Solpierre)
                    {
                        spriteBatch.Draw(texturesolpierre, casee.rectangle, Color.White);
                    }
                    #endregion

                    #endregion
                }
            }
            #endregion

            if (affichecol)
            {
                foreach (List<Rectangle> rectligne in collision)
                {
                    foreach (Rectangle rect in rectligne)
                    {
                        spriteBatch.Draw(coltext, new Rectangle(rect.X + xmap, rect.Y + ymap, rect.Width, rect.Height), Color.White);
                    }
                }
            }
        }
    }
}
