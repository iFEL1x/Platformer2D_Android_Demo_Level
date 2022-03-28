using UnityEngine.UI;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private GameObject StartTutorial;
    [SerializeField] private Text textSettingsTutorial;
    [Range(1,2)]
    [SerializeField] private int infoTutorial = 1;

    public int InfoTutorial => infoTutorial;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Info_Tutorial"))
        {
            infoTutorial = PlayerPrefs.GetInt("Info_Tutorial");

            if (infoTutorial == 1)
            {
                textSettingsTutorial.GetComponent<Text>().text = "Tutorial On";
                if(StartTutorial)
                    StartTutorial.gameObject.SetActive(true);

            }
            else if (infoTutorial == 2)
            {
                textSettingsTutorial.GetComponent<Text>().text = "Tutorial Off";
                if(StartTutorial)
                    StartTutorial.gameObject.SetActive(false);
            }
        }
    }

    public void OnClickTutorialOff()
    {
        if (infoTutorial == 1)
        {
            infoTutorial = 2;
            textSettingsTutorial.GetComponent<Text>().text = "Tutorial Off";
            PlayerPrefs.SetInt("Info_Tutorial", infoTutorial);
            if (StartTutorial)
                StartTutorial.gameObject.SetActive(false);
        }

        else if (infoTutorial == 2)
        {
            infoTutorial = 1;
            textSettingsTutorial.GetComponent<Text>().text = "Tutorial On";
            PlayerPrefs.SetInt("Info_Tutorial", infoTutorial);
            if (StartTutorial)
                StartTutorial.gameObject.SetActive(true);
        }
    }
}