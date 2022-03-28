using System.Collections;
using UnityEngine;

/*  Класс объекта "Arrow", стрела(снаряд) игрока, отвечает за нанесение урона врагу. */

public class Arrow : MonoBehaviour, IObjectDestroyer
{
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private TriggerDamage triggerDamage;
    [SerializeField] private float lifeTime;
    private Player player;
    private int defaultDamage;


    private void Awake()
    {
        defaultDamage = triggerDamage.Damage;
    }
    public void Destroy(GameObject gameObject) //Реализация метода интерфейса "IObjectDestroyer".
    {
        player.ReturnArrowToPool(this);
    }

    public void SetImpulse(Vector2 direction, float force, int bonusDamage, Player player) //Вызов "Player".
    {
        this.player = player;
        triggerDamage.Init(this); //Описан в "TriggerDamage".
        triggerDamage.Parent = player.gameObject;
        triggerDamage.Damage = bonusDamage == 0 ? defaultDamage : bonusDamage;
        rigidBody2D.AddForce(direction * force, ForceMode2D.Impulse);
        if (force < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); //Разворачивает стрелу по направлению атаки.; 
        }
        StartCoroutine(StartLife()); //Удаляет стрелу через установленное время(lifetime), если НЕ произошло столкновение с объектом.
    }

    private IEnumerator StartLife()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
        yield break;
    }
}