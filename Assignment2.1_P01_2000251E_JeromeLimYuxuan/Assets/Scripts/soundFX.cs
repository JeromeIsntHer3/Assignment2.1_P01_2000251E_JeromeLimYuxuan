using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class soundFX : MonoBehaviour
{
   public AudioSource source;
   public AudioClip clip;
   public string loadTo;

   public void OnButtonPress()
   {
      source.PlayOneShot(clip);
      DontDestroyOnLoad(gameObject);
      SceneManager.LoadScene(loadTo);
      Destroy(gameObject, 1.5f);
   }
}
