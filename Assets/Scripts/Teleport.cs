using System.Collections;
using UnityEngine;

/* Класс объекта "Teleport", телепортирует игрока в заданную координату */

public class Teleport : MonoBehaviour
{
    [SerializeField] private float positionX;
    [SerializeField] private float positionY;
    [SerializeField] private int lifeTimeAtExitPortal;
    [SerializeField] private GameObject portalExit;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.position = new Vector2(positionX, positionY);
            var portal = Instantiate(portalExit, new Vector2(positionX, positionY), Quaternion.identity);         //Создает портала на выходе.
            StartCoroutine(StartLife(portal));
        }
    }

    //Карутина ожидает и закрывает портал выхода.
    private IEnumerator StartLife(GameObject portal)
    {
        yield return new WaitForSeconds(lifeTimeAtExitPortal);
        Destroy(portal);
        yield break;
    }
}
