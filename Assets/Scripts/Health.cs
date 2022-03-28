using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] GameObject animationDestroy;

    public Action<int, GameObject> OnTakeHit;


    public void Start()
    {
        GameManager.Instance.healthContainer.Add(gameObject, this);
    }


    public int CurrentHealth
    {
        get { return health; }
    }


    public void TakeHit(int damage, GameObject attacker)
    {
        var player = Player.Instance;
        var gameManager = GameManager.Instance;
        health -= damage;


        if(OnTakeHit != null) //Action вызываемый в Player
        {
            OnTakeHit(damage, attacker);
        }

        if (health <= 0)
        {
            if (gameObject.name == "Enemy")
            {
                GetComponent<EnemyPatrol>().Speed = 0;
                GetComponent<EnemyPatrol>().HealthBarUI.SetActive(false);
                GetComponent<Animator>().SetTrigger("StartDestroy");
            }

            if (player.Health.CurrentHealth <= 0)
            {
                Destroy(gameObject);
                player.CanvasIsDie.gameObject.SetActive(true);
                player.Canvas.SetActive(false);
            }
        }
    }


    public void SetHealth(int bonusHealth)
    {
        health += bonusHealth;

        if (health > 100)
        {
            health = 100;
        }
    }


    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}