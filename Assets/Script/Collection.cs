using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collection : MonoBehaviour
{
    public Text collectText;
    public GameManager gameManager;
    public AudioClip collectClip;
    private int number = 0;

    private void Start()
    {
        collectText.text = "X " + number.ToString();
    }
    
    public void updateText()
    {
        number++;
        gameManager.audioManager.PlayOneShot(collectClip, 2f);
        collectText.text = "X " + number.ToString();
    }
}
