using UnityEngine;

/* Класс объекта "ChekPoint", при падени игрока за уровень, восстанавливает его на последнем пройденном "ChekPoint". */

public class CheckPoin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.Instance.CheckPoint = gameObject.transform.position;
        }
    }
}
