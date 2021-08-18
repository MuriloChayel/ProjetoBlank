using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateTrigger : MonoBehaviour
{

    public bool interact = false;
    public bool inInteractArea = false;
    public string stateName;
    public bool lastStepComplete;

    public float StartAfteXSeconds;
    public float duration;

    
    private Animator an;

    private void Awake()
    {
        an = GetComponent<Animator>();
        GetComponent<BoxCollider2D>().enabled = false;
    }
    private void Start()
    {
        StartCoroutine(WaitToInit());
    }
    IEnumerator WaitToInit()
    {
        yield return new WaitForSeconds(StartAfteXSeconds);
        GetComponent<BoxCollider2D>().enabled = true;
    }
    private void Update()
    {
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
