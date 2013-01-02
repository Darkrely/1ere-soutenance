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
    class SoundManager
    {
        SoundEffect musiquelancement, musiquejeu, taverne, festif, aventure;
        SoundEffectInstance sonmanager;
        bool jeu;
        int piste;

        public SoundManager()
        {
            jeu = true;
            piste = 1;
        }

        public void LoadContent(ContentManager content)
        {
            musiquelancement = content.Load<SoundEffect>("debut");
            musiquejeu = content.Load<SoundEffect>("01-yasuharu_takanashi-fairy_tail_main_theme-cocmp3");
            taverne = content.Load<SoundEffect>("taverne");
            festif = content.Load<SoundEffect>("festif"); 
            aventure = content.Load<SoundEffect>("aventure");
            sonmanager = musiquelancement.CreateInstance();
            sonmanager.IsLooped = true;
            sonmanager.Play();
        }

        public void Update(GameManager gameManager, Menu menu)
        {
            if (jeu && gameManager.Etat == GameManager.etat.InGame)
            {
                sonmanager.Stop();
                sonmanager = taverne.CreateInstance();
                sonmanager.IsLooped = false;
                sonmanager.Play();
                jeu = false;
            }

            if (gameManager.Etat == GameManager.etat.InGame && sonmanager.State == SoundState.Stopped)
            {
                piste++;
                if (piste == 1)
                {
                    sonmanager.Stop();
                    sonmanager = taverne.CreateInstance();
                    sonmanager.IsLooped = false;
                    sonmanager.Play();
                }
                else if (piste == 2)
                {
                    sonmanager.Stop();
                    sonmanager = festif.CreateInstance();
                    sonmanager.IsLooped = false;
                    sonmanager.Play();
                }
                else if (piste == 3)
                {
                    sonmanager.Stop();
                    sonmanager = aventure.CreateInstance();
                    sonmanager.IsLooped = false;
                    sonmanager.Play();
                }
                else
                {
                    piste = 0;
                }
            }

            if (menu.sound == Menu.Son.On && sonmanager.State == SoundState.Paused)
            {
                sonmanager.Play();
            }

            if (menu.sound == Menu.Son.Off && sonmanager.State == SoundState.Playing)
            {
                sonmanager.Pause();
            }
        }
    }
}
