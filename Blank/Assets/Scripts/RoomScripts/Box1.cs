using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box1 : MonoBehaviour
{
    public GameObject obj;
    public bool inRadius;
    public GameObject keyRoom;
    private void Update()
    {
        DropInside();
    }

    public void DropInside()
    {
        if (inRadius && Input.GetKey(KeyCode.E))
            StartCoroutine(DropWait(1));
    }
    IEnumerator DropWait(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(obj, transform.position, Quaternion.identity);
        SetKeyVisible();
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            inRadius = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inRadius = false;
    }
    public void SetKeyVisible()
    {
        keyRoom.SetActive(true);
    }
}
