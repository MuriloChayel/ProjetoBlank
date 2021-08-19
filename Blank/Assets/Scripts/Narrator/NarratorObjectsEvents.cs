using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Narrador/objects")]
public class NarratorObjectsEvents : ScriptableObject
{
    public AudioClip[] clipsInOrder;
    private AudioSource source;
    
    public int currentClipIndex;
    
    public void Setup(AudioSource source)
    {
        this.source = source;
    }
    public IEnumerator EventTriggerAudio(float t)
    {
        //IF PLAYER DOES NOT HAVE A KEY
        if (!InventorySetup.Instance.ReturnIfHaveThisItem(ItemClass.ItemType.chave))
        {
            yield return new WaitForSeconds(t);

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
}
