using UnityEngine;

public class HitRegLeftTrigger : MonoBehaviour
{
    /* Класс объекта "HitRegLeftTrigger" группы "EnemyArea\Enemy" отвечающий за определение с какой из сторон было 
    зарегистрировано попадание снаряда дальнобойного оружия игрока*/

    private bool collisionLeftDetected = false;
    public bool CollisionLeftDetected
    {
        get { return collisionLeftDetected; }
        set { collisionLeftDetected = value; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            collisionLeftDetected = true;
        }
    }
}
