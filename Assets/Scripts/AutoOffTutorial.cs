using UnityEngine;

public class AutoOffTutorial : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var infoTutorial = PlayerPrefs.GetInt("Info_Tutorial");

        if (infoTutorial == 1)
        {
            PlayerPrefs.SetInt("Info_Tutorial", 2);
        }
    }
}
