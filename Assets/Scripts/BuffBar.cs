using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BuffBar : MonoBehaviour
{
    [Header("Панели бафов")]
    [SerializeField] private Text textBuffBar; //Панель баффа, текстовая панель баффа для отчета времени.
    [SerializeField] private GameObject buffBar;
    [SerializeField] private Image iconBuffBar;
    private bool isBuffBar = false;
    
    private Player player;
    private Coroutine coroutine = null;
    private float lifeTimeBuff;
    private float lifeTimeBuffBar;

    public Coroutine Coroutine
    {
        get { return coroutine; }
    }
    public static BuffBar Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        player = Player.Instance;
        lifeTimeBuff = player.LifeTimeBuff;
        lifeTimeBuffBar = lifeTimeBuff;
    }

    void Update()
    {
        if (isBuffBar)
        {
            textBuffBar.text = Mathf.Round(lifeTimeBuffBar -= Time.deltaTime).ToString(); //Обратный отчет.
        }
    }

    public void UseItem(Item item)
    {
        if (coroutine == null)
        {
            coroutine = StartCoroutine(StartLife(buffBar, item));
        }                                                                          
    }

    private IEnumerator StartLife(GameObject gameObject, Item item) //Карутина для активации\деактивации иконки баффа в UI.
    {
        iconBuffBar.sprite = item.Icon; //В иконку баффа на UI добавялет иконку того итема который активирован.
        gameObject.SetActive(true); //Активирует UI панель с таймером баффа.
        isBuffBar = true;
        
        yield return new WaitForSeconds(lifeTimeBuff);
        lifeTimeBuffBar = lifeTimeBuff;
        gameObject.SetActive(false);
        isBuffBar = false;
        coroutine = null;
        yield break;
    }
}
