using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemClass.ItemType type;
    public Sprite sprite;

    public bool playAudio, waitTimetoPlay;
    bool ctrl;

    public float timeToStartSoundFx, loopTime;
    public AudioClip clip;
    public AudioSource source;


    private void Awake()
    {
        source = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>().sprite;    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        if (waitTimetoPlay)
            StartCoroutine(WaitALittle());
        else
            playAudio = true;
    }
    private void Update()
    {
        ItemSurroundSoundFX();  
    }
    public void ItemSurroundSoundFX()
    {
        if (playAudio && !ctrl)
            StartCoroutine(Sound(loopTime));
    }
    IEnumerator Sound(float cooldownSoundFx)
    {
        ctrl = true;
        source.clip = clip;
        source.Play();  
        yield return new WaitForSeconds(cooldownSoundFx);
        ctrl = false;
    }
    IEnumerator WaitALittle()
    {
        yield return new WaitForSeconds(timeToStartSoundFx);
        playAudio = true;
    }
}
