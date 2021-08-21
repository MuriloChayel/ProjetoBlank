using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Narrador/SequentialClips")]
public class SequencialClips : ScriptableObject
{
    [Header("Settings")]
    public NarratorObjectsEvents narratorObjects;
    public int currentClipIndex;
    public bool introFinished;
    private AudioSource source;
    
    [Header("Clips")]
    public AudioClip[] clipsInOrder;
    [Header("Intro Time")]
    public float[] timeBtwClips;


    public void Setup(AudioSource source) {
        this.source = source;
    }
    //RODANDO SONS ALEATORIOS

    public void Play()
    {
        source.gameObject.GetComponent<MonoBehaviour>().StartCoroutine(Wait());
    }
    public IEnumerator Wait()
    {
        for (int a = 0; a < clipsInOrder.Length; a++)
        {
            source.clip = clipsInOrder[a];
            source.Play();
            yield return new WaitForSeconds(timeBtwClips[a]);
        }
        introFinished = true;
    }
}
