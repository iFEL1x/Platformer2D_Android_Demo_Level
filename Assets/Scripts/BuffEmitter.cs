using UnityEngine;

public class BuffEmitter : MonoBehaviour
{
    [SerializeField] private Buff buff;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.buffRecieverContainer.ContainsKey(collision.gameObject))
        {
            var reciever = GameManager.Instance.buffRecieverContainer[collision.gameObject];
            reciever.AddBuff(buff);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameManager.Instance.buffRecieverContainer.ContainsKey(collision.gameObject))
        {
            var reciever = GameManager.Instance.buffRecieverContainer[collision.gameObject];
            reciever.RemoveBuff(buff);
        }
    }
}


[System.Serializable]
public class Buff
{
    public BuffType type;
    public float additiveBonus;
    public float multipleBonus;
}

/* Выпадающий список(Enum) наследуемый "byte" для уменьшения занимаемого места, где значения задаются для нумерации объекта. */
public enum BuffType : byte
{
    Damage, Force, Armor
}