using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveChecker : MonoBehaviour
{
    [SerializeField] private GameObject ShopCanvas;
    [SerializeField] private GameObject[] _upgrades;

    private Shop _shop;

    private void Start()
    {
        _shop = ShopCanvas.GetComponent<Shop>();

        _shop.Unhide();

        foreach (var upgrade in _upgrades)
        {
            if (upgrade.TryGetComponent<DamageUpgrade>(out DamageUpgrade damageUpgrade))
                damageUpgrade.CheckUpgradeLevel("DamageLevel");

            else if (upgrade.TryGetComponent<FireRateUpgrade>(out FireRateUpgrade fireRateUpgrade))
                fireRateUpgrade.CheckUpgradeLevel("FireRateLevel");

            else if (upgrade.TryGetComponent<HealthUpgrade>(out HealthUpgrade healthUpgrade))
                healthUpgrade.CheckUpgradeLevel("HealthLevel");

            else if (upgrade.TryGetComponent<SniperUpgrade>(out SniperUpgrade sniperUpgrade))
                sniperUpgrade.CheckUpgradeLevel("SniperLevel");

            else if (upgrade.TryGetComponent<SoldierUpgrade>(out SoldierUpgrade soldierUpgrade))
                soldierUpgrade.CheckUpgradeLevel("SoldierLevel");

            else if (upgrade.TryGetComponent<TankUpgrade>(out TankUpgrade tankUpgrade))
                tankUpgrade.CheckUpgradeLevel("TankLevel");
        }

        Time.timeScale = 0;
    }

    public void ResetProgress()
    {
        ScoreAndMoney.Score = 0;
        ScoreAndMoney.Money = 0;
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
}
