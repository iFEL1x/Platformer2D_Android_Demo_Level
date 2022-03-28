using UnityEngine.UI;
using UnityEngine;

public class HealthBarEnemy : MonoBehaviour
{
    private float divideValue;
    private float healthValue;
    private string fullHealthText;

    [SerializeField] private Image health;
    [SerializeField] private Text healthText;
    [SerializeField] private float delta;    
    [HideInInspector] public int damage;
    [HideInInspector] public Health enemyHealth; //Приходит из TriggerDamage


    private void Start()
    {
        divideValue = enemyHealth.CurrentHealth + damage; //Полное здоровье цели
        healthValue = enemyHealth.CurrentHealth / divideValue; //Текущее здоровье цели
        fullHealthText = (enemyHealth.CurrentHealth + damage).ToString(); //Полное здоровье цели в тектовом варианте для HealthBar
    }


    void Update()
    {
        var currentHealth = enemyHealth.CurrentHealth / divideValue;
        if (currentHealth != 0f)
        {
            if (currentHealth > healthValue) //healthValue - здоровье игрока, currentHealth - HealthBar в игре(переменная health данного скрипта)
            {
                healthValue += delta;
            }
            if (currentHealth < healthValue)
            {
                healthValue -= delta;
            }
            if (Mathf.Abs(currentHealth - healthValue) < delta)
            {
                healthValue = currentHealth;
            }
            health.fillAmount = healthValue;
            healthText.text = enemyHealth.CurrentHealth + "/" + fullHealthText; 
        }
    }
}
