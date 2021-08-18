using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RandomClips")]

public class RandomClipsNarrador : ScriptableObject
{
    public static RandomClipsNarrador Instance { get; private set; }

    [Header("Sequencias")]
    public SequencialClips[] sequentialDialogues;

    [Header("Clips")]
    public AudioClip[] idles;
    public AudioClip[] loops;   //repetindo acoes
    public AudioClip[] instrucoes;
    public AudioClip[] nearby;
    public AudioClip[] tutorial;

    public void WaitPlayRandom(AudioSource source, int id)
    {
        if (!source.isPlaying)
        {
            source.clip = GetRandomClipInArray(source, id);
            source.Play();
        }
    }
    public AudioClip GetRandomClipInArray(AudioSource source, int id) // id - tipo de clip
    {
        switch (id)
        {
            case 0: // idles
                return (idles[Random.Range(0, idles.Length)]);
            case 1: // loops 
                return (loops[Random.Range(0, idles.Length)]);
            default:
                return null;
        }
    }
}
