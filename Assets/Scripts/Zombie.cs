using UnityEngine;
using YG;

public class Zombie : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject _deathEffect;
    [SerializeField] private float _health = 10;
    [SerializeField] private float _speed = 2;
    [SerializeField] private float _damage = 5;
    [SerializeField] private int _deathScore = 10;
    [SerializeField] private int _deathMoney = 5;

    public float Health { get => _health; set => _health = value; }

    private Animator _animator;
    private ParticleSystem _particle;
    private SoldierTrigger _soldierTrigger;
    private Transform _target;
    private float _elapsedTime = 0;
    private float _attackDelay = 0.5f;
    private float _rotationSpeed = 5f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _particle = GetComponent<ParticleSystem>();
        _target = GameObject.Find("Base").transform;
    }

    private void Update()
    {
        transform.position += (_target.position - transform.position).normalized * _speed * Time.deltaTime;
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, _rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<SoldierTrigger>(out SoldierTrigger soldierTrigger))
            _soldierTrigger = soldierTrigger;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<MilitaryBase>(out MilitaryBase militaryBase))
        {
            _elapsedTime += Time.deltaTime;
            _speed = 0;

            if (_elapsedTime >= _attackDelay)
            {
                _elapsedTime = 0;
                militaryBase.TakeDamage(_damage);
            }

            _animator.SetBool("isAttacking", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<MilitaryBase>(out MilitaryBase militaryBase))
            _animator.SetBool("isAttacking", false);
    }

    private void OnDestroy()
    {
        _soldierTrigger.Targets.Remove(transform);
    }

    public void TakeDamage(float value)
    {
        _health -= value;

        _particle.Play();

        if(_health <= 0)
        {
            ScoreAndMoney.Money += _deathMoney;
            ScoreAndMoney.Score += _deathScore;
            Vector3 effectPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            Instantiate(_deathEffect, effectPosition, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
