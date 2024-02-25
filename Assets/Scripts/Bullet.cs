using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _shootSpeed = 0.5f;

    private Transform _target;

    public void Enable()
    {
        gameObject.SetActive(true);
        StartCoroutine(GoToTarget());
    }

    public void Disable() => gameObject.SetActive(false);

    public void SetTarget(Transform target) => _target = target;

    private IEnumerator GoToTarget()
    {
        while (transform.position != _target.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _shootSpeed);
            yield return null;
        }

        Disable();
    }
}