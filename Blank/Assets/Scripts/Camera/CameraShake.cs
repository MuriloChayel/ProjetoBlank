using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }
    private Animator an;

    private void Awake()
    {
        an = GetComponent<Animator>();
        Instance = this;
    }
    public void Shake(float wait)
    {
        StartCoroutine(WaitToShake(wait));
    }
    IEnumerator WaitToShake(float wait)
    {
        yield return new WaitForSeconds(wait);
        print("Shaking");
        an.Play("shake_02");
        an.SetBool("exit", true);

    }
}
