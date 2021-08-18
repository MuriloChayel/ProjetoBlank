using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Narrador/SequentialClips")]
public class SequencialClips : ScriptableObject
{
    [Header("Intro")]
    public bool intro = false;
    public bool sequential;
    public bool introFinished;

    public float[] timeBtwClips;

    [Header("Settings")]
    public int currentClipIndex;
    public float timeLockDoorAudio;
    
    [Header("Clips")]
    public AudioClip[] clipsInOrder;

    [Header("DoorsClips")]
    public AudioClip TheDoorIsLockedAudio;

    public bool PlayInOrder(AudioSource source)
    {
        if (sequential) // ON THE DOOR
        {
            source.gameObject.GetComponent<MonoBehaviour>().StartCoroutine(TheDoorIsLocked(source));
        }
        else //START OF THE GAME
        {
            if (intro)
            {
                source.gameObject.GetComponent<MonoBehaviour>().StartCoroutine(Wait(source));
                intro = false;
                Debug.Log("Started");
            }
        }
        return source.isPlaying;
    }
    public void SetIntroToTrue()
    {
        intro = true;
    }
    private IEnumerator TheDoorIsLocked(AudioSource source)
    {
        //IF PLAYER DOES NOT HAVE A KEY
        if (!InventorySetup.Instance.ReturnIfHaveThisItem(ItemClass.ItemType.chave))
        {
            source.clip = TheDoorIsLockedAudio;
            source.Play();
            yield return new WaitForSeconds(timeLockDoorAudio);

            if (currentClipIndex == clipsInOrder.Length)
                currentClipIndex = 0;
            if (currentClipIndex < clipsInOrder.Length)
            {
                if (!source.isPlaying)
                {
                    source.clip = clipsInOrder[currentClipIndex];
                    source.Play();
                    currentClipIndex++;
                }
            }
        }
        else
        {
            Debug.Log("have a key");
        }
    }
    public IEnumerator Wait(AudioSource source)
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
