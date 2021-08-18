using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    public Image imgPanel;
    public Sprite[] story;
    public AudioClip[] clips;
    public float[] waitBtwScenes;
    public int currentId = 0;
    void Start()
    {

        currentId = 0;
        Next();
    }
    private IEnumerator Fade()
    {
        imgPanel.canvasRenderer.SetAlpha(0.0f);
        imgPanel.CrossFadeAlpha(1, 2.0f, false);
        yield return new WaitForSeconds(waitBtwScenes[currentId]);
        Next();
    }
    public void Next()
    {
        if (currentId < story.Length)
            imgPanel.sprite = story[currentId];
        if (currentId < waitBtwScenes.Length) 
        { 
            StartCoroutine(Fade());
            currentId++;
        }
        else
            StartGame();

    }
    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }
}
