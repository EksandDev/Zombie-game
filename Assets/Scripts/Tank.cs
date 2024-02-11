using UnityEngine;

public class Tank : MonoBehaviour
{
    [SerializeField] private SoldierTrigger _trigger;
    [SerializeField] private GameObject _explosionEffect;

    private Transform _currentTarget;
    private float _rotationSpeed = 5;
    private bool _isReadyToShoot;
    private float _fireElapsedTime = 0;
    private float _fireDelay = 10;
    private float _radius = 10;
    private float _damage = 500;

    private void Update()
    {
        _fireElapsedTime += Time.deltaTime;

        CheckReadyToShoot();

        if (_isReadyToShoot )
        {
            RotateToTarget();
            Shoot();
        }
    }

    private void CheckReadyToShoot()
    {
        if (_trigger.Targets.Count > 0)
            _isReadyToShoot = true;

        else
            _isReadyToShoot = false;
    }

    private void RotateToTarget()
    {
        _currentTarget = _trigger.Targets[0];
        Vector3 direction = (_currentTarget.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, _rotationSpeed * Time.deltaTime);
    }

    private void Shoot()
    {
        if (_fireElapsedTime > _fireDelay)
        {
            _fireElapsedTime = 0;
            Collider[] hitColliders = Physics.OverlapSphere(_currentTarget.position, _radius);
            Instantiate(_explosionEffect, _currentTarget.position, Quaternion.identity);

            foreach (Collider collider in hitColliders)
            {
                if (collider.TryGetComponent<IDamageable>(out IDamageable damageable))
                    damageable.TakeDamage(_damage);
            }
        }
    }
}
