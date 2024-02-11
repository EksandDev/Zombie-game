using UnityEngine;

public class SupportSoldier : MonoBehaviour
{
    [SerializeField] SoldierTrigger _trigger;

    public bool IsReadyToShoot { get => _isReadyToShoot; }

    private float _rotationSpeed = 10;
    private bool _isReadyToShoot = false;
    private Transform _currentTarget;

    private void Update()
    {
        CheckReadyToShoot();
        RotateToTarget();
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
        if (_isReadyToShoot)
        {
            _currentTarget = _trigger.Targets[0];
            Vector3 direction = (_currentTarget.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}
