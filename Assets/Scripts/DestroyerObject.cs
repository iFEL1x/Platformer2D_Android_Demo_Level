using System.Collections;
using UnityEngine;

public class DestroyerObject : MonoBehaviour
{   
    [SerializeField] private bool _triggerDestroy;
    [SerializeField] private bool _collisionDestroy;
    [SerializeField] private string _tagColObject;
    [SerializeField] private float _timeDestroyColObject;
    [Space(10)]
    [SerializeField] private bool _destroyThisObject;
    [SerializeField] private float _timeDestroyThisObject;


    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.healthContainer.ContainsKey(collision.gameObject) || _triggerDestroy)
        {
            if (collision.gameObject.CompareTag(_tagColObject))
            {
                var health = GameManager.Instance.healthContainer[collision.gameObject];
                StartCoroutine(pauseDestroy(health));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(GameManager.Instance.healthContainer.ContainsKey(collision.gameObject) || _collisionDestroy)
        {
            if (collision.gameObject.CompareTag(_tagColObject))
            {
                var health = GameManager.Instance.healthContainer[collision.gameObject];
                StartCoroutine(pauseDestroy(health));
            }
        }
    }

    private IEnumerator pauseDestroy(Health health)
    {
        yield return new WaitForSeconds(_timeDestroyColObject);
        health.TakeHit(1000, gameObject);
        
        yield return new WaitForSeconds(_timeDestroyThisObject);
        if (_destroyThisObject)
        {
            Destroy(gameObject);
        }
        yield break;
    }
}
