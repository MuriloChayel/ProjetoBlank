using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Levels/level")]
public class Levels : ScriptableObject
{
    [Header("STEPS")]
    public int stepCount;
    public bool[] steps;

    [Header("COMPLETE")]
    public bool stepComplete;
    
    [Header("AUDIOS")]
    public SequencialClips clips;
    
    public void StartLevel()
    {
        stepComplete = false;
        steps = new bool[stepCount];
        for(int a = 0; a < stepCount; a++)
        {
            steps[a] = new bool();
        }
    }
    public void CompleteStep(int id)
    {
        if (id == 0)
        {
            steps[0] = true;
        }
        else
        {
            if (steps[id - 1])
                steps[id] = true;
        }
        stepComplete = CompleteLevel();
    }
    public bool CompleteLevel()
    {
        for(int a = 0; a < steps.Length; a++)
        {
            if (steps[a] == true)
                continue;
            else
                return false;
        }
        return true;
    }
    //RETURNS THE PREVIOUS STEP VALUE
    public bool GetLastStepsState(int stepId)
    {
        if (stepId > 0 && stepId < steps.Length)
            return steps[stepId - 1];
        else if (stepId == 0 && stepId < steps.Length)
            return steps[0];
        return false;
    }
    public void SpawnItem(int id, GameObject item, Vector2 position)
    {
        Instantiate(item, position, Quaternion.identity);
        CompleteStep(id);
    }
    public void NarratorPlaySequentialAudios()
    {
        clips.PlayInOrder(Narrator.Instance.voicePlayer);
        Debug.Log("NARRADOR INICIADO");
    }
}
