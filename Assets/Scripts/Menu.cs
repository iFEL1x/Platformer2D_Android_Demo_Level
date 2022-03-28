using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private InputField nameField;
    [SerializeField] private Animator animator;
    [SerializeField] private int infoSound;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject settings;


    private void Start()
    {
        if (PlayerPrefs.HasKey("Player_Name"))
        {
            nameField.text = PlayerPrefs.GetString("Player_Name");
        }

        if (PlayerPrefs.HasKey("Info_Sound"))
        {
            infoSound = PlayerPrefs.GetInt("Info_Sound");

            if (infoSound == 1)
            {
                animator.SetInteger("info_Sound", infoSound);
            }
            else if (infoSound == 2)
            {
                animator.SetInteger("info_Sound", infoSound);
            }
        }

    }

    public void OnEndEditName() //Ввод "Имя пользователя"
    {
        PlayerPrefs.SetString("Player_Name", nameField.text);
    }

    public void OnInputeSound() //Кнопка "Mute"
    {
        if (infoSound == 1)
        {
            infoSound = 2;
            animator.SetInteger("info_Sound", infoSound);
            PlayerPrefs.SetInt("Info_Sound", infoSound);
        }
        else if (infoSound == 2)
        {
            infoSound = 1;
            animator.SetInteger("info_Sound", infoSound);
            PlayerPrefs.SetInt("Info_Sound", infoSound);
        }
    }

    public void OnClickPlay() //Кнопка "Play"
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void OnClickSettings() //Кнопка "Settings"
    {
        menu.SetActive(false);
        settings.SetActive(true);
    }
    public void OnClickBack() //Кнопка "Back"
    {
        menu.SetActive(true);
        settings.SetActive(false);
    }

    public void OnClickExit() //Кнопка "Exit"
    {
        Application.Quit();
    }
}
