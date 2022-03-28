using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FireBar : MonoBehaviour
{
    private Player player;
    private float reloadGun;
    [SerializeField] Image fire;
    

    public static FireBar Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        player = Player.Instance;
        reloadGun = player.ReloadGun;
    }

    public void StartShoot()
    {

        StartCoroutine(ReloadTime());
    }

    private IEnumerator ReloadTime() //Метод запускает анимацию перезарядки оружия в кнопке.
    {
        float timeReload = reloadGun / 100f; //Делим на 100 для заполнения плавного заполнения шкалы
        float timePlusI = timeReload / reloadGun; //Так как fire.fillAmount равен 1 и для точного значения заполнения шкалы число должно быть не > 1 нам нужно его поделить на наше время перезарядки.
        for (float i = 0; i <= 1; i += timePlusI)
        {
            fire.fillAmount = i;
            yield return new WaitForSeconds(timeReload);
        }
    }
}
