using UnityEngine;

/* Класс установленный на GameObject делает его ребенком объекта */

public class BecomeParentObject : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.transform.parent = gameObject.transform;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.gameObject.transform.parent = null;
    }
}
