using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    [SerializeField] private bool disableBoxCollider2D;
    [SerializeField] GameObject activateObject;
    [SerializeField] Animator animationTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (animationTrigger)
            {
                animationTrigger.SetTrigger("StartTrigger");
            }
            if (activateObject)
            {
                activateObject.SetActive(true);
            }
            if (disableBoxCollider2D)
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }


    /* Use method in Animation*/
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
