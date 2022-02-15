using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class soundFX : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;

    public void OnButtonPress()
    {
        source.PlayOneShot(clip);
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("Menu");
        Destroy(gameObject, 1.5f);
    }
}
