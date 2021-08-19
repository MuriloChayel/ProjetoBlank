using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BoxArea/Box Area Methods")]
public class FuncBoxArea : ScriptableObject
{

    [Header("box area properties")]

    [Header("properties")]
    public int currentStep;


    //COUNTS THE AMOUNT OF BOX AREAS
    int count;
 
    public void ReceiveBoxID(float waitTime, int id)
    {

    }
    public void ReceiveBoxAreaID(int idBox)
    {
        //currentStep = currentStep < idBox? currentStep : idBox;
        currentStep = idBox;
    }
    public void SpawnItemInLevel(Vector2 position, Levels currentLevel, GameObject itemObj, int id)
    {
        currentLevel.SpawnItem(itemObj, position, id);
    }
   
}
