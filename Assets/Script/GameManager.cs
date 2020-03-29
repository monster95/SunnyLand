using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioSource audioManager;
    public Animator fadeAnim;
    int status = 0;


    public void ChangeScene(int index)
    {
        fadeAnim.SetTrigger("FadeOut");
        status = index;
        Invoke("LoadScene", 1f);
    }

    void LoadScene()
    {
        switch(status)
        {
            case 0: Application.Quit();break;
            case 1: SceneManager.LoadScene("Main");break;
            case 2: SceneManager.LoadScene("Level01");break;
        }
    }
}
