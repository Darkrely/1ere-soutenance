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
    class Menu
    {
        #region Declaration
            Texture2D fond;
            int hauteur, largeur, choix;
            public enum Mode
        {
            Menu,
            Option,
            ChoixSexe,
            Solo,
            Multi,
            Pause,
                Choixediteurdemap,
                EditMap
        };
            public Mode mode;

            #region Son
                public enum Son
        {
            On, 
            Off
        };
                public Son sound;
            #endregion

            #region Selecteur
            Texture2D selecteur;
            bool clavierhaut, clavierbas, clavierentrer, changement;
        #endregion

            #region menu
            string solo, multi, option, reprendre, quitter;
        #endregion

            #region choixjeu
            string nouvjeu, charge;
        #endregion

            #region Option
                public enum Langue
        {
            Francais,
            Anglais
        };
                Langue langue;
                string son, sonetat, sonetatlanguage, language, nomlangue, plangue, pson;
            #endregion

            #region ChoixSexe
            string homme, fille, questionsexe;
        #endregion

            /*
            #region Editeur de carte
                string editeur, nouvellecarte, modifiercarte, questiontaillecarte, largeurcarte, hauteurcarte;
            #endregion
            */

            SpriteFont font;
            Rectangle rectselecteur;
        #endregion

        public Menu(GameWindow window)
        {
            hauteur = window.ClientBounds.Height;
            largeur = window.ClientBounds.Width;

            #region Booleen clavier
            clavierhaut = false;
            clavierbas = false;
            clavierentrer = false;
            changement = false;
            #endregion

            choix = 1;
            rectselecteur = new Rectangle(10, 60, 40, 30);
            langue = Langue.Francais;
            mode = Mode.Menu;
            sound = Son.On;            
        }

        public void LoadContent(ContentManager content)
        {
            fond = content.Load<Texture2D>("fond");
            selecteur = content.Load<Texture2D>("selecteur");
            font = content.Load<SpriteFont>("SpriteFont1");
        }

        public void Update(KeyboardState clavier, GameManager gameManager, Game1 game1, MapManager mapManager, GestionJeu jeu, PersonnageJouable joueur, GameWindow window)
        {
            #region langue
            if (langue == Langue.Francais)
            {
                solo = "Solo";
                multi = "Multijoueur";
                option = "Options";
                quitter = "Quitter";
                son = "Son: ";
                language = "Langue: ";
                nomlangue = "Francais";
                nouvjeu = "Nouvelle partie";
                charge = "Charger partie"; 
                homme = "Homme"; 
                fille = "Femme"; 
                questionsexe = "Que voulez-vous être?";
                reprendre = "Reprendre";

                /*
                #region Editeur de carte
                editeur = "Editeur de carte";
                nouvellecarte = "Nouvelle carte"; 
                modifiercarte = "Modifier carte"; 
                questiontaillecarte = "Quel est la taille de la carte?"; 
                largeurcarte = "Largeur: ";
                hauteurcarte = "Hauteur: ";
                #endregion
                */
            }
            else
            {
                solo = "Solo";
                multi = "Multiplayer";
                option = "Settings";
                quitter = "Exit";
                son = "Sound: ";
                language = "Language: ";
                nomlangue = "English";
                nouvjeu = "New game";
                charge = "Load game";
                homme = "Male";
                fille = "Female";
                questionsexe = "What do you want to be?";
                reprendre = "Resume";

                /*
                #region Editeur de carte
                    editeur = "Map editor";
                    nouvellecarte = "New map";
                    modifiercarte = "Modify map";
                    questiontaillecarte = "What's the size of the map?";
                    largeurcarte = "Width: ";
                    hauteurcarte = "Heigth: ";
                #endregion
                */
            }

            plangue = language + nomlangue;
            #endregion

            #region Son
            if (sound == Son.On)
                sonetat = "On";
            else
                sonetat = "Off";
            pson = son + sonetat;
            #endregion

            #region menu
            if (mode == Mode.Menu)
            {
                #region positionchoix
                if (!clavierhaut)
                {
                    if (clavier.IsKeyDown(Keys.Up) && !clavier.IsKeyDown(Keys.Down))
                    {
                        clavierhaut = true;
                    }
                }

                if (clavier.IsKeyUp(Keys.Up) && !clavier.IsKeyDown(Keys.Down) && clavierhaut)
                {
                    choix--;
                    clavierhaut = false;
                    if (choix <= 0)
                        choix = 4;
                }

                if (!clavierbas)
                {
                    if (!clavier.IsKeyDown(Keys.Up) && clavier.IsKeyDown(Keys.Down))
                    {
                        clavierbas = true;
                    }
                }

                if (!clavier.IsKeyDown(Keys.Up) && clavier.IsKeyUp(Keys.Down) && clavierbas)
                {
                    choix++;
                    clavierbas = false;
                    if (choix >= 5)
                        choix = 1;
                }
                rectselecteur = new Rectangle(10, 20 + choix * 40, 40, 30);
                #endregion

                /*
                #region positionchoix
                if (!clavierhaut)
                {
                    if (clavier.IsKeyDown(Keys.Up) && !clavier.IsKeyDown(Keys.Down))
                    {
                        clavierhaut = true;
                    }
                }

                if (clavier.IsKeyUp(Keys.Up) && !clavier.IsKeyDown(Keys.Down) && clavierhaut)
                {
                    choix--;
                    clavierhaut = false;
                    if (choix <= 0)
                        choix = 5;
                }

                if (!clavierbas)
                {
                    if (!clavier.IsKeyDown(Keys.Up) && clavier.IsKeyDown(Keys.Down))
                    {
                        clavierbas = true;
                    }
                }

                if (!clavier.IsKeyDown(Keys.Up) && clavier.IsKeyUp(Keys.Down) && clavierbas)
                {
                    choix++;
                    clavierbas = false;
                    if (choix >= 6)
                        choix = 1;
                }
                rectselecteur = new Rectangle(10, 20 + choix * 40, 40, 30);
                #endregion
                */

                #region validation

                if (clavier.IsKeyDown(Keys.Enter))
                {
                    clavierentrer = true;
                }
                if (clavierentrer && clavier.IsKeyUp(Keys.Enter))
                {
                    if (choix == 1)
                    {
                        changement = true;
                        rectselecteur = new Rectangle(10, 60, 40, 30);
                        choix = 1;
                        mode = Mode.Solo;
                    }
                    /*
                    if (choix == 3)
                    {
                        choix = 1;
                        rectselecteur = new Rectangle(10, 60, 40, 30);
                        changement = true;
                        mode = Mode.Choixediteurdemap;
                    }

                    if (choix == 4)
                    {
                        choix = 1;
                        rectselecteur = new Rectangle(10, 60, 40, 30);
                        changement = true;
                        mode = Mode.Option;
                    }
                    if (choix == 5)
                        game1.Exit();
                    */
                    if (choix == 3)
                    {
                        choix = 1;
                        rectselecteur = new Rectangle(10, 60, 40, 30);
                        changement = true;
                        mode = Mode.Option;
                    }

                    if (choix == 4)
                    {
                        game1.Exit();
                    }
                }
                #endregion
            }
            #endregion

            #region option
            if (mode == Mode.Option)
            {
                #region positionchoix
                if (!clavierhaut)
                {
                    if (clavier.IsKeyDown(Keys.Up) && !clavier.IsKeyDown(Keys.Down))
                    {
                        clavierhaut = true;
                    }
                }

                if (clavier.IsKeyUp(Keys.Up) && !clavier.IsKeyDown(Keys.Down) && clavierhaut)
                {
                    choix--;
                    clavierhaut = false;
                    if (choix <= 0)
                        choix = 2;
                }

                if (!clavierbas)
                {
                    if (!clavier.IsKeyDown(Keys.Up) && clavier.IsKeyDown(Keys.Down))
                    {
                        clavierbas = true;
                    }
                }

                if (!clavier.IsKeyDown(Keys.Up) && clavier.IsKeyUp(Keys.Down) && clavierbas)
                {
                    choix++;
                    clavierbas = false;
                    if (choix >= 3)
                        choix = 1;
                }
                rectselecteur = new Rectangle(10, 20 + choix * 40, 40, 30);
                #endregion

                #region Validation
                if (mode == Mode.Option)
                {
                    if (clavier.IsKeyDown(Keys.Enter))
                    {
                        clavierentrer = true;
                    }

                    if (clavier.IsKeyUp(Keys.Enter) && clavierentrer)
                    {
                        if (choix == 1 && !changement)
                        {
                            if (sound == Son.On)
                                sound = Son.Off;
                            else
                                sound = Son.On;
                        }

                        if (choix == 2)
                        {
                            if (langue == Langue.Francais)
                                langue = Langue.Anglais;
                            else
                                langue = Langue.Francais;
                        }
                        clavierentrer = false;
                    }
                }
                #endregion

                if (clavier.IsKeyDown(Keys.Escape))
                {
                    if (gameManager.Etat == GameManager.etat.Menu)
                    {
                        mode = Mode.Menu;
                    }
                    if (gameManager.Etat == GameManager.etat.Pause)
                    {
                        mode = Mode.Pause;
                    }
                    rectselecteur = new Rectangle(10, 60, 40, 30);
                    choix = 1;
                }

                if (clavier.IsKeyUp(Keys.Enter))
                    changement = false;
            }
            #endregion

            #region Solo
            if (mode == Mode.Solo)
            {
                #region positionchoix
                if (!clavierhaut)
                {
                    if (clavier.IsKeyDown(Keys.Up) && !clavier.IsKeyDown(Keys.Down))
                    {
                        clavierhaut = true;
                    }
                }

                if (clavier.IsKeyUp(Keys.Up) && !clavier.IsKeyDown(Keys.Down) && clavierhaut)
                {
                    choix--;
                    clavierhaut = false;
                    if (choix <= 0)
                        choix = 2;
                }

                if (!clavierbas)
                {
                    if (!clavier.IsKeyDown(Keys.Up) && clavier.IsKeyDown(Keys.Down))
                    {
                        clavierbas = true;
                    }
                }

                if (!clavier.IsKeyDown(Keys.Up) && clavier.IsKeyUp(Keys.Down) && clavierbas)
                {
                    choix++;
                    clavierbas = false;
                    if (choix >= 3)
                        choix = 1;
                }
                rectselecteur = new Rectangle(10, 20 + choix * 40, 40, 30);
                #endregion

                #region Validation
                if (mode == Mode.Solo)
                {
                    if (clavier.IsKeyDown(Keys.Enter))
                    {
                        clavierentrer = true;
                    }

                    if (clavier.IsKeyUp(Keys.Enter) && clavierentrer)
                    {
                        if (choix == 1 && !changement)
                        {
                            jeu.NouveauJeu(mapManager, joueur, window);
                            gameManager.Etat = GameManager.etat.InGame;
                        }

                        if (choix == 2)
                        {
                            
                        }
                        clavierentrer = false;
                    }
                }
                #endregion

                if (clavier.IsKeyDown(Keys.Escape))
                {
                    mode = Mode.Menu;
                    rectselecteur = new Rectangle(10, 60, 40, 30);
                    choix = 1;
                }

                if (clavier.IsKeyUp(Keys.Enter))
                    changement = false;
            }
            #endregion

            #region Choix Sexe

            #endregion

            #region Pause
            if (mode == Mode.Pause)
            {
                #region positionchoix
                if (!clavierhaut)
                {
                    if (clavier.IsKeyDown(Keys.Up) && !clavier.IsKeyDown(Keys.Down))
                    {
                        clavierhaut = true;
                    }
                }

                if (clavier.IsKeyUp(Keys.Up) && !clavier.IsKeyDown(Keys.Down) && clavierhaut)
                {
                    choix--;
                    clavierhaut = false;
                    if (choix <= 0)
                        choix = 3;
                }

                if (!clavierbas)
                {
                    if (!clavier.IsKeyDown(Keys.Up) && clavier.IsKeyDown(Keys.Down))
                    {
                        clavierbas = true;
                    }
                }

                if (!clavier.IsKeyDown(Keys.Up) && clavier.IsKeyUp(Keys.Down) && clavierbas)
                {
                    choix++;
                    clavierbas = false;
                    if (choix >= 4)
                        choix = 1;
                }
                rectselecteur = new Rectangle(10, 20 + choix * 40, 40, 30);
                #endregion

                #region validation
                if (clavier.IsKeyDown(Keys.Enter))
                {
                    clavierentrer = true;
                }
                if (clavierentrer && clavier.IsKeyUp(Keys.Enter))
                {
                    if (choix == 1)
                    {
                        gameManager.Etat = GameManager.etat.InGame;
                        mode = Mode.Pause;
                    }
                    if (choix == 2)
                    {
                        choix = 1;
                        rectselecteur = new Rectangle(10, 60, 40, 30);
                        changement = true;
                        mode = Mode.Option;
                    }
                    if (choix == 3)
                        game1.Exit();

                    clavierentrer = false;
                }
                #endregion
            }
            #endregion

            #region ChoixEditeurDeMap
            if (mode == Mode.Choixediteurdemap)
            {
                #region positionchoix
                if (!clavierhaut)
                {
                    if (clavier.IsKeyDown(Keys.Up) && !clavier.IsKeyDown(Keys.Down))
                    {
                        clavierhaut = true;
                    }
                }

                if (clavier.IsKeyUp(Keys.Up) && !clavier.IsKeyDown(Keys.Down) && clavierhaut)
                {
                    choix--;
                    clavierhaut = false;
                    if (choix <= 0)
                        choix = 2;
                }

                if (!clavierbas)
                {
                    if (!clavier.IsKeyDown(Keys.Up) && clavier.IsKeyDown(Keys.Down))
                    {
                        clavierbas = true;
                    }
                }

                if (!clavier.IsKeyDown(Keys.Up) && clavier.IsKeyUp(Keys.Down) && clavierbas)
                {
                    choix++;
                    clavierbas = false;
                    if (choix >= 3)
                        choix = 1;
                }
                rectselecteur = new Rectangle(10, 20 + choix * 40, 40, 30);
                #endregion

                #region Validation
                if (mode == Mode.Option)
                {
                    if (clavier.IsKeyDown(Keys.Enter))
                    {
                        clavierentrer = true;
                    }

                    if (clavier.IsKeyUp(Keys.Enter) && clavierentrer)
                    {
                        if (choix == 1 && !changement)
                        {
                            mode = Mode.EditMap;
                        }

                        if (choix == 2)
                        {
                            mode = Mode.EditMap;
                        }
                        clavierentrer = false;
                    }
                }
                #endregion

                if (clavier.IsKeyDown(Keys.Escape))
                {
                    if (gameManager.Etat == GameManager.etat.Menu)
                    {
                        mode = Mode.Menu;
                    }
                    rectselecteur = new Rectangle(10, 60, 40, 30);
                    choix = 1;
                }

                if (clavier.IsKeyUp(Keys.Enter))
                    changement = false;
            }
            #endregion
        }
        

        public void Draw(SpriteBatch spriteBatch)
        { 
            spriteBatch.Draw(fond, new Rectangle(0,0, largeur, hauteur), Color.White);

            #region Menu
            if (mode == Mode.Menu)
            {
                spriteBatch.DrawString(font, solo, new Vector2(60, 60), Color.Silver);
                spriteBatch.DrawString(font, multi, new Vector2(60, 100), Color.Silver);
                //spriteBatch.DrawString(font, editeur, new Vector2(60, 140), Color.Silver);
                //spriteBatch.DrawString(font, option, new Vector2(60, 180), Color.Silver);
                //spriteBatch.DrawString(font, quitter, new Vector2(60, 220), Color.Silver);
                spriteBatch.DrawString(font, option, new Vector2(60, 140), Color.Silver);
                spriteBatch.DrawString(font, quitter, new Vector2(60, 180), Color.Silver);
            }
            #endregion

            #region Option
            if (mode == Mode.Option)
            {
                spriteBatch.DrawString(font, pson, new Vector2(60, 60), Color.Silver);
                spriteBatch.DrawString(font, plangue, new Vector2(60, 100), Color.Silver);
            }
            #endregion

            #region Solo
            if (mode == Mode.Solo)
            {
                spriteBatch.DrawString(font, nouvjeu, new Vector2(60, 60), Color.Silver);
                spriteBatch.DrawString(font, charge, new Vector2(60, 100), Color.Silver);
            }
            #endregion

            #region Pause
            if (mode == Mode.Pause)
            {
                spriteBatch.DrawString(font, reprendre, new Vector2(60, 60), Color.Silver);
                spriteBatch.DrawString(font, option, new Vector2(60, 100), Color.Silver);
                spriteBatch.DrawString(font, quitter, new Vector2(60, 140), Color.Silver);
            }
            #endregion

            /*
            #region choixediteurdemap
            if(mode == Mode.Choixediteurdemap)
            {
                spriteBatch.DrawString(font, nouvellecarte, new Vector2(60, 60), Color.Silver);
                spriteBatch.DrawString(font, modifiercarte, new Vector2(60, 100), Color.Silver);
            }
            #endregion
            */
            spriteBatch.Draw(selecteur, rectselecteur, Color.White);
        }
    }
}