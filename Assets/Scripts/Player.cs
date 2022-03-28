using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 _direction;
    private bool _isJumping;
    private bool _isBlockMovement;
    private bool _isCouldown;                               //Перезарядка
    private bool _uIOff = true;
    private float _bonusForce;
    private float _bonusDamage;
    private float _bonusHealth;
    private List<Arrow> _arrowPool;                         //Лист(Пул) стрел, для List необходимо использовать коллекцию (using System.Collections.Generic;)
    private UICharacterController _controller;
    private Vector2 _checkPoint;

    [SerializeField] private Camera playerCamera;  
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Health health;
    [SerializeField] private GroundDetection groundDetection;
    [SerializeField] private Arrow arrow;                   //Префаб стрелы
    [SerializeField] private GameObject canvasIsDie;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject canvasTutorial;

    [SerializeField] private float speed;
    [SerializeField] private int force;
    [SerializeField] private float lifeTimeBuff;
    [SerializeField] private float shootForce;              //Сила выстрела
    [SerializeField] private float reloadGun;               //Время перезарядки
    [SerializeField] private int arrowCount;                //Коллиечство создаваемых стрел
    [SerializeField] private float forcePushInAttack;
    [SerializeField] private float minimalHeight;

    [SerializeField] private Arrow currentArrow;            //Импульс стрелы
    [SerializeField] private BuffReciever buffReciever;     //Подписка на Action
    [SerializeField] private Transform arrowSpawnpoint;     //Место респавна "Стрелы"

    #region Property
    public float LifeTimeBuff => lifeTimeBuff;                                      //Панель баффа
    public float ReloadGun => reloadGun;
    public Health Health => health;
    public GameObject CanvasIsDie
    {
        get { return canvasIsDie; }
        set { canvasIsDie = value; }
    }
    public GameObject Canvas
    {
        get { return canvas; }
        set { canvas = value; }
    }
    public Vector2 CheckPoint
    {
        get { return _checkPoint; }
        set { _checkPoint = value; }
    }
    public GroundDetection GroundDetection
    {
        get { return groundDetection; }
        set { groundDetection = value; }
    }
    public bool _UIOff
    {
        get { return _uIOff; }
        set { _uIOff = value; }
    }
    #endregion
    public static Player Instance { get; set; }             //Singltone


    private void Awake()                                    //Инициализируем Singltone 
    {
        Instance = this;
    }
    
    
    private void Start()
    {
        _arrowPool = new List<Arrow>(); //Создаем стрелу и добавляем в пул
        for(int i = 0; i < arrowCount; i++)
        {
            var arrowTemp = Instantiate(arrow, arrowSpawnpoint); //Создаем
            _arrowPool.Add(arrowTemp); //Добавялем
            arrowTemp.gameObject.SetActive(false); //Деактивируем
        }

        health.OnTakeHit += TakeHit;
        buffReciever.OnBuffChanged += ApplyBuffs; //Подписываемся на Делегата OnBuffChanged в BuffReciver
    }

    void Update()
    {
//#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (!canvasTutorial.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.OnClickPause();
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckShoot();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GameManager.Instance.OnClickInventory();
        }
