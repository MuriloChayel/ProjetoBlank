using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narrator : MonoBehaviour
{
    public static Narrator Instance { get; private set; }

    public RandomClipsNarrador randomClipsNarrador;
    public NarratorObjectsEvents objectsEvents;
    public SequencialClips clip; //intro clip
    public SequencialClips[] EvenList;
    public AudioSource voicePlayer;

    public PlayerBehaviour player;
    public float t;
    bool ctrl = false;
    public float idleTime;
    public bool skipIntro;
    public bool finishEvent01;
    public int idClip;
    private void Awake()
    {
        voicePlayer = GetComponent<AudioSource>();
        Instance = this;
    }
    private void Start()
    {
        clip.Setup(voicePlayer);
        if (!skipIntro)
            StartEvent();
    }
    public void StartEvent()
    {
        clip.Setup(voicePlayer);
        voicePlayer.Stop();
        clip.Play();
    }
    public void NextEvent(float wait)
    {
        StartCoroutine(WaitForNext(wait));
    }
    
    IEnumerator WaitForNext(float wait)
    {
        yield return new WaitForSeconds(wait);
        idClip++;
        clip = EvenList[idClip];
        StartEvent();
    }
    private void Update()
    {
        if (clip.introFinished && player.tooMchIdle && !ctrl)
        {
            StartCoroutine(Wait());
        }
    }
    IEnumerator Wait()
    {
        ctrl = true;
        randomClipsNarrador.PlayRandomClip(voicePlayer, 0);
        yield return new WaitForSeconds(idleTime);
        ctrl = false;
    }
    public void TryOpenDoor()
    {
        print("Try open door");
        if (!voicePlayer.isPlaying)
        {
            objectsEvents.Setup(voicePlayer);
            randomClipsNarrador.PlayRandomClip(voicePlayer,2); //2 - playerReturns
            StartCoroutine(objectsEvents.EventTriggerAudio(t));
        }
    }
}
