using UnityEngine;

[HelpURLAttribute("")]
[SelectionBase]

/*  Класс объекта "Enemy" группы "EnemyArea", отвечает за зкорость движения, путрулирование выделенной зоны и реакцию на игрока 
 в случае когда он заходит в зону патрулирования или произвел атаку на "Enemy" */
public class EnemyPatrol : MonoBehaviour
{
    private float _speed;
    private bool _isRightDirection;

    private float _playerPosition;                                  //Позиция игрока в мировых координат.
    private float _playerPositionFromEnemy;                         //Позиция игрока в мировых координат с вычетом мировой позиции "Enemy".
    private bool _playerOutsidePatrolBorder;                        //Определения игрока вне(false) или внутри(true) зоны патрулирования.

    [Header("Стандартные классы")]
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    [Header("Созданные классы")]
    [SerializeField] private Health health;
    [SerializeField] private GameObject healthBarUI;
    [SerializeField] private GroundDetection groundDetection;       //Регистрация косании земли.
    [SerializeField] private CollisionDamage collisionDamage;

    [Header("Объекты зоны патрулирования и обнаружения игрока")]
    [Tooltip("Зона патрулирования")]
    [SerializeField] private GameObject patrolLeftBorder;
    [SerializeField] private GameObject patrolRightBorder;
    [Tooltip("Зона преследования")]
    [SerializeField] private GameObject pursuitLeftBorder;
    [SerializeField] private GameObject pursuitRighBorder;
    [Space(10)]
    [Tooltip("Триггер регистрации попадания")]
    [SerializeField] private HitRegLeftTrigger hitRegLeftTrigger;
    [SerializeField] private HitRegRightTrigger hitRegRightTrigger;
    [Tooltip("Зона обнаружения игрока")]
    [SerializeField] private DetectingZone detectingZone;
    
    [Header("Параметры определяющие скорость движения 'Enemy'")]
    [SerializeField] private float speedDefault;
    [SerializeField] private float speedIsDetectingZone;
    [SerializeField] private float speedIsHitRegTrigger;

