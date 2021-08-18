using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoors : MonoBehaviour
{
    public static OpenDoors Instance { get; private set; }

    public InventorySetup inventorySetup;

    public KeyCode openDoorKey;

    [Header("PROPERTIES")]
    public GameObject door;
    public BoxCollider2D doorCollider;
    
    [Header("CHECKs")]
    public bool levelComplete;
    public bool haveAKey = false;
    public bool inDoorArea = false;

    [Header("DOOR PROPERTIES")]
    public bool frontDoor;
    public GameObject openDoor, closeDoor;
    public BoxCollider2D frontDoorCollider;
    [Header("AUDIO")]
    public AudioClip openSound;
    public AudioClip tryingToOpen;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        Instance = this;
        if(!frontDoor)
            door.SetActive(false);
    }
    //ABRIR A PORTA + USAR CHAVE
    private void Update()
    {
        if (haveAKey && inDoorArea && Input.GetKeyDown(openDoorKey))
        {
            inventorySetup.UseKey();
            OpenDoor();
        }
        else if(inDoorArea && Input.GetKeyDown(openDoorKey))
        {
            source.clip = tryingToOpen;
            if(!source.isPlaying)
                source.Play();
            if(!Narrator.Instance.voicePlayer.isPlaying)
                Narrator.Instance.TryOpenDoor();
        }
    }
    // CHECANDO SE POSSUI CHAVES
    private void CheckIfHaveAKey()
    {
        if (inventorySetup.itemList.Count > 0)
        {
            foreach (var item in inventorySetup.itemList)
            {
                if (item.type == ItemClass.ItemType.chave)
                    haveAKey = true;
            }
        }
    }
    // OPEN DOOR
    private void OpenDoor()
    {
        source.clip = openSound;
        source.Play();

        if (!frontDoor)
        {
            door.SetActive(true);
            doorCollider.enabled = false;
        }
        else
        {
            closeDoor.SetActive(false);
            openDoor.SetActive(true);
            frontDoorCollider.enabled = false;
        }
    }
    //CHECAGEM ABRIR PORTA - NA AREA DA PORTA --
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CheckIfHaveAKey();
            inDoorArea = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inDoorArea = false;
    }
}
