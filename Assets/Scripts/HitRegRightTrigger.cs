using UnityEngine;

public class HitRegRightTrigger : MonoBehaviour
{
    /* Класс объекта "HitRegRightTrigger" группы "EnemyArea\Enemy" отвечающий за определение с какой из сторон было 
    зарегистрировано попадание снаряда дальнобойного оружия игрока*/

    private bool collisionRightDetected = false;
    public bool CollisionRightDetected
    {
        get { return collisionRightDetected; }
        set { collisionRightDetected = value; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Arrow"))
        {
            collisionRightDetected = true;
        }  
    }
}