//#endif
        animator.SetFloat("Speed", Mathf.Abs(_direction.x)); //Аниммация ходьбы
        animator.SetBool("isGrounded", groundDetection.isGrounded); //Анимация проверки касания земли - для запуска анимации приземления

        if (!_isJumping && !groundDetection.isGrounded && !animator.GetCurrentAnimatorStateInfo(0).IsName("StartJump"))
        {
            animator.SetTrigger("StartFall"); //(2)Проверка на падение - запуск анимации падения
        }

        _isJumping = _isJumping && !groundDetection.isGrounded; //(3)Проверка на падение - каректруем переменную isJumping

        CheckFall(); //Падение
    }

    void FixedUpdate()
    {
        Move();
    }

    ////////////////////////////////////////////

    void Move()
    {
        _direction = Vector3.zero; //Обнуляем скорость игрока каждый кадр

/*#if UNITY_EDITOR  */
        
        if (Input.GetKey(KeyCode.A))
        {
            _direction = Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _direction = Vector3.right;
        }

/*#endif  */

        if (_controller.Left.IsPressed) //Направление движения вправо
        {
            _direction = Vector3.left;
        }

        if (_controller.Right.IsPressed) //Направление движения влево
        {
            _direction = Vector3.right;
        }
        _direction *= speed; //Умножаем направление игрока на скорость
        _direction.y = rigidBody2D.velocity.y;
        if (!_isBlockMovement)
        {
            rigidBody2D.velocity = _direction; //Перекладываем direction в rigidBody2D таким образом осуществляем передвижение персонажа через rigidBody2D
        }


        if (_direction.x > 0) //Поворот спрайта игрока вправо
        {
            spriteRenderer.flipX = false;

        }
        if (_direction.x < 0) //Поворот спрайта игрока влево
        {
            spriteRenderer.flipX = true;
        }
    }
    

    public void Jump() 
    {
        if (groundDetection.isGrounded && _uIOff)
        {
            rigidBody2D.AddForce(Vector2.up * (force + _bonusForce), ForceMode2D.Impulse); //Параметры прыжка
            animator.SetTrigger("StartJump"); //Запуск анимации прыжка
            _isJumping = true; //(1)Проверка на падение - исключаем падение при прыжке                     
        }
    }

    public void InitUIController(UICharacterController uiController) //Метод для кнопок в Canvas
    {
        _controller = uiController;
        _controller.Jump.onClick.AddListener(Jump); //Подписываемся на событие по кнопке Jump в классе UICharacterController методом AddListener где в скобках указываем ССЫЛКУ на метод Jump данного класса для его вызова (Поэтому Jump без скобок)
        _controller.Fire.onClick.AddListener(CheckShoot);
    }

    private void ApplyBuffs()
    {
        StartCoroutine(StartLife());
    }
    IEnumerator StartLife()
    {
        var forceBuff = buffReciever.Buffs.Find(t => t.type == BuffType.Force); //Ищем элемент с лямбда выражение BuffTyp.Force
        var damageBuff = buffReciever.Buffs.Find(t => t.type == BuffType.Damage);
        var armorBuff = buffReciever.Buffs.Find(t => t.type == BuffType.Armor);

        _bonusForce = forceBuff == null ? 0 : forceBuff.additiveBonus;
        _bonusDamage = damageBuff == null ? 0 : damageBuff.additiveBonus;
        health.SetHealth((int)( _bonusHealth = armorBuff == null ? 0 : armorBuff.additiveBonus ));
        
        yield return new WaitForSeconds(lifeTimeBuff);
        if(forceBuff != null)
        {
            _bonusForce = 0;
            buffReciever.RemoveBuff(forceBuff);
        }
            
        if (damageBuff != null)
        {
            _bonusDamage = 0;
            buffReciever.RemoveBuff(damageBuff);
        }
   
        if (armorBuff != null)
        {
            buffReciever.RemoveBuff(armorBuff);
        }
        yield break;
    }

    void TakeHit(int damage, GameObject attacker)
    {
        animator.SetBool("GetDamage", true);
        animator.SetTrigger("TakeHit");
        _isBlockMovement = true;
        rigidBody2D.AddForce(transform.position.x < attacker.transform.position.x ? new Vector2(-forcePushInAttack, 0) : new Vector2(forcePushInAttack, 0), ForceMode2D.Impulse);
    }

    void UnblockMovement() //Вызов в Аниматоре
    {
        _isBlockMovement = false;
        animator.SetBool("GetDamage", false);
    }


    private Arrow GetArrowFromPool() //Достаем стрелу из пула и удаляем ее от туда
    {
        if (_arrowPool.Count > 0)
        {
            Arrow arrowTemp = _arrowPool[0]; //Достаем
            _arrowPool.Remove(arrowTemp); //Удаляем из пула
            arrowTemp.gameObject.SetActive(true); //Активируем
            arrowTemp.transform.parent = null; //Достаем стрелу удалив у родителя
            arrowTemp.transform.position = arrowSpawnpoint.transform.position; //Устанавливаем позицию респавна
            return arrowTemp; //Возвращаем
        }
        return Instantiate (arrow, arrowSpawnpoint.position, Quaternion.identity); //На случай если в пуле будет 0 стрел, то мы можем создать стрелду через Instantiate
    }

    public void ReturnArrowToPool(Arrow arrowTemp) //Вызываем в Arrow
    {
        if (!_arrowPool.Contains(arrowTemp))
        {
            _arrowPool.Add(arrowTemp);
        }
        arrowTemp.transform.parent = arrowSpawnpoint;
        arrowTemp.gameObject.SetActive(false);
    }

    void CheckShoot()
    {
        if (_uIOff)
        {
            var fireBar = FireBar.Instance;
            if (!_isCouldown && groundDetection.isGrounded && rigidBody2D.velocity.x == 0)
            {
                StartCoroutine(Cooldown());
                animator.SetTrigger("StartShoot");
                if (fireBar)
                {
                    fireBar.StartShoot();
                }
            }
        }
    }

    void InitArrow() //В аниматоре создается стрела
    {
        currentArrow = GetArrowFromPool();
        currentArrow.SetImpulse(Vector2.right, 0, 0, this); //Метод описан в Arrow
    }

    void Shoot() //Выстрел через аниматор
    {
        currentArrow.SetImpulse(Vector2.right, spriteRenderer.flipX ? -force * shootForce : force * shootForce, (int)_bonusDamage, this);      //Метод описан в Arrow
    }
    void CheckFall()
    {
        if (transform.position.y < minimalHeight)
        {
            int damage = 20;
            health.TakeHit(damage, gameObject); 
            rigidBody2D.velocity = new Vector2(0, 0); //Сбрасваем скорость падения
            transform.position = _checkPoint; //Точка респавна
        }
    }

    private IEnumerator Cooldown()
    {
        _isCouldown = true;
        yield return new WaitForSeconds(reloadGun);
        _isCouldown = false;
        yield break;
    }

    void OnDestroy()
    {
        playerCamera.transform.parent = null;
        playerCamera.enabled = true;
    }
}