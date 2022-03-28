using System.Linq;
using UnityEngine;

public class ParallaxEffect: MonoBehaviour
{
    private Player _player;
    private ChangeBackground _changeBackground;
    private bool _hideParalax = false;

    private SpriteRenderer _spriteRenderer;    
    private Color _color;

    //Фоновые паралакс объекты
    private GameObject _paralaxEarth;
    private Transform[] _paralaxObj;
    private Transform _tempObject;
    private Vector3 _positionObject;


    private void Start()
    {
        _changeBackground = ChangeBackground.Instance;
        _player = Player.Instance;

        _paralaxEarth = GameObject.Find("Stage 1");
        _paralaxObj = _paralaxEarth.GetComponentsInChildren<Transform>().Skip(1).ToArray();
    }

    private void Update()
    {
        if(_player != null)
        {
            //Плавно меняем альфа канал на ноль если выходим из биома с заданным фоном
            if (_player.transform.position.x > _changeBackground.ChangeToForest && _player.transform.position.x < 200
                && !_hideParalax || _player.transform.position.x > 275 && _player.transform.position.x < 280)
            {
                for (int i = 0; i < _paralaxObj.Length; i++)
                {
                    _tempObject = _paralaxObj[i];
                    _spriteRenderer = _tempObject.GetComponent<SpriteRenderer>();
                    _color = _tempObject.GetComponent<SpriteRenderer>().color;
                    _color.a -= 0.05f;
                    _spriteRenderer.color = _color;
                }

                if (_spriteRenderer.color.a <= 0f)
                    _hideParalax = true;
            }

            //Плавно меняем альфа канал на еденицу если выходим из биома с заданным фоном
            else if (_player.transform.position.x > 200f && _hideParalax)
            {
                for (int i = 0; i < _paralaxObj.Length; i++)
                {
                    _tempObject = _paralaxObj[i];
                    _tempObject.gameObject.transform.position = _player.gameObject.transform.position;
                    _spriteRenderer = _tempObject.GetComponent<SpriteRenderer>();
                    _color = _tempObject.GetComponent<SpriteRenderer>().color;
                    _color.a += 0.05f;
                    _spriteRenderer.color = _color;
                }

                if (_spriteRenderer.color.a >= 1f)
                {
                    int i;
                    for (i = 0; i < _paralaxObj.Length; i++)
                    {
                        _tempObject = _paralaxObj[i];
                        _tempObject.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.4f, 0.4f, 0.1f);
                    }
                    if (i == _paralaxObj.Length)
                    {
                        _hideParalax = false;
                    }
                }
            }

            if(_player.transform.position.x > 280f && !_hideParalax) //Выключаем фон с паралаксом.
            {
                gameObject.SetActive(false);
            }
        }
    }
}
