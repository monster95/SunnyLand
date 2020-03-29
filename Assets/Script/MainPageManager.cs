using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainPageManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip onClickClip;
    public float volume;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Play()
    {
        SceneManager.LoadScene("Level01");
    }

    public void OnClickPlay()
    {
        audioSource.PlayOneShot(onClickClip, volume);
        Invoke("Play", 0.5f);     
    }

    public void OnClickQuit()
    {
        audioSource.PlayOneShot(onClickClip, volume);
        Application.Quit();
    }
}
