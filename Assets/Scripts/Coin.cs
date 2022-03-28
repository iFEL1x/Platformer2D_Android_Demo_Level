using UnityEngine;

/* Класс объекта "Coin" */

public class Coin : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Start()
    {
        GameManager.Instance.coinContainer.Add(gameObject, this);
    }
    public void StartDestroy() //Вызов в "PlayerInventory".
    {
        animator.SetTrigger("StartDestroy");
    }

    public void EndDestroy() //Вызов в аниматоре объекта "Coin".
    {
        Destroy(gameObject);
    }
}
