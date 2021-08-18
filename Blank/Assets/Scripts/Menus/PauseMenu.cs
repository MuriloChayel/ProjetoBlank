using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    private bool paused = false;
    public GameObject pauseUI;
    public GameObject inventory ;
    public AudioSource ambientSound;

    [SerializeField]
    private GameObject itemsButton;

    //CONFIGURAR O PRIMEIRO BOTAO SELECIONAVEL -- 
    private void FirstSelectButton()
    {
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(itemsButton, new BaseEventData(eventSystem));
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Pause()
    {
        ambientSound.pitch = .5f;
        pauseUI.SetActive(true);
        inventory.SetActive(false);
        FirstSelectButton();
        Time.timeScale = 0;
        paused = true;
    }
    public void Resume()
    {
        ambientSound.pitch = 1f;

        pauseUI.SetActive(false);
        inventory.SetActive(true);
        Time.timeScale = 1;
        paused = false;
    }
}
