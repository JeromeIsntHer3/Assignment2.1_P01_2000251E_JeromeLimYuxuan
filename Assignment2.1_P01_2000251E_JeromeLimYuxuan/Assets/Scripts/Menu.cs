using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
   // We are going to add the button click 
   // from script instead of doing it from 
   // the Unity Editor.
   [SerializeField]
   Button _buttonSinglePlayer;
   [SerializeField]
   Button _buttonMultiPlayer;

   private void Start()
   {
      _buttonSinglePlayer.onClick.AddListener(
        delegate ()
        {
           OnClick_SinglePlayer();
        });

      _buttonMultiPlayer.onClick.AddListener(
        delegate ()
        {
           OnClick_MultiPlayer();
        });
   }

   public void OnClick_SinglePlayer()
   {
      Debug.Log("Loading Single Player Scene");
      SceneManager.LoadScene("SinglePlayer");
   }
   public void OnClick_MultiPlayer()
   {
      Debug.Log("Loading Multiplayer Launcher Scene");
      SceneManager.LoadScene("MultiPlayerLauncher");
   }
}
