using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private bool _isShoot;
    [SerializeField] private Transform _weaponBarrel;
    [SerializeField] private Animator _animator;
    [SerializeField] private TargetPoints _targetPoints;

    private List<Bullet> _pool = new();
    private Transform _target;
    private float _delay = 0.25f;
    private float _poolSize = 10;
    private float _timePerTarget = 2f;

    private void Awake() => FillPool();

    private void Start()
    {
        StartCoroutine(SetTarget());
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        _animator.SetBool("IsShoot", true);
        WaitForSeconds wait = new(_delay);

        while (_isShoot)
        {
            transform.LookAt(_target);
            GetBullet();
            yield return wait;
        }
    }

    private void FillPool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            Bullet bullet = Instantiate(_bullet);
            bullet.Disable();
            _pool.Add(bullet);
        }
    }

    private void GetBullet()
    {
        Bullet bullet = _pool.FirstOrDefault(currentBullet => currentBullet.gameObject.activeSelf == false);

        if (bullet != null)
        {
            bullet.SetTarget(_target);
            bullet.transform.position = _weaponBarrel.position;
            bullet.Enable();
        }
    }

    private IEnumerator SetTarget()
    {
        WaitForSeconds wait = new(_timePerTarget);

        while (_isShoot)
        {
            _target = _targetPoints.GetPoint();
            _targetPoints.ChangePoint();
            yield return wait;
        }
    }
}