using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
public class ResolvePuzzles : MonoBehaviour
{
    public static ResolvePuzzles Instance { get; private set; }

    [Header("Level Properties")]
    public FuncBoxArea boxAreaFunctions;
    public CameraBehaviour cameraBehaviours;
    public Levels currentLevel;

    [Header("Room Properties")]
    public bool playerInThisRoom;
    public Image fadeImg;
    public Vector2 camPositionsInThisRoom;
    public PlayerLevelProgress lightsSettings;


    public GameObject lightsInThisRoom;

    [Header("level box areas")]
    [HideInInspector] public bool[] steps;
    public boxArea[] boxAreas;
    
    private void Awake()
    {
        lightsSettings = GetComponentInParent<PlayerLevelProgress>();
        currentLevel.StartLevel();

        Instance = this;
    }

    IEnumerator ActiveFirstRoomCollider()
    {
        yield return new WaitForSeconds(2);
        GetComponent<BoxCollider2D>().enabled = true;
    }

    [SerializeField]bool inStart;
    //FADE IN FADE OUT
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!inStart)
            {
                StartCoroutine(cameraBehaviours.FadeIn(fadeImg, camPositionsInThisRoom, lightsInThisRoom));
            }
            else
            {
                StartCoroutine(cameraBehaviours.FadeOut(fadeImg, camPositionsInThisRoom, lightsInThisRoom));
                inStart = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInThisRoom = false;
    }
}
