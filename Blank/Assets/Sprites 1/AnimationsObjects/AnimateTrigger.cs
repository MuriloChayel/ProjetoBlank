using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateTrigger : MonoBehaviour
{
    [Header("Properties")]
    private ResolvePuzzles resolvePuzzles;
    
    [Header("Statistics")]
    public bool interact = false;
    public bool inInteractArea = false;
    public string stateName;
    public bool lastStepComplete;
    public float duration;

    
    private Animator an;

    private void Awake()
    {
        an = GetComponent<Animator>();
    }
    private void Start()
    {
        resolvePuzzles = transform.parent.GetComponent<ResolvePuzzles>();
    }

    private void Update()
    {
        lastStepComplete = resolvePuzzles.currentLevel.GetLastStepsState(resolvePuzzles.currentLevel.GetCurrentStep());

        if (inInteractArea && interact  && lastStepComplete)
        {
            Interact();
        }
    }
    public void Interact()
    {
        an.SetBool(stateName, interact);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
    }
    public void SetInteract(bool value)
    {
        interact = value;
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
            inInteractArea = true;
           
    }  
    public void OnTriggerExit2D()
    {
        inInteractArea = false;
    }
}
