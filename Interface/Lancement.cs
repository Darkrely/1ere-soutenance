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
    class Lancement
    {
        Video videoallumage;
        public bool lancementfini { get; set; }
        VideoPlayer player;
        Texture2D videotexture;

        public Lancement()
        {
        }

        public void LoadContent(ContentManager content)
        {
            videoallumage = content.Load<Video>("Composition");
            player = new VideoPlayer();
            player.Play(videoallumage);
        }

        public void Update(KeyboardState clavier, GameManager gameManager)
        {
            if (player.State == MediaState.Stopped)
                lancementfini = true;
            else
                lancementfini = false;

            if (clavier.IsKeyDown(Keys.Escape))
            {
                gameManager.Etat = GameManager.etat.Menu;
                player.Stop();
            }

        }

        public void Draw(SpriteBatch spriteBatch, GameWindow window)
        {
            if (player.State != MediaState.Stopped)
                videotexture = player.GetTexture();

            if (videotexture != null)
            {
                spriteBatch.Draw(videotexture, new Rectangle(0, 0, window.ClientBounds.Width, window.ClientBounds.Height), Color.White);
            }
        }
    }
}
