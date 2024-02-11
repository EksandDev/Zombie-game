using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        _text.text = ScoreAndMoney.Score.ToString();
    }
}