    #region Property
    public GameObject HealthBarUI
    {
        get { return healthBarUI; }
        set { healthBarUI = value; }
    }
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }
    #endregion

    private void Awake()
    {
        _speed = speedDefault;
    }

    void Update()
    {
        //Когда "Enemy" атакует игрока он обнуляет скорость на время равное скорости анимации "Атаки"
        if (collisionDamage.IsAttack)
        {
            _speed = 0;
            return;
        }
        //Когда "Enemy" завершает атаку и теряет игрока из "Зона видимости", скорость движения становится по умолчанию.
        else if(_speed == 0 && !collisionDamage.IsAttack)
        {
            _speed = speedDefault;
        }

        //Когда игрок находится внутри "Зона обнаружения", определяем его положение.
        _playerPosition = detectingZone.PlayerPosition;
        
        if (_playerPosition != 0)
        {
            _playerPositionFromEnemy = _playerPosition - transform.position.x;
        }
        else if(_playerPosition == 0 && _playerPositionFromEnemy != 0)
        {
            _playerPositionFromEnemy = 0;
        }

        //Определяем положение игрока внутри "Зона патрулирования".
        _playerOutsidePatrolBorder = true;
        if (patrolLeftBorder.transform.position.x < _playerPosition && _playerPosition < patrolRightBorder.transform.position.x)
        {
            _playerOutsidePatrolBorder = false;
        }

        /* Когда "Enemy" находится в соприкосновении с объектом "Platform", определяем направление и движение для "Enemy"
        в "Зона патрулирования"(1) и поведением на обнаружение игрока (2, 3). */
        if (groundDetection.isGrounded)
        {
            /* Изменение направления движение в лево.
             * 1. "Зона патрулирования" 2. "Триггер регистрации попадания дальнобойным оружием" 3. Положением игрока внутри "Зона патрулирования". */
            if (transform.position.x > patrolRightBorder.transform.position.x || hitRegLeftTrigger.CollisionLeftDetected || (_playerPositionFromEnemy < 0 && !_playerOutsidePatrolBorder))
            {
                ChangeDirectionLeft();
                //Сбрасываем скорость на скорость по умолчанию, только при достижении конца зоны патрулирования.
                if (transform.position.x > patrolRightBorder.transform.position.x)
                {
                    _speed = speedDefault;
                }

                //"Триггер регистрации попадания дальнобойным оружием."
                if (hitRegLeftTrigger.CollisionLeftDetected)
                {
                    _speed = speedIsHitRegTrigger;
                    hitRegLeftTrigger.CollisionLeftDetected = false;
                }
            }

            /* Изменение направления движение в право. 
              * 1. "Зона патрулирования" 2. "Триггер регистрации попадания дальнобойным оружием" 3. Положением игрока внутри "Зона патрулирования". */
            else if (transform.position.x < patrolLeftBorder.transform.position.x || hitRegRightTrigger.CollisionRightDetected || (_playerPositionFromEnemy > 0 && !_playerOutsidePatrolBorder))
            {
                ChangeDirectionRight();
                //Сбрасываем скорость на скорость по умолчанию, только при достижении конца зоны патрулирования.
                if(transform.position.x < patrolLeftBorder.transform.position.x)
                {
                    _speed = speedDefault;
                }

                //"Триггер регистрации попадания дальнобойным оружием."
                if (hitRegRightTrigger.CollisionRightDetected)
                {
                    _speed = speedIsHitRegTrigger;
                    hitRegRightTrigger.CollisionRightDetected = false;
                }
            }

            /* Если игрок, находясь в "Зона обнаружения" попадает в "Зона преследования", 
            то "Enemy" меняет свое направление в сторону игрока и отключает "Триггер регистрации попадания". */
            if (_playerPositionFromEnemy != 0 && (pursuitLeftBorder.transform.position.x < _playerPosition && _playerPosition < pursuitRighBorder.transform.position.x))
            {
                if (_playerPosition < patrolLeftBorder.transform.position.x)
                {
                    ChangeDirectionLeft();
                }
                else if (patrolRightBorder.transform.position.x < _playerPosition)
                {
                    ChangeDirectionRight();
                }
                if(_speed != speedIsHitRegTrigger)
                {
                    _speed = speedIsDetectingZone;
                }

                hitRegLeftTrigger.gameObject.SetActive(false);
                hitRegRightTrigger.gameObject.SetActive(false);
            }

            //Если игрок выходит из "Зона обнаружения", то "Enemy" включает "Триггер регистрации попадания".
            else if (_playerPositionFromEnemy == 0)
            {
                hitRegLeftTrigger.gameObject.SetActive(true);
                hitRegRightTrigger.gameObject.SetActive(true);
            }

            //Если между "Enemy" и игроком растояние сокращается до активации "collisionDamage", останавливаем "Enemy" для нанесения урона.
            if (_playerPosition != 0 && Mathf.Abs(_playerPositionFromEnemy) <= 1.47f)
            {
                if (Player.Instance.GroundDetection.isGrounded)
                {
                    _speed = 0;
                }
            }

            //Движение "Enemy".
            rigidBody.velocity = _isRightDirection ? Vector2.right : Vector2.left;
            rigidBody.velocity *= _speed;

            if (rigidBody.velocity.x > 0)
            {
                spriteRenderer.flipX = true;
            }
            if (rigidBody.velocity.x < 0)
            {
                spriteRenderer.flipX = false;
            }
        }
    }


    //Методы определяющие сторону направления движения "Enemy".
    private void ChangeDirectionLeft()
    {
        _isRightDirection = false;
        detectingZone.gameObject.transform.rotation = Quaternion.Euler(0, 360, 0);
        collisionDamage.gameObject.transform.rotation = Quaternion.Euler(0, 360, 0);
    }

    private void ChangeDirectionRight()
    {
        _isRightDirection = true;
        detectingZone.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        collisionDamage.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    public void EnemyDamage()
    {
        collisionDamage.SetDamage();
    }
}