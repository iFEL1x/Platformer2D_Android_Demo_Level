using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private Animator animator;
    
    private Health health;
    private float direction;
    private bool isAttack = false;

    public bool IsAttack => isAttack;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.Instance.healthContainer.ContainsKey(collision.gameObject))     //Если есть Health то True
        {
            health = GameManager.Instance.healthContainer[collision.gameObject];        //Берем health из словаря
            if (health != null && !isAttack)                                            //Так как у нас есть проверка GameManager (стр.26), эта функция нам не нужна.
            {
                direction = (collision.transform.position - transform.position).x;      //Берем позицию столкнувшигося объекта и объекта который столкнулся(Родитель), вычетаем из нее "X", расстояние рассчитывается от позиции (0.0) тем самым получаем разворот в сторону укуса
                animator.SetFloat("Direction", Mathf.Abs(direction));                   //Из переменнгой direction закладываем парамтры в переменную Direction нашего Аниматора параметром Mathf.Abs
                isAttack = true;
            }
        }
    }
    public void SetDamage()
    {
        if(health != null)
        {
            health.TakeHit(_damage, gameObject);
        }
        health = null; 
        direction = 0;
        animator.SetFloat("Direction", 0f);
        isAttack = false;
    }
}
