using UnityEngine;

/*  Класс объектов "Tutorial" группы "StartTutorial", отвечает за отображение подсказок */

public class StartTutorial : MonoBehaviour
{
    [SerializeField] GameObject canvasTutorial;
    [SerializeField] GameObject tutorial;
    [SerializeField] GameObject button;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(canvasTutorial && tutorial && button)
            {
                Time.timeScale = 0;
                canvasTutorial.SetActive(true);
                tutorial.SetActive(true);
                button.SetActive(true);
            }
        }
    }
}
