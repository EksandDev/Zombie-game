using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class Lose : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _recordText;
    [SerializeField] private GameObject[] _otherCanvases;

    private int _record;

    public void Unhide()
    {
        gameObject.SetActive(true);

        foreach (GameObject canvas in _otherCanvases)
            canvas.SetActive(false);

        if (ScoreAndMoney.Score > PlayerPrefs.GetInt("Record"))
        {
            _record = ScoreAndMoney.Score;
            _recordText.text = "Новый рекорд!";
            PlayerPrefs.SetInt("Record", _record);
            YandexGame.NewLeaderboardScores("score", _record);
        }

        else
            _recordText.text = "Рекорд: " + _record;

        _scoreText.text = "Счёт: " + ScoreAndMoney.Score;
        Time.timeScale = 0;
    }

    public void Restart()
    {
        ScoreAndMoney.Score = 0;
        ScoreAndMoney.Money = 0;
        SceneManager.LoadScene(0);
        Time.timeScale = 0;
    }
}
