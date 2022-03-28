using UnityEngine;

/* Класс объекта "BonusHealth", мгновенная аптечка */
public class BonusHealth : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public int bonusHealth;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            Health health = collision.gameObject.GetComponent<Health>();
            health.SetHealth(bonusHealth);
            StartDestroy();
        }
    }

    public void StartDestroy()
    {
        animator.SetTrigger("StartDestroy");
    }

    public void EndDestroy()
    {
        Destroy(gameObject);
    }
}
