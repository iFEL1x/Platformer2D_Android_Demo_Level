using UnityEngine;

/* Класс объекта "Spikes", наносит урон при коллизии */

public class Shape : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private Animator animator;

   private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow")) //Игнорирует стрелы.
        {
            return;
        }

        if (GameManager.Instance.healthContainer.ContainsKey(collision.gameObject))
        {
            var health = GameManager.Instance.healthContainer[collision.gameObject];
            if (health != null)
            {
                health.TakeHit(damage, gameObject);
            }    
        }
    }
}
