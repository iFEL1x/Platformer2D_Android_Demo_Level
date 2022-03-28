using UnityEngine;

/* Класс объекта "SlidePlatform", задает случайное начала позиции скольжения платформы */
public class StartRandomAnimation : MonoBehaviour
{
    void Start()
    {
        Animator animator = gameObject.GetComponent<Animator>();
        animator.SetFloat("RandomStart", Random.Range(0.1f, 0.7f));
    }
}
