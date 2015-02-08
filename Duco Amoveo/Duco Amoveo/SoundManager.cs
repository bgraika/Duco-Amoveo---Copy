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

namespace Duco_Amoveo
{
    public class SoundManager
    {
       public SoundEffect smallShot, bigShot, bitsPlease, bulletBurst, death, inGame, menu, shapeShift, playerHurt;
       public List<SoundEffectInstance> soundList;
       public SoundEffectInstance currentInstance;

       public SoundManager()
       {
       }

       public void LoadContent(ContentManager Content)
       {
           smallShot = Content.Load<SoundEffect>("Amoveo_Small_Shot");
           bigShot = Content.Load<SoundEffect>("Amoveo_Big_Shot");
           bitsPlease = Content.Load<SoundEffect>("Amoveo_Bits_Please");
           bulletBurst = Content.Load<SoundEffect>("Amoveo_Bullet_Burst");
           death = Content.Load<SoundEffect>("Amoveo_Death");
           inGame = Content.Load<SoundEffect>("Amoveo_InGame");
           menu = Content.Load<SoundEffect>("Amoveo_Menu");
           shapeShift = Content.Load<SoundEffect>("Amoveo_Shape_Shift");
           playerHurt = Content.Load<SoundEffect>("Amoveo_Player_Hurt");
       }


       # region Plays

       public void playHurt()
       {
           SoundEffectInstance instance = playerHurt.CreateInstance();
           instance.IsLooped = false;
           instance.Play();
           ////instance.Stop();
           //smallShot.Play();
       }

       public void playSmallShot()
       {
           SoundEffectInstance instance = smallShot.CreateInstance();
           instance.IsLooped = false;
           instance.Volume -= 0.8f;
           instance.Play();
           ////instance.Stop();
           //smallShot.Play();
       }

       public void playBigShot()
       {
           SoundEffectInstance instance = bigShot.CreateInstance();
           instance.IsLooped = false;
           instance.Volume -= 0.2f;
           instance.Play();
           //bigShot.Play();
          
       }

       public void playBitsPlease()
       {
           SoundEffectInstance instance = bitsPlease.CreateInstance();
           instance.IsLooped = false;
           instance.Play();
           //bitsPlease.Play();
       }

       public void playBulletBurst()
       {
           SoundEffectInstance instance = bulletBurst.CreateInstance();
           instance.IsLooped = false;
           instance.Play();
           //bulletBurst.Play();
       }

       public void playDeath()
       {
           SoundEffectInstance instance = death.CreateInstance();
           instance.IsLooped = false;
           instance.Play();
           //death.Play();
       }

       public void playInGame()
       {
           SoundEffectInstance instance = inGame.CreateInstance();
           instance.IsLooped = false;
           instance.Play();

           //inGame.Play();
       }

       public void playMenu()
       {
           SoundEffectInstance instance = menu.CreateInstance();
           instance.IsLooped = false;
           instance.Play();
           //menu.Play();
       }

       public void playShapeShift()
       {
           SoundEffectInstance instance = shapeShift.CreateInstance();
           instance.IsLooped = false;
           instance.Play();
           //shapeShift.Play();
       }
       #endregion

       #region Loops

       public void LoopMenu()
       {
           SoundEffectInstance instance;
           instance = menu.CreateInstance();
           instance.IsLooped = true; 
           instance.Play();
           currentInstance = instance;
       }

        public void LoopInGame()
       {
           SoundEffectInstance instance;
           instance = inGame.CreateInstance();
           instance.IsLooped = true;
           instance.Play();
           currentInstance = instance;
       }

        public void LoopDeath()
        {
            SoundEffectInstance instance;
            instance = inGame.CreateInstance();
            instance.IsLooped = true;
            currentInstance = instance;
            currentInstance.Play();
        }

        public void ChangeLoopToDeath()
        {
            currentInstance = death.CreateInstance();
            currentInstance.IsLooped = true;
            currentInstance.Play();
        }

        #endregion

       
    }
}



