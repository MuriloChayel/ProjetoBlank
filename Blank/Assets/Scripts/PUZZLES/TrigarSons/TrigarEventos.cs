using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Areas/trigarEventosEmArea ")]
public class TrigarEventos : ScriptableObject
{
    //is o som e permanente
    public bool isPerpetue;
    public AudioClip[] Fx;


    public void PlayAudio(AudioSource source, int id)
    {
        Debug.Log("play");
        source.clip = Fx[id];
        source.Play();
    }
    public void StopAudio(AudioSource source)
    {
        Debug.Log("stopped");

        source.Stop();
    }
}
