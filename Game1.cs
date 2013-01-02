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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region Declaration
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        PersonnageJouable joueur;

        #region Gestionnaire
        GameManager gameManager;
        CollisionManager collisionManager;
        GestionJeu jeu;
        MapManager mapManager;
        SoundManager soundManager;
        GestionTeleportation gestionTeleportation;
        #endregion

        #region Interface
        Lancement lancement;
        Menu menu;
        Jauge jauge;
        Curseur curseur;
        #endregion
        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.ToggleFullScreen();

            #region Gestionnaire
            gameManager = new GameManager();
            jeu = new GestionJeu();
            mapManager = new MapManager();
            soundManager = new SoundManager();
            collisionManager = new CollisionManager();
            gestionTeleportation = new GestionTeleportation();
            #endregion

            #region Interface
            lancement = new Lancement();
            menu = new Menu(Window);
            curseur = new Curseur(Content.Load<Texture2D>("Curseur"));
            jauge = new Jauge();
            #endregion

            joueur = new PersonnageJouable(Window, PersonnageJouable.Sexe.femme);
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            #region Gestionnaire
            soundManager.LoadContent(Content);
            mapManager.LoadContent(Content);
            #endregion

            joueur.LoadContent(Content);

            #region Interface
            lancement.LoadContent(Content);
            menu.LoadContent(Content);
            jauge.LoadContent(Content, "Jauge//barredevie", "Jauge//santepleine", "Jauge//manapleine");
            #endregion
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {   
            KeyboardState clavier = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            #region lancement
            if (gameManager.Etat == GameManager.etat.Lancement)
            {
                lancement.Update(clavier, gameManager);

                if (lancement.lancementfini)
                {
                    gameManager.Etat = GameManager.etat.Menu;
                }
            }
            #endregion

            #region menu
            if (gameManager.Etat == GameManager.etat.Menu || gameManager.Etat == GameManager.etat.Pause)
            {
                menu.Update(clavier, gameManager, this, mapManager, jeu, joueur, Window);
            }
            #endregion

            #region Jeu

            gameManager.Update(clavier, menu);
            gestionTeleportation.MiseAJourTeleporteurs(joueur, mapManager, Window);
            collisionManager.CollisionPersoDeplacement(mapManager, joueur);

            if (gameManager.Etat == GameManager.etat.InGame)
            {
                curseur.Update();
                mapManager.Update(clavier, Window, joueur);
                joueur.Update(clavier);
            }
            #endregion

            #region Son
            soundManager.Update(gameManager, menu);
            #endregion

            jauge.Update(gameManager.combat);
            jauge.UpdateMana(joueur.mana, joueur.manaMax);
            jauge.UpdateSante(joueur.vie, joueur.vieMax);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            #region lancement
            if (gameManager.Etat == GameManager.etat.Lancement)
                lancement.Draw(spriteBatch, Window);
            #endregion

            #region menu
            if (gameManager.Etat == GameManager.etat.Menu)
                menu.Draw(spriteBatch);
            #endregion

            #region jeu
            if (gameManager.Etat == GameManager.etat.InGame || gameManager.Etat == GameManager.etat.Pause)
            {
                mapManager.AffichageMap(spriteBatch);
                joueur.Draw(spriteBatch);
                if (gameManager.Etat == GameManager.etat.Pause)
                {
                    menu.Draw(spriteBatch);
                }

                curseur.Draw(spriteBatch);
            }
            #endregion

            spriteBatch.End();

            if(gameManager.Etat == GameManager.etat.InGame)
            jauge.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}