using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private int coinsCount;
    private List<Item> items; //Лист предметов которые мы подбираем(далее ложим в инвентарь в методе "OnEnable" через цикл "for", а удаляем предметы из списка в классе "Cell" метод "OnCliclCell"),использует using System.Collections.Generic;
    
    public Text coinsText;
    public BuffReciever buffReciever;

    public List<Item> Items
    {
        get { return items; }
    }

    private void Start()
    {
        GameManager.Instance.inventory = this;                  //Инициализируем себя в GameManager (другой подход)
        coinsText.text = coinsCount.ToString();                 //Записываем колличество монет в UI
        items = new List<Item>();                               //Создаем лист
    }

    void OnTriggerEnter2D(Collider2D collision)                                             //Сбор моенты через Singleton
    {
        if (GameManager.Instance.coinContainer.ContainsKey(collision.gameObject))
        {
            if (collision.gameObject.CompareTag("Coin"))
            {
                coinsCount++;                                                               //Добавление монеты
            }
            else if (collision.gameObject.CompareTag("Old Coin"))
            {
                coinsCount += 10;                                                           //Добавление монеты
            }
            coinsText.text = coinsCount.ToString();                                         //UI интерфейс
            var coin = GameManager.Instance.coinContainer[collision.gameObject];
            coin.tag = "Untagged";
            coin.StartDestroy();
        }

        if (GameManager.Instance.itemsContainer.ContainsKey(collision.gameObject))
        {
            var itemComponent = GameManager.Instance.itemsContainer[collision.gameObject];
            if (collision.gameObject.name == "Potion")
            {
                items.Add(itemComponent.Item);                                               //Добавляем зелья при столкновении в лист items
                var potion = GameManager.Instance.coinContainer[collision.gameObject];
                collision.gameObject.name = "Untagged";
                potion.StartDestroy();
            }
        }
    }
}

