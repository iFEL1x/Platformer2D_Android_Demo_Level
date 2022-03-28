using UnityEngine;

public class ItemComponent : MonoBehaviour, IObjectDestroyer
{
    [SerializeField] private ItemType type;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Item item;
    public void Destroy(GameObject gameObject) //Реализация интерфейса IObjectDestroyer
    {
        MonoBehaviour.Destroy(gameObject); //Конфлик метода "Destroy", нужно явно указать какого класса будем ииспользовать метод "Destroy"
    }
    public Item Item
    {
        get { return item; }
    }
    
    void Start()
    {
        GameManager.Instance.itemsContainer.Add(gameObject, this);
        item = GameManager.Instance.itemDataBase.GetItemOfID((int) type);
        spriteRenderer.sprite = item.Icon;
    }
}

public enum ItemType 
{
    ArmorPotion = 1,
    DamagePotion = 2,
    ForcePotion = 3,
}