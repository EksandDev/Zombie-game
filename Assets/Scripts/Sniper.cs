using UnityEngine;

public class Sniper : MonoBehaviour
{
    [SerializeField] private SoldierTrigger _trigger;
    [SerializeField] private AudioClip _sniperShot;
    [SerializeField] private GameObject _aimPrefab;

    private AudioSource _audio;
    private Transform _currentTarget;
    private Transform _enemy;
    private GameObject _aim;
    private Vector3 _aimPosition;
    private float _previousHealth = 0;
    private float _fireElapsedTime = 0;
    private float _fireDelay = 5f;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();

        _aim = Instantiate(_aimPrefab, _aimPosition, Quaternion.identity);
    }

    private void Update()
    {
        _fireElapsedTime += Time.deltaTime;

        AimControl();
        Shoot();
    }

    private void AimControl()
    {
        if (_currentTarget != null)
        {
            _aimPosition = new Vector3
                    (_currentTarget.position.x, _currentTarget.position.y + 2, _currentTarget.position.z);
            _aim.transform.rotation = _currentTarget.rotation;
            _aim.transform.position = _aimPosition;
            _aim.SetActive(true);
        }

        else if (_currentTarget == null)
            _aim.SetActive(false);
    }

    private void Shoot()
    {
        if (_trigger.Targets.Count > 0)
        {
            for (int i = 0; i < _trigger.Targets.Count; i++)
            {
                _enemy = _trigger.Targets[i];
                bool isDamageable = _enemy.TryGetComponent<IDamageable>(out IDamageable damageable);

                if (_fireElapsedTime >= _fireDelay && _currentTarget != null)
                {
                    _fireElapsedTime = 0;
                    _currentTarget.TryGetComponent<IDamageable>(out IDamageable targetDamageable);
                    targetDamageable.TakeDamage(targetDamageable.Health);
                    _previousHealth = 0;
                    i = 0;

                    _audio.PlayOneShot(_sniperShot, 0.1f);
                }

                if (isDamageable)
                {
                    if (_previousHealth == 0)
                        _previousHealth = damageable.Health;

                    if (damageable.Health >= _previousHealth)
                    {
                        _currentTarget = _enemy;
                    }
                }

                if (i == _trigger.Targets.Count)
                {
                    _previousHealth = 0;
                    i = 0;
                }
            }
        }
    }
}
