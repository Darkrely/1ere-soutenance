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
    class PersonnageJouable : PersonnageAttaquable
    {
        #region Declaration

        #region Declaration gestion des textures
        bool haut, bas, gauche, droite;
        Texture2D[,] textureperso = new Texture2D[4, 3];
        int compteurtext;
        #endregion

        #region Declaration du Rectangle (contient les coordonnées du joueur)
        private Rectangle rectangle;
        public Rectangle _rectangle
        {
            get { return rectangle; }
            set { value = rectangle; }
        }
        #endregion

        #region Caracteristique du joueur
        public enum Sexe
        {
            homme,
            femme,
        };
        Sexe sexe;
        public int vieMax, manaMax;
        #endregion

        #region Gestion de l'affichage
        GameWindow window;
        bool vershaut, versbas, versgauche, versdroite;
        #endregion

        #region Gestion des deplacements
        public int X { get; set; }
        public int Y { get; set; }
        public bool dplcmthaut { get; set; }
        public bool dplcmtbas { get; set; }
        public bool dplcmtgauche { get; set; }
        public bool dplcmtdroite { get; set; }
        public bool positionne { get; set; }
        public bool blocgauche { get; set; }
        public bool blocdroit { get; set; }
        public bool blochaut { get; set; }
        public bool blocbas { get; set; }
        public Rectangle collisionhaut { get; set; }
        public Rectangle collisionbas { get; set; }
        public Rectangle collisiongauche { get; set; }
        public Rectangle collisiondroite { get; set; }
        #endregion

        #region Gestion de la course
        bool courseactive = false;
        #endregion

        #endregion

        public PersonnageJouable(GameWindow window, Sexe sexe)
            : base(40, 0, 1, new Arme(Arme.typearme.Poing))
        {
            #region Rectangles de collision
                rectangle = new Rectangle(window.ClientBounds.Width / 2 - 10, window.ClientBounds.Height / 2 - 25, 40, 100);
                collisionhaut = new Rectangle(rectangle.X, rectangle.Y + 60, rectangle.Width, 40);
                collisionbas = new Rectangle(rectangle.X, rectangle.Y + rectangle.Height, rectangle.Width, 10);
                collisiongauche = new Rectangle(rectangle.X - 10, rectangle.Y + 60, 10, 40);
                collisiondroite = new Rectangle(rectangle.X + rectangle.Width, rectangle.Y + 60, 10, 40);
            #endregion

            this.window = window;
            this.sexe = sexe;
            vieMax = 40;
            manaMax = 0;
            #region Initialisation des booleens de deplacement
            dplcmthaut = false;
            dplcmtbas = false;
            dplcmtgauche = false;
            dplcmtdroite = false;
            versbas = false;
            versdroite = false;
            versgauche = false;
            vershaut = false;
            blocbas = false;
            blocdroit = false;
            blocgauche = false;
            blochaut = false;
            #endregion

            #region Initialisation des textures(Vers le bas)
            haut = false;
            bas = true;
            gauche = false;
            droite = false;
            compteurtext = 0;
            #endregion
        }

        public PersonnageJouable(int vie, int mana, int force, Arme arme, Sexe sexe, GameWindow window)
            : base(vie, mana, force, arme)
        {
            #region Rectangles de collision
                rectangle = new Rectangle(window.ClientBounds.Width / 2 - 10, window.ClientBounds.Height / 2 - 25, 40, 100);
                collisionhaut = new Rectangle(rectangle.X, rectangle.Y + 60, rectangle.Width, 40);
                collisionbas = new Rectangle(rectangle.X, rectangle.Y + rectangle.Height, rectangle.Width, 10);
                collisiongauche = new Rectangle(rectangle.X - 10, rectangle.Y + 80, 10, 20);
                collisiondroite = new Rectangle(rectangle.X + rectangle.Width, rectangle.Y + 80, 10, 20);
            #endregion

            this.sexe = sexe;
            this.window = window;
            vieMax = 40;
            manaMax = 0;
            #region Initialisation des booleens de deplacement
            dplcmthaut = false;
            dplcmtbas = false;
            dplcmtgauche = false;
            dplcmtdroite = false;
            versbas = false;
            versdroite = false;
            versgauche = false;
            vershaut = false;
            #endregion
            
            #region Initialisation des textures(Vers le bas)
            haut = false;
            bas = true;
            gauche = false;
            droite = false;
            compteurtext = 0;
            #endregion
        }

        public void LoadContent(ContentManager content)
        {
            #region femme
            if (sexe == Sexe.femme)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        textureperso[i, j] = content.Load<Texture2D>("TextureJoueur\\Femme\\persof-" + i.ToString() + "-" + j.ToString());
                    }
                }
            }
            #endregion

            #region homme
            if (sexe == Sexe.homme)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        textureperso[i, j] = content.Load<Texture2D>("TextureJoueur\\Homme\\persoh-" + i.ToString() + "-" + j.ToString());
                    }
                }
            }
            #endregion
        }

        public void Update(KeyboardState touche)
        {

            if(touche.IsKeyDown(Keys.Up) || touche.IsKeyDown(Keys.Down) || touche.IsKeyDown(Keys.Left) || touche.IsKeyDown(Keys.Right))
                compteurtext++;

            #region Test deplacement du joueur en cours?
            if (rectangle.X + rectangle.Width / 2 < window.ClientBounds.Width / 2)
                dplcmtgauche = true;
            else
                dplcmtgauche = false;
            if (rectangle.X + rectangle.Width / 2 > window.ClientBounds.Width / 2)
                dplcmtdroite = true;
            else
                dplcmtdroite = false;
            if (rectangle.Y + rectangle.Height / 2 < window.ClientBounds.Height / 2)
                dplcmthaut = true;
            else
                dplcmthaut = false;
            if (rectangle.Y + rectangle.Height / 2 > window.ClientBounds.Height / 2)
                dplcmtbas = true;
            else
                dplcmtbas = false;
            #endregion

            courseactive = touche.IsKeyDown(Keys.LeftShift);
            

            UpdateDirection(touche);
        }

        public void UpdateDirection(KeyboardState touche)
        {
            #region Haut
            if (touche.IsKeyDown(Keys.Up) && !(touche.IsKeyDown(Keys.Down) && touche.IsKeyDown(Keys.Left) && touche.IsKeyDown(Keys.Right)))
            {
                if (!versbas && !versdroite && !versgauche)
                    vershaut = true;

                haut = true;
                bas = false;
                gauche = false;
                droite = false;
            }
            if(touche.IsKeyUp(Keys.Up))
                vershaut = false;
            #endregion

            #region Bas
            if (touche.IsKeyDown(Keys.Down) && !(touche.IsKeyDown(Keys.Up) && touche.IsKeyDown(Keys.Left) && touche.IsKeyDown(Keys.Right)))
            {
                if(!vershaut && !versdroite && !versgauche)
                    versbas = true;

                haut = false;
                bas = true;
                gauche = false;
                droite = false;
            }
            if(touche.IsKeyUp(Keys.Down))
                versbas = false;
            #endregion

            #region Gauche
            if (touche.IsKeyDown(Keys.Left) && !(touche.IsKeyDown(Keys.Down) && touche.IsKeyDown(Keys.Up) && touche.IsKeyDown(Keys.Right)))
            {
                if(!vershaut && !versbas && !versdroite)
                    versgauche = true;

                haut = false;
                bas = false;
                gauche = true;
                droite = false;
            }
            if(touche.IsKeyUp(Keys.Left))
                versgauche = false;
            #endregion

            #region Droite
            if (touche.IsKeyDown(Keys.Right) && !(touche.IsKeyDown(Keys.Down) && touche.IsKeyDown(Keys.Left) && touche.IsKeyDown(Keys.Up)))
            {
                if(!vershaut && !versbas && !versgauche)
                    versdroite = true;
                haut = false;
                bas = false;
                gauche = false;
                droite = true;
            }
            if(touche.IsKeyUp(Keys.Right))
                versdroite = false;
            #endregion
        }

        public void PositionnementJoueur(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Deplacement(bool limitehautecarteatteinte, bool limitebassecarteatteinte, bool limitegauchecarteatteinte, bool limitedroitecarteatteinte, KeyboardState clavier, GameWindow window)
        {
            #region Taille de la fenetre
                int hauteurfenetre = window.ClientBounds.Height;
                int largeurfenetre = window.ClientBounds.Width;
            #endregion

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

            if (limitehautecarteatteinte)
            {
                if (clavier.IsKeyDown(Keys.Up) && !dplcmtbas)
                {
                    dplcmthaut = true;
                    if(Y > 0 && !blochaut)
                        Y -= deplacement;
                }
                
                if (Y > hauteurfenetre / 2 - rectangle.Height / 2 && !dplcmtbas)
                {
                    Y = window.ClientBounds.Height / 2 - this.rectangle.Height / 2;
                    dplcmthaut = false;
                }
            }
            if (clavier.IsKeyDown(Keys.Down) && dplcmthaut && !dplcmtbas && !blocbas)
            {
                Y += deplacement;
            }
            #endregion

            #region Bas
            if (limitebassecarteatteinte)
            {
                #region vers le bas
                if (clavier.IsKeyDown(Keys.Down) && !dplcmthaut)
                {
                    dplcmtbas = true;
                    if (Y + rectangle.Height < hauteurfenetre && !blocbas)
                        Y += deplacement;
                }
                #endregion

                #region Test fin de deplacement bas
                if (Y < hauteurfenetre / 2 - this.rectangle.Height / 2 && !dplcmthaut)
                {
                    Y = hauteurfenetre / 2 - this.rectangle.Height / 2;
                    dplcmtbas = false;
                }
                #endregion
            }

            #region Vers le haut
            if (clavier.IsKeyDown(Keys.Up) && dplcmtbas && !dplcmthaut && !blochaut)
            {
                Y -= deplacement;
            }
            #endregion

            #endregion

            #region Gauche
            if (limitegauchecarteatteinte)
            {
                #region Vers la gauche
                if (clavier.IsKeyDown(Keys.Left) && !dplcmtdroite)
                {
                    if (X <= largeurfenetre / 2 - rectangle.Width / 2)
                        dplcmtgauche = true;

                    if (X > 0 && !blocgauche)
                        X -= deplacement;
                }
                #endregion

                #region Test fin de deplacement à gauche
                if (dplcmtgauche && X > window.ClientBounds.Width / 2 - rectangle.Width / 2 && !dplcmtdroite)
                {
                    X = window.ClientBounds.Width / 2 - rectangle.Width / 2;
                    dplcmtgauche = false;
                }
                #endregion
            }

            #region Vers la droite
            if (clavier.IsKeyDown(Keys.Right) && dplcmtgauche && !dplcmtdroite && !blocdroit)
            {
                X += deplacement;
            }
            #endregion
            #endregion

            #region Droite
            if (limitedroitecarteatteinte)
            {
                #region Vers la droite
                if (clavier.IsKeyDown(Keys.Right) && !dplcmtgauche)
                {
                    if (X > largeurfenetre / 2 - this.rectangle.Width / 2)
                        dplcmtdroite = true;
                    if (X + rectangle.Width < largeurfenetre && !blocdroit)
                        X += deplacement;
                }
                #endregion

                #region Test fin de deplacement à droite

                if (X < largeurfenetre / 2 - this.rectangle.Width / 2 && !dplcmtgauche)
                {
                    X = largeurfenetre / 2 - this.rectangle.Width / 2;
                    dplcmtdroite = false;
                }
                #endregion
            }

            #region Vers la gauche
            if (clavier.IsKeyDown(Keys.Left) && dplcmtdroite && !dplcmtgauche && !blocgauche)
            {
                X -= deplacement;
            }
            #endregion
            #endregion

            #region Mise à jour des rectangles de collision
            rectangle = new Rectangle(X, Y, rectangle.Width, rectangle.Height);
            collisionhaut = new Rectangle(rectangle.X, rectangle.Y + 60, rectangle.Width, 40);
            collisionbas = new Rectangle(rectangle.X, rectangle.Y + rectangle.Height, rectangle.Width, 10);
            collisiongauche = new Rectangle(rectangle.X - 10, rectangle.Y + 80, 10, 20);
            collisiondroite = new Rectangle(rectangle.X + rectangle.Width, rectangle.Y + 80, 10, 20);
            #endregion
        }

        public void Draw(SpriteBatch spritebatch)
        { 
            #region phase1
            if (compteurtext >= 0 && compteurtext < 10)
            {
                if (haut)
                {
                    spritebatch.Draw(textureperso[0, 0], rectangle, Color.White);
                }
                if (bas)
                {
                    spritebatch.Draw(textureperso[1, 0], rectangle, Color.White);
                }
                if (gauche)
                {
                    spritebatch.Draw(textureperso[2, 0], rectangle, Color.White);
                }
                if (droite)
                {
                    spritebatch.Draw(textureperso[3, 0], rectangle, Color.White);
                }
            }
            #endregion

            #region phase2
            if (compteurtext >= 10 && compteurtext < 20)
            {
                if (haut)
                {
                    spritebatch.Draw(textureperso[0, 1], rectangle, Color.White);
                }
                if (bas)
                {
                    spritebatch.Draw(textureperso[1, 1], rectangle, Color.White);
                }
                if (gauche)
                {
                    spritebatch.Draw(textureperso[2, 1], rectangle, Color.White);
                }
                if (droite)
                {
                    spritebatch.Draw(textureperso[3, 1], rectangle, Color.White);
                }
            }
            #endregion

            #region phase3
            if (compteurtext >= 20 && compteurtext < 30)
            {
                if (haut)
                {
                    spritebatch.Draw(textureperso[0, 0], rectangle, Color.White);
                }
                if (bas)
                {
                    spritebatch.Draw(textureperso[1, 0], rectangle, Color.White);
                }
                if (gauche)
                {
                    spritebatch.Draw(textureperso[2, 0], rectangle, Color.White);
                }
                if (droite)
                {
                    spritebatch.Draw(textureperso[3, 0], rectangle, Color.White);
                }
            }
            #endregion

            #region phase4
            if (compteurtext >= 30 && compteurtext < 40)
            {
                if (haut)
                {
                    spritebatch.Draw(textureperso[0, 2], rectangle, Color.White);
                }
                if (bas)
                {
                    spritebatch.Draw(textureperso[1, 2], rectangle, Color.White);
                }
                if (gauche)
                {
                    spritebatch.Draw(textureperso[2, 2], rectangle, Color.White);
                }
                if (droite)
                {
                    spritebatch.Draw(textureperso[3, 2], rectangle, Color.White);
                }
            }
            if (compteurtext == 39)
                compteurtext = 0;
            #endregion
        }
    }
}