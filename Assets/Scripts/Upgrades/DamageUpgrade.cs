using UnityEngine;
using UnityEngine.EventSystems;

public class DamageUpgrade : UpgradeBase, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private float _damage;

    private void Start()
    {
        _shop = transform.parent.parent.GetComponent<Shop>();
        _audio = transform.parent.parent.GetComponent<AudioSource>();
        _purchase = _shop.PurchaseSound;
        _cancel = _shop.CancelSound;

        _maxUpgradeLevel = 5;
        _upgradeName = "Урон";
    }

    private void Update()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y + 60, Input.mousePosition.z);
        _shop.Price.transform.position = mousePosition;
    }

    private void ShowText()
    {
        _upgradeDescription = "Повышает урон всем солдатам. Уровень: " + _upgradeLevel + "/" + _maxUpgradeLevel;
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
            PlayerPrefs.SetInt("DamageLevel", _upgradeLevel);

            ShowText();
            CheckUpgradeLevel("DamageLevel");

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
                _damage = 5;
                break;
            case 1:
                _damage = 7;
                ScoreAndMoney.Money -= _price;
                _price = 200;
                break;
            case 2:
                _damage = 11;
                ScoreAndMoney.Money -= _price;
                _price = 400;
                break;
            case 3:
                _damage = 16;
                ScoreAndMoney.Money -= _price;
                _price = 600;
                break;
            case 4:
                _damage = 22;
                ScoreAndMoney.Money -= _price;
                _price = 1000;
                break;
            case 5:
                _damage = 30;
                ScoreAndMoney.Money -= _price;
                break;
        }

        Bullet.Damage = _damage;
    }
}
