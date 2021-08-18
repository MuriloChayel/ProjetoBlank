using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxArea : MonoBehaviour
{
    //public Levels levels
    [Header("Properties")]
    public ResolvePuzzles resolvePuzzles;
    public Levels currentLevel;

    [Header("AereaProperties")]
    public bool haveAkey = false;
    public bool waitBeforeStart = false;
    public float WaitTime;
    public float waitTimeToShowItem;

    [Header("Audio Properties")]
    public bool haveSequentialAudios = false;
    public bool audioCooldownCtrl = false;
    public float cooldownBtwSeqAudio;

    public enum boxAreas
    {
        A = 0,
        B = 1,
        C = 2,
        D = 3,
        E = 4, 
        F = 5,
        G = 6,
        H = 7,
        I = 8,
        J = 9,
    };

    public boxAreas areaOrder;

    private void Start()
    {
        currentLevel = resolvePuzzles.level;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //funciona para abrir portas
            resolvePuzzles.ReceiveBoxID(WaitTime, (int)areaOrder);

            //SE POSSUIR SEQUENCIA NOS AUDIOS
            if (haveSequentialAudios && !audioCooldownCtrl)
            {
                StartCoroutine(ResetCooldown(cooldownBtwSeqAudio));
            }
            if (haveAkey)
            {
                 PassBoxArea(true, haveSequentialAudios);
            }
            else
            {
                PassBoxArea(false, haveSequentialAudios);
            }
        }
    }
    IEnumerator WaitALittle(bool temChave)
    {
        yield return new WaitForSeconds(WaitTime);
        resolvePuzzles.ReceiveBoxAreaID((int)areaOrder, waitTimeToShowItem, temChave, resolvePuzzles.level);
    }
    public void PassBoxArea(bool temChave, bool temAudioSequencia)
    {
        bool spawnedKey = resolvePuzzles.ReceiveBoxAreaID((int)areaOrder, waitTimeToShowItem, temChave, resolvePuzzles.level);
        if (spawnedKey)
            haveAkey = false;
    }
    //DOORS SEQUENTIAL AUDIO
    private IEnumerator ResetCooldown(float cooldown)
    {
        audioCooldownCtrl = true;
        currentLevel.NarratorPlaySequentialAudios();
        yield return new WaitForSeconds(cooldown);
        audioCooldownCtrl = false;
    }
}
