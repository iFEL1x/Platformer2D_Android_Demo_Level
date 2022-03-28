using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] private Cell[] cells;              //Массив ячеек
    [SerializeField] private int cellCount;             //Устанавливает базовое колличество ячеек в Инвентаре на объекте Grid
    [SerializeField] private Cell cellPrefab;           //Префаб ячейки
    [SerializeField] private Transform rootParent;      //Трансформ родительского объекта

    void Init()                                         //Создаем инвентарь
    {
        cells = new Cell[cellCount];                    //Массив в котором создаем наши ячейки в инвентаре.
        for (int i = 0; i < cellCount; i++)             //Заполняем инвентарь ячейками, колличество ячеек устанавливаем cellCount
        {
            cells[i] = Instantiate(cellPrefab, rootParent);     //Префаб находится на сцене, Родитель "Grid"
            cells[i].OnUpdateCell += UpdateInventory;   //Подписка на Action
        }
        cellPrefab.gameObject.SetActive(false);         //Выключаем Prefab который находится на сцене, иначе в инвентаре будет на одну ячейку больше он будет первым в списке и не будт в себе что-то хранить.
    }

    private void OnEnable()                             //Метод который выполняется при ВКЛЮЧЕНИИ объекта, так же есть метод который выполняется после выключения объекта OnDisable()
    {
        if (cells == null || cells.Length <= 0)         //Проверка для того что бы ИНВЕНТАРЬ не открывался первый раз пустым, из за неправльиного срабатывания очереди методов (Start и OnEnable)
        {
            Init();
        }
        UpdateInventory();

    }

    public void UpdateInventory()                       //Метод добавления\замене ячеек инвентаря на подобранные предметыпредметы 
    {
        var inventory = GameManager.Instance.inventory;
        foreach (var cell in cells)
        {
            cell.Init(null);
        }
        
        for (int i = 0; i < inventory.Items.Count; i++) //Данным циклом добавляем подобранные предметы из PlayerInventory.Items
        {
            if (i < cells.Length)                       //Проверяем не привысили ли мы колличества ячеекк в инвентаре
            {
                cells[i].Init(inventory.Items[i]);      //Берем зелье из листа Items(Лист не имеет ограничений на хранение) и перекладываем в массив cells(Массив ограничен нашим значением инвентаря). Внимание это Init не данного классс
            }
        }
    }
}
