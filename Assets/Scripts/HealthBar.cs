using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private float healthValue; //Полное(текущее) здоровье с imageHealth(HealthBar)
    private float currentHealth;
    private string fullHealthText;
    private Player player;

    [SerializeField] private Image health;
    [SerializeField] public Text healthText;
    [SerializeField] private float delta;


    void Start()
    {
        player = Player.Instance;
        healthValue = player.Health.CurrentHealth / 100.0f; //Обращаясь к скрипту игрока Player, мы берем его скрипт Health через свойство Health и берем уже его здоровье health через свойство CurrenHealt в скрипте Health
        fullHealthText = player.Health.CurrentHealth.ToString();
    }


    void Update()
    {
        currentHealth = player.Health.CurrentHealth / 100.0f; // Здоровье игрока
        
        if(currentHealth > healthValue) //currentHealth - здоровье игрока, healthValue - HealthBar в игре(переменная health данного скрипта)
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
        
        health.fillAmount = healthValue; //Такой же скрипт можно сделать через карутины
        healthText.text = player.Health.CurrentHealth + "/" + fullHealthText; //ЕСЛИ не хочется преобразовывать в ToString то в начале\конце перед\после player.Health.CurrentHealth поставить "" +
    }
}
