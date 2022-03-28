using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool activeInventory = true;

    [HideInInspector] public PlayerInventory inventory;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject tutorial;

    public Dictionary<GameObject, Health> healthContainer; //Описываем словарь с типом GameObject и значением Health, ключ GameObject, значение Health(например)
    public Dictionary<GameObject, Coin> coinContainer;
    public Dictionary<GameObject, BuffReciever> buffRecieverContainer;
    public Dictionary<GameObject, ItemComponent> itemsContainer;
    public ItemBase itemDataBase;

    public static GameManager Instance { get; set; } //Singlton

    private void Awake()
    {     
        Instance = this; //Инициалия Singltone

        healthContainer = new Dictionary<GameObject, Health>(); //Инициализируем словарь
        coinContainer = new Dictionary<GameObject, Coin>();
        buffRecieverContainer = new Dictionary<GameObject, BuffReciever>();
        itemsContainer = new Dictionary<GameObject, ItemComponent>();
    }


    public void OnClickInventory()
    {
        if (!menu.activeInHierarchy && !tutorial.activeInHierarchy && !settings.activeInHierarchy)
        {
            Player player = Player.Instance;
            if (activeInventory)
            {
                player._UIOff = false;
                inventoryPanel.SetActive(true);
                activeInventory = false;
                Time.timeScale = 0;
            }
            else
            {
                player._UIOff = true;
                inventoryPanel.SetActive(false);
                activeInventory = true;
                Time.timeScale = 1;
            }
        }
    }

    public void OnClickPause()
    {
        if (!inventoryPanel.activeInHierarchy && !tutorial.activeInHierarchy && !settings.activeInHierarchy)
        {
            Player player = Player.Instance;
            if (Time.timeScale > 0)
            {
                player._UIOff = false;
                menu.gameObject.SetActive(true);
                Time.timeScale = 0;
            }

            else
            {
                player._UIOff = true;
                menu.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }


    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
        Time.timeScale = 1;
    }


    public void OnClickSettings()
    {
        menu.SetActive(false);
        settings.SetActive(true);
    }


    public void OnClickBack()
    {
        menu.SetActive(true);
        settings.SetActive(false);
    }


    public void OnClickMainMenu()
    {
        if (menu)
        {
            SceneManager.LoadScene(0);
        }
    }
}
