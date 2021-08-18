using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu( menuName = "FadeObject/ fade")]
public class CameraBehaviour : ScriptableObject
{
    [Header("Properties")]
    [Range(0, 2f)]
    public float fadeTime;
    [Range(0,5)]
    public float timeTransition;
    public bool fading;
    
    public void SetPositionToShowRooms(Vector2 position)
    {
        Camera.main.transform.parent.position = new Vector3(position.x, position.y, -10);
    }
    public IEnumerator StartFade(Image fade)
    {
        fade.canvasRenderer.SetAlpha(1.0f);
        fade.CrossFadeAlpha(0, fadeTime, false);

        yield return null;
    }
    public IEnumerator FadeIn(Image fade, Vector2 camPosition, GameObject lightInThisRoom)
    {
        //fade.CrossFadeAlpha(0, 2.0f, false);
        fade.enabled = true;
        fade.canvasRenderer.SetAlpha(0.0f);
        fade.CrossFadeAlpha(1, fadeTime, false);
        yield return new WaitForSeconds(fadeTime);
        Debug.Log("waited");
        ResolvePuzzles.Instance.StartCoroutine(FadeOut(fade,camPosition, lightInThisRoom));
    }
    public IEnumerator FadeOut(Image fade, Vector2 camPosition, GameObject light)
    {
        SetPositionToShowRooms(camPosition);
        PlayerLevelProgress.Instance.TurnOffOtherLights(light);

        yield return new WaitForSeconds(timeTransition);
        Debug.Log("Fade Out");
        fade.canvasRenderer.SetAlpha(1.0f);
        fade.CrossFadeAlpha(0, fadeTime, false);
        yield return null;
    }
    public void InitLights(GameObject lamp)
    {
        lamp.SetActive(true);
    }
}
