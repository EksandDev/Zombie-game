using UnityEngine;

public class FirePoint : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private AudioClip _shotSound;

    public static float FireDelay { get => _fireDelay; set => _fireDelay = value; }

    private AudioSource _audio;
    private float _fireElapsedTime = 0;
    private static float _fireDelay = 0.6f;

    private void Start()
    {
        _audio = transform.parent.GetComponent<AudioSource>();
    }

    private void Update()
    {
        _fireElapsedTime += Time.deltaTime;

        if (Input.GetButton("Fire1") && _fireElapsedTime >= _fireDelay && 
            transform.parent.TryGetComponent<MainSoldier>(out MainSoldier _mainSoldier))
            Shoot();

        if (transform.parent.TryGetComponent<SupportSoldier>(out SupportSoldier _supportSoldier) &&
            _fireElapsedTime >= _fireDelay)
        {
            if(_supportSoldier.IsReadyToShoot)
                Shoot();
        }
    }

    private void Shoot()
    {
        _fireElapsedTime = 0;
        Instantiate(_bulletPrefab, transform.position, transform.parent.rotation);

        _audio.PlayOneShot(_shotSound, 0.1f);
    }
}
