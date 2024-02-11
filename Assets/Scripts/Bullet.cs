using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 20f;

    public static float Damage { get => _damage; set => _damage = value; }

    private Rigidbody _rigidbody;
    private static float _damage = 5;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _rigidbody.AddForce(transform.forward * _speed, ForceMode.Impulse);

        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
