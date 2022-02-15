using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PGGE.Patterns;
using UnityEngine.SceneManagement;

public class GameApp : Singleton<GameApp>
{
   private bool _mPause = false;

   /// <summary>
   /// Function to pause the game.
   /// I have refactored the original property and 
   /// instead imlemented this function.
   /// </summary>
   /// <param name="flag"></param>

   public void PauseGame(bool flag)
   {
      _mPause = flag;
      //mOnPause?.Invoke(GamePaused);
      if (_mPause)
      {
         Time.timeScale = 0;
      }
      else
      {
         Time.timeScale = 1;
      }
   }

   //public bool GamePaused
   //{
   //  get { return _mPause; }
   //  set
   //  {
   //    _mPause = value;
   //    //mOnPause?.Invoke(GamePaused);
   //    if (_mPause)
   //    {
   //      Time.timeScale = 0;
   //    }
   //    else
   //    {
   //      Time.timeScale = 1;
   //    }
   //  }
   //}

   // Start is called before the first frame update

   void Start()
   {
      SceneManager.LoadScene("Menu");
   }

   void Update()
   {
      if (Input.GetKeyDown(KeyCode.Escape))
      {
         PauseGame(!_mPause);
      }
   }
}