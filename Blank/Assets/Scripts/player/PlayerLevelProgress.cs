using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelProgress : MonoBehaviour
{

    public GameObject[] AllLamps;
    public static PlayerLevelProgress Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }
    public void TurnOffOtherLights(GameObject currentLamp)
    {
        currentLamp.SetActive(true);
        foreach(var item in AllLamps)
        {
            if (item != currentLamp)
                item.SetActive(false);
        }
    }
}
