using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxArea : MonoBehaviour
{
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

    //public Levels levels
    [Header("Properties")]
    private FuncBoxArea boxAreaFunctions;
    private Levels currentLevel;

    [Header("Obj and positions")]
    public Transform spawnPos;
    public GameObject itemObj;

    [Header("Options")]
    public bool spawAItem;
    public bool needAKeyToSpawn;
    public bool inDoorArea;
    public KeyCode keyToSpawn;
    public bool spawnedItem;

    private void Start()
    {
        boxAreaFunctions = transform.parent.GetComponent<ResolvePuzzles>().boxAreaFunctions;
        currentLevel = transform.parent.GetComponent<ResolvePuzzles>().currentLevel;
        spawnedItem = false;
    }
    private void Update()
    {
        if (spawAItem)
        {
            if (needAKeyToSpawn && Input.GetKeyDown(keyToSpawn) && inDoorArea && !spawnedItem)
            {
                boxAreaFunctions.SpawnItemInLevel(spawnPos.position, currentLevel, itemObj, (int)areaOrder);
                spawnedItem = true;
            }
            else if (!needAKeyToSpawn && inDoorArea && !spawnedItem)
            {
                boxAreaFunctions.SpawnItemInLevel(spawnPos.position, currentLevel, itemObj, (int)areaOrder);
                spawnedItem = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inDoorArea = true;
            boxAreaFunctions.ReceiveBoxAreaID((int)areaOrder);
            PassarOIndexDaArea();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inDoorArea = false;

    }
    public void PassarOIndexDaArea()
    {
        currentLevel.SetStepComplete((int)areaOrder);
    }

}
