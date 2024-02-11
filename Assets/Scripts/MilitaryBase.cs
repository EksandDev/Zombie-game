using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MilitaryBase : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _loseScreen;

    public float Health { get => _health; set => _health = value; }
    public float MaxHealth 
    { 
        get => _maxHealth; 
        set 
        { 
            _maxHealth = value;
            _slider.maxValue = _maxHealth;
            _slider.value = _maxHealth;
        } 
    }

    private Lose _lose;
    private float _health = 100;
    private float _maxHealth = 100;

    private void Start()
    {
        _lose = _loseScreen.GetComponent<Lose>();

        _slider.maxValue = _maxHealth;
        _slider.value = _health;
    }

    public void TakeDamage(float value)
    {
        _health -= value;
        _slider.value = _health;

        if (_health <= 0)
        {
            _lose.Unhide();
        }
    }
}
