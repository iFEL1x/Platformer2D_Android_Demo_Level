using UnityEngine;

public class TriggerDamage : MonoBehaviour
{

    [SerializeField] private bool isDestroyAfterCollision;              //Булевая переменная задействованная для уничтожения стрелы
    [SerializeField] private int damage;

    private IObjectDestroyer objectDestroyer;
    private GameObject parent;

    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public GameObject Parent
    {
        get { return parent; }
        set { parent = value; }

    }

    public static TriggerDamage Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    ////////////////////////////////////////////

    public void Init(IObjectDestroyer objectDestroyer) //Передаем стрелу, метод класса Arrow - SetImpulse
    {
        this.objectDestroyer = objectDestroyer;
    }

    private void OnTriggerEnter2D(Collider2D collision) //Нанесение урона
    {
        Physics2D.IgnoreLayerCollision(8, 9, true); //Игнорируем коллайдер тригера агрессии врага
        if (collision.gameObject == parent)
        {
            return; //Прерывается метод
        }

        if (GameManager.Instance.healthContainer.ContainsKey(collision.gameObject)) //Если есть Health то True
        {
            Health health = GameManager.Instance.healthContainer[collision.gameObject]; //Берем health из словаря                    
            var healthBarUI = collision.gameObject.GetComponent<EnemyPatrol>().HealthBarUI; //Берем панель HealthBar сцены у персонажа в которого попали
            var healthBarEnemy = healthBarUI.gameObject.GetComponent<HealthBarEnemy>(); //Берем класс HealthBarEnemy в отдельную переменную
            
            if (!healthBarUI.activeInHierarchy) //Проверяем активна ли HealthBar у врага, если нет, то активируем
            {
                healthBarUI.gameObject.SetActive(true); //Активируем HealthBar врага
                healthBarEnemy.damage = damage; //Передаем damage стрелы, что бы прибавить его уже к вычтеному здоровью от стрелы и получить число равному полному здоровью
            }
            healthBarEnemy.enemyHealth = health;
            health.TakeHit(damage, gameObject);
        }

        if (isDestroyAfterCollision)                                                        //Уничтожаем стрелу
        {
            if (collision.gameObject.CompareTag("IgnoreTriggerArrow"))
            {
                return;
            }

            if (objectDestroyer == null)
            {
                Destroy(gameObject);
            }
            else 
            {
                objectDestroyer.Destroy(gameObject);
            }
        }
    }
}

public interface IObjectDestroyer
{
    void Destroy(GameObject gameObject);                                //Метод реализован в классе Arrow  
}