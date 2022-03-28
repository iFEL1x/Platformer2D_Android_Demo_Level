using UnityEngine.UI;
using UnityEngine;
using System;

/* Класс группы объектов "Canvas/Inventory/Grid/", объекта "Cell" - ячейки инвентаря. */

public class Cell : MonoBehaviour
{
    public Action OnUpdateCell;

    [SerializeField] private Image icon;
    [SerializeField] private Text text;
    
    private Item item;


    private void Awake()
    {
        icon.sprite = null;
    }

    /* Вызов в "InventoryUIController", заменяет пустую ячейку инвентаря на найденный объект и в обратном порядке. */
    public void Init(Item item)             
    {
        this.item = item;
        if (item == null)
        {
            icon.sprite = null;
            icon.enabled = false;
            text.text = null;
        }
        else
        {
            icon.enabled = true;
            icon.sprite = item.Icon;
            text.text = item.ItemName;
            icon.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }

    }
    public void OnClickCell() //Активируем зелья в инветаре.
    {
        if (BuffBar.Instance.Coroutine == null)
        {
            if (item == null)
            {
                return;
            }

            GameManager.Instance.inventory.Items.Remove(item);
            Buff buff = new Buff
            {
                type = item.Type, //Тип бонуса.
                additiveBonus = item.Value //Адитивный бонус.
            };

            GameManager.Instance.inventory.buffReciever.AddBuff(buff); //Активируем зелье.

            BuffBar.Instance.UseItem(item);
            icon.GetComponent<Image>().color = new Color32(130, 136, 183, 255); //Изменяем цвет пустой иконки на стандартный цвет инвентаря в замен уничтоженного "item".

            if (OnUpdateCell != null) //Проверка делегата на подписанные методы, при наличие - вызывает.
            {
                OnUpdateCell();
            }
        }
    }   
}
