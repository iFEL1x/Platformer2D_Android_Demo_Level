using UnityEngine.UI;
using UnityEngine;

public class UICharacterController : MonoBehaviour
{
    [SerializeField] private PressedButton left;
    [SerializeField] private PressedButton right;
    [SerializeField] private Button fire;
    [SerializeField] private Button jump;
    [SerializeField] private Button inventory;
    
    public PressedButton Left
    {
        get { return left; }
    }

    public PressedButton Right
    {
        get { return right; }
    }

    public Button Fire
    {
        get { return fire; }
    }

    public Button Jump
    {
        get { return jump; }
    }
    
    public Button Inventory
    {
        get { return inventory; }
    }
    void Start()
    {
        Player.Instance.InitUIController(this);     //Передаем сами себя в класс Player переменную private UICharacterController controller;
    }

}
