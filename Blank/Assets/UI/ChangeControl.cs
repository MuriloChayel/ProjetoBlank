using UnityEngine;

public class ChangeControl : MonoBehaviour
{
    public bool changing = false;
    public GameObject player;
    public int number;
    public string subs;
    void Start()
    {
        subs = null;
    }

    void Update()
    {
        if (changing == true && Input.inputString != "")
        {
            changing = false;
            subs = Input.inputString;
        }
        KeyCode n = (KeyCode)System.Enum.Parse(typeof(KeyCode), subs.ToString());
    }

    public void ChangeButton(int x)
    {
        if (changing == false)
        {
            number = x;
            changing = true;
        }
    }
}
