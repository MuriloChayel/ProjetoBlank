using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class IghtPlayer : MonoBehaviour
{
    
    private Light2D ligh;
    [Header("Properties")]
    [Range(0, 5)]
    public float blinkTime;
    [Range(1,5)]
    public float timeMultiplier;
    public float learpF;
    public float learpAmount;
    [Range(0,1)]
    public float value;
    private void Awake()
    {
        ligh = GetComponent<Light2D>();
    }
    private void Start()
    {
        StartCoroutine(Blink());
    }
    public float t;
    private void Update()
    {
        value = Mathf.Lerp(learpAmount, learpF, t);
        t += Time.deltaTime * timeMultiplier;
        ligh.intensity = value;

        if (t > 1.0f)
        {
            float temp = learpAmount;
            learpAmount = learpF;
            learpF = temp;
            t = 0.0f;
        }
    }
    IEnumerator Blink()
    {
        yield return new WaitForSeconds(blinkTime);
        learpAmount = Random.Range(0, .5f);
        learpF = Random.Range(.5f, 1f);
        StartCoroutine(Blink());
    }
}
