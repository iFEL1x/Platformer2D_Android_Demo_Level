# <p align="center"> Platformer 2D DEMO</p>

<div align="Center">
    <img src = https://github.com/iFEL1x/iFEL1x/blob/main/Resources/Screenshots/Screen(Platformer%202D)(1).png height="400">
</div>


## Описание проекта

Данный проект является моей выпускной квалификационной работой по курсу 
***"Создание 2D игры на Unity"*** онлайн школы Pixel.one
Проект собран в Unity3D с использованием языка программирование C# и принципов ООП

___
## Скачивание и установка
Для того что бы запустить проект на своем ПК

* [Скачайте](https://unity3d.com/ru/get-unity/download) и [установите](https://docs.unity3d.com/2018.2/Documentation/Manual/InstallingUnity.html) Unity3D последней версии с официального сайта.
* Скачайте проект по [ссылке](https://github.com/iFEL1x/Platformer2D_Android_Demo_Level/archive/refs/heads/main.zip) или с текущей странице "Code\Download ZIP".
    + Распакуйте архив на своем ПК.
* Запустите Unity3D
    + Рядом с кнопкой "Open" нажмите на стрелочку :arrow_down_small:, в открывшимся списке выберете "Add project from disk"
    + Выберете путь распаковки проекта, нажмите "Add Project"

___
## В данном проекте применяется
* **GameManager**, для часто используемых данных как для удобства обращения к ним, так и оптимизации проекта;
* **ScriptableObject**, на котором построена база данных для хранения предметов инвентаря и отображения их
в интерфейсе инвентаря игрока через компоненты класса GUILayout;
* **Action**, делегата событий которого, осуществляется взаимодействие объектов инвентаря с игроком;
* **PlayerPrefs**, сохранение данных пользователя в интерфейсе выполняется компонентом данного класса.

*Демонсрация кода:*

```C#
public class ItemBase : ScriptableObject
{
    [SerializeField, HideInInspector] private List<Item> items;
    [SerializeField] private Item currentItem;
    private int _currentIndex;

    public void CreateItem()
    {
        if (items == null)
            items = new List<Item>();
            
        Item item = new Item();
        items.Add(item);
        currentItem = item;
        _currentIndex = items.Count - 1;
    }
...
```

```C#
public class Menu : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.HasKey("Player_Name"))
            nameField.text = PlayerPrefs.GetString("Player_Name");
...
```

**Основная задача проекта** - изучение Unity3D, его интерфейса, инструментов и возможностей, а так же изучение основ языка программирования С#, принципов ООП, паттернов и их использования.

*Демонстрация финальной части игрового процесса:*

![PLATFORMER 2D](https://github.com/iFEL1x/iFEL1x/blob/main/Resources/Image/Gif/mp4%20to%20GIH(Platformer%202D).gif)
