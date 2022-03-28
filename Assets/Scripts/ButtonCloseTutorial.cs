using UnityEngine;

/* Класс объекта "StartTutorial", кнопка - закрывает обучающие подсказки */

public class ButtonCloseTutorial : MonoBehaviour
{
    [SerializeField] GameObject canvasTutorial;
    [SerializeField] GameObject tutorialControl;
    [SerializeField] GameObject tutorialJump;
    [SerializeField] GameObject tutorialAbout;
    [SerializeField] GameObject buttonTutorial;
    public void OnClickCloseTutorial()
    {
        tutorialControl.SetActive(false);
        tutorialAbout.SetActive(false);
        buttonTutorial.SetActive(false);
        canvasTutorial.SetActive(false);
        Time.timeScale = 1;
    }
}
