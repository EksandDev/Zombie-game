using TMPro;
using UnityEngine;

public abstract class UpgradeBase : MonoBehaviour
{
    public int UpgradeLevel { get => _upgradeLevel; set => _upgradeLevel = value; }

    protected TMP_Text _nameText;
    protected Shop _shop;
    protected AudioSource _audio;
    protected AudioClip _purchase;
    protected AudioClip _cancel;
    protected int _maxUpgradeLevel = 0;
    protected int _upgradeLevel = 0;
    protected int _price = 100;
    protected string _upgradeName;
    protected string _upgradeDescription;

    public abstract void AddLevel();
    public abstract void CheckUpgradeLevel(string key);
}
