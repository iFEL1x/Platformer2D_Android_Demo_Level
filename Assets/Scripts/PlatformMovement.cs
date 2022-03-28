using UnityEngine;

[SelectionBase]
public class PlatformMovement : MonoBehaviour
{
    /* Класс объекта "Platform Movement" - передвигающийся платформы */

    private bool _isRightDirection;

    [SerializeField] private GameObject patrolLeftOrDownBorder; //Маяк патрулирования
    [SerializeField] private GameObject patrolRightOrUpBorder;
    [SerializeField, Range(0.0f, 0.07f)] private float speedHorizontal;
    [SerializeField, Range(0.0f, 0.07f)] private float speedVertical;

    void Update()
    {
        if(Time.timeScale != 0)
        {
            if (speedHorizontal != 0)
            {
                if (transform.position.x > patrolRightOrUpBorder.transform.position.x) //Движение С ЛЕВО на ПРАВО до Борта или в сторону сторону противника, direction обнуляется в анимации
                {
                    _isRightDirection = false;
                }
                else if (transform.position.x < patrolLeftOrDownBorder.transform.position.x) //Движение с ПРАВО на ЛЕВО до Борта или в сторону сторону противника, direction обнуляется в анимации
                {
                    _isRightDirection = true;
                }

                transform.position = new Vector2(transform.position.x + (_isRightDirection ? speedHorizontal : -speedHorizontal), transform.position.y);
            }

            if (speedVertical != 0)
            {
                if (transform.position.y > patrolRightOrUpBorder.transform.position.y) //Движение С ЛЕВО на ПРАВО до Борта или в сторону сторону противника, direction обнуляется в анимации
                {
                    _isRightDirection = false;
                }
                else if (transform.position.y < patrolLeftOrDownBorder.transform.position.y) //Движение с ПРАВО на ЛЕВО до Борта или в сторону сторону противника, direction обнуляется в анимации
                {
                    _isRightDirection = true;
                }

                transform.position = new Vector2(transform.position.x, transform.position.y + (_isRightDirection ? speedVertical : -speedVertical));
            }
        }
    }     
}
