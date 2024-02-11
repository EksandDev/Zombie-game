using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class FireRateUpgrade : UpgradeBase, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private float _fireRate;

    private void Start()
    {
        _shop = transform.parent.parent.GetComponent<Shop>();
        _audio = transform.parent.parent.GetComponent<AudioSource>();
        _purchase = _shop.PurchaseSound;
        _cancel = _shop.CancelSound;

        _maxUpgradeLevel = 5;
        _upgradeName = "Скорость стрельбы";
    }

    private void ShowText()
    {
        _upgradeDescription = "Увеличивает скорость стрельбы солдат. Уровень: " + _upgradeLevel + "/" + _maxUpgradeLevel;
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
            PlayerPrefs.SetInt("FireRateLevel", _upgradeLevel);

            ShowText();
            CheckUpgradeLevel("FireRateLevel");

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
                _fireRate = 0.6f;
                break;
            case 1:
                _fireRate = 0.5f;
                ScoreAndMoney.Money -= _price;
                _price = 200;
                break;
            case 2:
                _fireRate = 0.4f;
                ScoreAndMoney.Money -= _price;
                _price = 400;
                break;
            case 3:
                _fireRate = 0.3f;
                ScoreAndMoney.Money -= _price;
                _price = 600;
                break;
            case 4:
                _fireRate = 0.2f;
                ScoreAndMoney.Money -= _price;
                _price = 1000;
                break;
            case 5:
                _fireRate = 0.1f;
                ScoreAndMoney.Money -= _price;
                break;
        }

        FirePoint.FireDelay = _fireRate;
    }
}
