using UnityEngine;

public class ParalaxBehavior : MonoBehaviour
{
    private Vector3 targetPreviousPositin;

    [SerializeField] private Transform followingTarget;
    [SerializeField, Range(0f, 1f)] private float parallaxStrenght = 0.1f;
    [SerializeField] private bool disableVerticalParallax;


    private void Awake()
    {
        if (!followingTarget)
        {
            followingTarget = Camera.main.transform;
        }
        targetPreviousPositin = followingTarget.position;
    }

    private void Update()
    {
        var delta = followingTarget.position - targetPreviousPositin;

        /* Задаем параметр в Инспекторе */
        if (disableVerticalParallax)
        {
            delta.y = 0;
        }
        targetPreviousPositin = followingTarget.position;

        transform.position += delta * parallaxStrenght;
    }
}
