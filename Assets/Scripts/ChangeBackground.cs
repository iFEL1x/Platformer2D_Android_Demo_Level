using UnityEngine;

public class ChangeBackground : MonoBehaviour
{
    [SerializeField] private GameObject backgroundForest;
    [SerializeField] private GameObject backgroundDarkForest;
    [SerializeField] private GameObject backgroundLava;
    [SerializeField] private GameObject backgroundDangeon;
    [Space(10)]
    [SerializeField] private float changeOnSurface;
    [SerializeField] private float changeToForest;
    [SerializeField] private float changeToDarkForest;
    [SerializeField] private float changeToLava;
    [SerializeField] private float changeToDangeon;

    #region Property
    public float ChangeToForest => changeToForest;

    public float ChangeToDarkForest => changeToDarkForest;

    public float ChangeToLava => changeToLava;

    public float ChangeToDangeon => changeToDangeon;
    #endregion
    public static ChangeBackground Instance { get; set; }


    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        ToSwichBackground();
    }

    void ToSwichBackground()
    {
        if (transform.position.y > changeToLava) //Верхний уровень биомов.
        {
            if (transform.position.x < changeToDarkForest) //Биом Темный Лес.
            {
                if (!backgroundDarkForest.activeInHierarchy)
                {
                    backgroundForest.SetActive(false);
                    backgroundDarkForest.SetActive(true);
                    backgroundLava.SetActive(false);
                    backgroundDangeon.SetActive(false);
                }
            }

            if (transform.position.x > changeToForest) //Биом Лес.
            {
                if (!backgroundForest.activeInHierarchy)
                {
                    backgroundForest.SetActive(true);
                    backgroundDarkForest.SetActive(false);
                    backgroundLava.SetActive(false);
                    backgroundDangeon.SetActive(false);
                }
            }

            if (transform.position.x > changeToDangeon) //Биом Лес.
            {
                if (backgroundForest.activeInHierarchy)
                {
                    backgroundForest.SetActive(false);
                    backgroundDarkForest.SetActive(false);
                    backgroundLava.SetActive(false);
                    backgroundDangeon.SetActive(true);
                }
            }
        }


        if (transform.position.y < changeToLava) //Нижний уровень биомов.
        {
            if (!backgroundLava.activeInHierarchy) //Биом Лава.
            {
                backgroundForest.SetActive(false);
                backgroundDarkForest.SetActive(false);
                backgroundLava.SetActive(true);
            }
        }
    }
}
