using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SniperUpgrade : UpgradeBase, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private GameObject _sniper;

    private void Start()
    {
        _shop = transform.parent.parent.GetComponent<Shop>();
        _audio = transform.parent.parent.GetComponent<AudioSource>();
        _purchase = _shop.PurchaseSound;
        _cancel = _shop.CancelSound;

        _maxUpgradeLevel = 1;
        _price = 2000;
        _upgradeName = "Снайпер";
    }

    private void ShowText()
    {
        _upgradeDescription =
            "Вызывает на помощь снайпера, который убивает зомби с одного выстрела. Уровень: " + _upgradeLevel + "/" + _maxUpgradeLevel;
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
            PlayerPrefs.SetInt("SniperLevel", _upgradeLevel);

            ShowText();
            CheckUpgradeLevel("SniperLevel");

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
            case 1:
                ScoreAndMoney.Money -= _price;
                _sniper.SetActive(true);
                break;
        }
    }
}
