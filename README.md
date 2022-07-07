# Platformer 2D DEMO Level

## Описание проекта

Данный проект является моей выпускной квалификационной работой по курсу 
***"Создание 2D игры на Unity"*** онлайн школы Pixel.one
Проект собран в Unity3D с использованием языка программирование C# и принципов ООП

### В данном поекте применяется
* **GameManager**, для часто используемых данных как для удобства обращения к ним, так и оптимизации проекта;
* **ScriptableObject**, на котором построена база данных для хранения предметов инвентаря и отображения их
в интерфейсе инвентаря игрока через компоненты класса GUILayout;
* **Action**, делегата событий которого, осуществляется взаимодействие объектов инвентаря с игроком;
* **PlayerPrefs**, сохранение данных пользователя в интерфейсе выполняется компонентом данного класса.


*Демонсрация кода:*

```C#
public class GameManager : MonoBehaviour
{
    public Dictionary<GameObject, BuffReciever> buffRecieverContainer;
    public Dictionary<GameObject, ItemComponent> itemsContainer;
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
