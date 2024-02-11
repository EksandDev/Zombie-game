using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private TMP_Text _upgradeNameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private GameObject[] _otherCanvases;
    [SerializeField] private AudioClip _purchaseSound;
    [SerializeField] private AudioClip _cancelSound;

    public TMP_Text UpgradeName { get => _upgradeNameText; set => _upgradeNameText = value; }
    public TMP_Text Description {  get => _descriptionText; set => _descriptionText = value; }
    public TMP_Text Price { get => _priceText; set => _priceText = value; }
    public AudioClip PurchaseSound { get => _purchaseSound; set => _purchaseSound = value; }
    public AudioClip CancelSound { get => _cancelSound; set => _cancelSound = value; }

    private void Update()
    {
        _moneyText.text = "Δενόγθ: " + ScoreAndMoney.Money;
    }

    public void Unhide() 
    {
        foreach (GameObject canvas in _otherCanvases)
            canvas.SetActive(false);

        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        _otherCanvases[0].SetActive(true);
        Time.timeScale = 1;
    }
}
