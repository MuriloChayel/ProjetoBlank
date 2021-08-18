using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuEnter : MonoBehaviour
{
    public static MainMenuEnter Instace { get; private set; }

    [Header("Scene Properties")]
    public Image fadeImg;

    public AudioClip confirmationClip;
    private AudioSource source;
    public bool inMainMenu = true;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        if (Instace == null)
            Instace = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(Instace);

    }
    void Update()
    {
        if (Input.anyKeyDown && inMainMenu)
            StartCoroutine(ClickSound());
    }
    IEnumerator ClickSound()
    {
        source.clip = confirmationClip;
        source.Play();
        yield return new WaitForSeconds(1);
        StartCoroutine(FadeIn());
    }
    IEnumerator FadeIn()
    {
        fadeImg.canvasRenderer.SetAlpha(0.0f); ;
        fadeImg.gameObject.SetActive(true);
        fadeImg.CrossFadeAlpha(1, 1.0f, false);
        print("set");
        yield return new WaitForSeconds(1.5f);
        NextScene();
    }
    public void NextScene()
    {
        inMainMenu = false;
        SceneManager.LoadScene(1);
    }
} 

