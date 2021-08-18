using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narrator : MonoBehaviour
{
    public static Narrator Instance { get; private set; }

    public RandomClipsNarrador randomClipsNarrador;

    public AudioSource voicePlayer;

    public PlayerBehaviour player;

    bool ctrl = false;
    public float idleTime;
    public bool finishEvent01;

    private void Awake()
    {
        voicePlayer = GetComponent<AudioSource>();
        Instance = this;
    }
    private void Start()
    {
        randomClipsNarrador.sequentialDialogues[0].SetIntroToTrue();
        randomClipsNarrador.sequentialDialogues[0].PlayInOrder(voicePlayer);
        
    }
    private void Update()
    {
        if (randomClipsNarrador.sequentialDialogues[0].introFinished && player.tooMchIdle && !ctrl)
        {
            StartCoroutine(Wait());
        }
    }
    IEnumerator Wait()
    {
        ctrl = true;
        randomClipsNarrador.WaitPlayRandom(voicePlayer, 0);
        yield return new WaitForSeconds(idleTime);
        ctrl = false;
    }
    public void TryOpenDoor()
    {
        if(!voicePlayer.isPlaying)
            randomClipsNarrador.sequentialDialogues[0].PlayInOrder(voicePlayer);
    }
}
