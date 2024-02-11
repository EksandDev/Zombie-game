using UnityEngine;
using UnityEngine.EventSystems;

public class HealthUpgrade : UpgradeBase, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private MilitaryBase _militaryBase;

    private float _maxHealth;

    private void Start()
    {
        _shop = transform.parent.parent.GetComponent<Shop>();
        _audio = transform.parent.parent.GetComponent<AudioSource>();
        _purchase = _shop.PurchaseSound;
        _cancel = _shop.CancelSound;

        _maxUpgradeLevel = 5;
        _upgradeName = "Здоровье";
    }

    private void ShowText()
    {
        _upgradeDescription = "Повышает прочность вашей базы. Уровень: " + _upgradeLevel + "/" + _maxUpgradeLevel;
        _shop.UpgradeName.text = _upgradeName;
        _shop.Description.text = _upgradeDescription;
        _shop.Price.gameObject.SetActive(true);

        if (_upgradeLevel < _maxUpgradeLevel)
            _shop.Price.text = "Цена: " + _price;

        else
            _shop.Price.text = "Максимум";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowText();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ShowText();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _shop.Price.gameObject.SetActive(false);
    }

    public override void AddLevel()
    {
        if (_upgradeLevel < _maxUpgradeLevel && ScoreAndMoney.Money >= _price)
        {
            _upgradeLevel++;
            PlayerPrefs.SetInt("HealthLevel", _upgradeLevel);

            ShowText();
            CheckUpgradeLevel("HealthLevel");

            _audio.PlayOneShot(_purchase, 0.2f);
        }

        else
            _audio.PlayOneShot(_cancel, 0.2f);
    }

    public override void CheckUpgradeLevel(string key)
    {
        _upgradeLevel = PlayerPrefs.GetInt(key);

        switch (_upgradeLevel)
        {
            case 0:
                _maxHealth = 100;
                break;
            case 1:
                _maxHealth = 150;
                ScoreAndMoney.Money -= _price;
                _price = 200;
                break;
            case 2:
                _maxHealth = 250;
                ScoreAndMoney.Money -= _price;
                _price = 400;
                break;
            case 3:
                _maxHealth = 400;
                ScoreAndMoney.Money -= _price;
                _price = 600;
                break;
            case 4:
                _maxHealth = 650;
                ScoreAndMoney.Money -= _price;
                _price = 1000;
                break;
            case 5:
                _maxHealth = 1000;
                ScoreAndMoney.Money -= _price;
                break;
        }

        _militaryBase.MaxHealth = _maxHealth;
        _militaryBase.Health = _maxHealth;
    }
}
