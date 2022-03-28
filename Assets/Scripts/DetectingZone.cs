using UnityEngine;

/* Класс объекта "DetectingZone" группы "EnemyArea\Enemy" определяющий нахождения игрока в "Зона обнаружения". */

public class DetectingZone : MonoBehaviour
{
    private float _playerPosition;

    public float PlayerPosition
    {
        get { return _playerPosition; }
    }

    //Передаем положение игрока в скрипт "EnemyPatrol".
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerPosition = collision.transform.position.x;
        }
    }

    //Обнуляем позицию игрока, когда он вне "Зона обнаружения".
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerPosition = 0;
        }
    }
}
