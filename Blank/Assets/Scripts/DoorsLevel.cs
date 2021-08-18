using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsLevel : MonoBehaviour
{
    public bool canOpen;

    private void OpenDoor()
    {
        print("open");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && canOpen)
        {
            OpenDoor();
        }
    }
}
