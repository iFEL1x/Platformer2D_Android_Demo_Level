using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemBase))]                        //Надстройка над Unity, переопределяем ее через "ItemBase"
public class ItemBaseEditor : Editor
{
    private ItemBase itemBase;


    /*Ссылка на нашу Базу через поле "Target" заложенное в "Editor" через который мы наследуемся, он
    представляет из себя яссылку типа "UnityEngine" Object и по факту является объектом "[CustomEditor(typeof(ItemBase))]"
    он хранит в себе ссылку на "ItemBase" в поле "Target" */
    private void Awake()
    {
        itemBase = (ItemBase)target;                    
    }

    public override void OnInspectorGUI()               //Что бы изменить внешний вид GUI Unity необходимо писать в данном методе
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("New Item"))               //Создаем кнопку внутри блока "if"
        {
            itemBase.CreateItem();
        }

        if (GUILayout.Button("Remove"))
        {
            itemBase.RemoveItem();
        }

        if (GUILayout.Button("<="))
        {
            itemBase.PrevItem();
        }

        if (GUILayout.Button("=>"))
        {
            itemBase.NextItem();
        }

        GUILayout.EndHorizontal();

        base.OnInspectorGUI();                          //Метод отвечает за обработку\Отрисовку GUI Unity
    }
}
